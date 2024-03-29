﻿/*
* Copyright (c) 2013-2014 Dobrescu Andrei
* 
* This file is part of Universal Chevereto Uploadr.
* Universal Chevereto Uploadr is a free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
* as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
* Universal Chevereto Uploadr is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
* of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
* You should have received a copy of the GNU General Public License along with Universal Chevereto Uploadr. If not, see http://www.gnu.org/licenses/.
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.Threading;

namespace Universal_Chevereto_Uploadr
{
	//this class contains the core functionality of the program,
	//dealing with the photo uploading
    public static class Uploadr
    {
        public static Thread t;
        public static System.Windows.Forms.Timer te;

        public static void StartUpload (byte []upload_byte_array=null, string url=null)
        {
        	/* Starts the upload. Arguments:
        	 * nothing (local upload)
        	 * OR upload_byte_array=the byte array containing a picture (local upload)
        	 * OR url=the url (remote upload)*/
            t=new Thread ((ThreadStart)delegate
            {
            VerifyNetworkConnection ();
            foreach (var v in Program.History) v.FromLastUpload=false;
            Program.WriteHistory ();
            if (upload_byte_array==null&&url==null)
            {
            	//upload all the files - used in uploading files from local drive
                foreach (string s in Program.FilesToUpload) Upload (s);
            }
            else if (upload_byte_array!=null&&url==null)
            {
            	//upload bytes - used for uploading a screenshot saved in a MemoryStream, then converted to byte array
                Upload (upload_byte_array);
            }
            else 
            {
            	//upload URL
                Upload (new Uri (url));
            }
            //notify the user
            Program.checker.notify.BalloonTipTitle="Upload done!";
            Program.checker.notify.BalloonTipText="You can open history window to see the photos you've just upload.";
            Program.checker.notify.BalloonTipIcon=ToolTipIcon.Info;
            Program.checker.notify.ShowBalloonTip (1000);
            Program.checker.BuildContextMenu ();
            });
		    t.SetApartmentState(ApartmentState.STA); //Set the thread to STA
		    t.Start(); 
		    t.Join();
		    if (Program.Chevereto3) return;
            //if the setting is on, copy the DirectUrl to the picture to the clipboard
            if (Sets.CopyAfterUpload)
            {
                te=new System.Windows.Forms.Timer ();
                te.Interval=100;
                te.Tick+=delegate
                {
                    if (t.IsAlive==false)
                    {
                        te.Stop ();
                        string txt="";
                        foreach (var v in Program.History) if (v.FromLastUpload) txt+=v.DirectLink+" ";
                        Clipboard.SetText (txt);
                    }
                };
                te.Start ();
            }
        }

        private static void VerifyNetworkConnection ()
        {
        	//verify...
            Program.checker.notify.BalloonTipTitle="Uploading files...";
            Program.checker.notify.BalloonTipText=" ";
            Program.checker.notify.BalloonTipIcon=ToolTipIcon.Info;
            Program.checker.notify.ShowBalloonTip (1000);
            if (Request.Verify ("http://www.google.com/")==false)
            {
                Program.checker.BuildContextMenu ();
                MessageBox.Show ("Unable to connect to the internet. Verify network connection.");
                Program.ApplicationRestart ();
            }
        }

        private static void Upload (string file)
        {
        	//upload a file...
            string []es=new string [1];
            string response="";
            es=file.Split ("\\".ToCharArray ());
            response=Request.Post (Program.Url, GetAPIArgs (), file);
            if (response=="{Retry}") Upload (file);
            else if (response=="{Ignore}") {return;}
            ParseResponse (response, es, file);
        }

        private static void Upload (byte []array)
        {
        	//upload byte array
            string response=Request.Post (Program.Url, GetAPIArgs (), "null", array);
            if (response=="{Retry}") Upload (array);
            else if (response=="{Ignore}") {return;}
            ParseResponse (response);
        }

        private static void Upload (Uri url)
        {
        	//upload url
        	string response=Request.Post (Program.Url, GetAPIArgs (), "null", null, url.ToString ());
            if (response=="{Retry}") Upload (url);
            else if (response=="{Ignore}") {return;}
            ParseResponse (response);
        }
        
        public static string GetAPIArgs ()
        {
        	if (Program.Chevereto3) return "?key="+Program.Key+"&format=json";
        	return "key="+Program.Key;
        }

        private static void ParseResponse (string response, string []es=null, string file="null", byte []array=null)
        {
        	//after uploading the photo, the api will return a Json response containing some data about the photo
        	//that data is stored on a UploadedPhoto class for each single photo, then saved in history
            string []pe=response.Split (new string [] {"\"status_code\":", ","}, StringSplitOptions.None);
            if (Convert.ToInt32 (pe[1])!=200) 
            {
            		//something's wrong
                    Program.checker.BuildContextMenu ();
                    DialogResult dr=MessageBox.Show ("Error data: "+response, "Error", MessageBoxButtons.AbortRetryIgnore);
                    if (dr==DialogResult.Abort)
                    {
                        Program.ApplicationRestart ();
                        return;
                    }
                    else if (dr==DialogResult.Retry)
                    {
                        if (file=="null") Upload (array);
                        else Upload (file);
                        return;
                    }
                    else if (dr==DialogResult.Ignore) return;
            }
            //collect the data
            UploadedPhoto up=new UploadedPhoto ();
            if (es==null) up.LocalName="no local name";
            else up.LocalName=es[es.Length-1];
            {
            	string []p=response.Split (new string [] {(Program.Chevereto3)?"\"filename\":\"":"\"image_filename\":\"", "\","}, StringSplitOptions.None);
            	up.ServerName=ParseValue (p[(Program.Chevereto3)?12:3]);
            }
            {
            	string []p=response.Split (new string [] {(Program.Chevereto3)?"\"url\":":"\"image_url\":", ","}, StringSplitOptions.None);
            	up.DirectLink=ParseValue (p[(Program.Chevereto3)?26:7]);
            }
            {
                string []p=response.Split (new string [] {(Program.Chevereto3)?"\"url\":":"\"image_thumb_url\":", ","}, StringSplitOptions.None);
                up.Miniatura=ParseValue (p[(Program.Chevereto3)?40:13]);
            }
            {
                string []p=response.Split (new string [] {(Program.Chevereto3)?"\"url_viewer\":":"\"image_viewer\":", ","}, StringSplitOptions.None);
                up.Viewer=ParseValue (p[(Program.Chevereto3)?27:20]);
            }
            {
                string []p=response.Split (new string [] {(Program.Chevereto3)?"":"\"image_shorturl\":", ","}, StringSplitOptions.None);
                up.ShortUrl=(Program.Chevereto3)?"":ParseValue (p[21]);
            }
            {
                string []p=response.Split (new string [] {(Program.Chevereto3)?"":"\"image_delete_confirm_url\":", ","}, StringSplitOptions.None);
                up.Delete=(Program.Chevereto3)?"":ParseValue (p[24]);
            }
            {
                int idtofind=1;
                while (FindId (idtofind)==true) idtofind++;
                up.Id=idtofind;
            }
            up.FromLastUpload=true;
            //save the photo to history
            Program.History.Add (up);
            Program.WriteHistory ();
        }

        private static string ParseValue (string s)
        {
        	//parses the responsed - I used this because the response is looking like this:
        	//[\"image_url\":] \"http://..."\ instrad of
        	//[image_url:] http://...
            string r="";
            string []o=s.Split ("\\".ToCharArray ());
            foreach (string es in o) r+=es;
            string []i=r.Split ("\"".ToCharArray (), StringSplitOptions.RemoveEmptyEntries);
            return i[0];
        }

        private static bool FindId (int id)
        {
            foreach (var v in Program.History)
                if (v.Id==id) return true;
            return false;
        }
    }
}
