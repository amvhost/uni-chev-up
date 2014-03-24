/*
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
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;

namespace Universal_Chevereto_Uploadr
{
    static class Program
    {
    	//some globals
        [DllImport ("user32.dll")] public static extern IntPtr GetForegroundWindow (); 
        public static List <string> FilesToUpload;
        public static List <UploadedPhoto> History;
        public static string Url, Key;
        public static History HistoryForm;
        public static MainClass MainClassInstance;
        public static Checker checker;
        public static string AppPath;
        public static bool IsHistoryFormShowed;
		
		/*store app's main features ' descriptions and events:
		* the key=feature's description:
		* 0=Upload files
		* 1=Drag and Drop files					6=Remote upload
		* 2=Upload from clipboard				7=Settings
		* 3=Upload desktop screenshot			8=History
		* 4=Upload cropped screenshot			9=About
		* 5=Upload active window screenshot		10=Exit
		* the value = link the feature's description to its functionality*/
		public static List <KeyValuePair <string, EventHandler>> AppFunctionalities;

        [STAThread]
        static void Main ()
        {
            Application.EnableVisualStyles ();
            Application.SetCompatibleTextRenderingDefault (false);
            //get the folder where the exe is located
            {
            	string [] eps=Application.ExecutablePath.Split ("\\".ToCharArray ());
            	AppPath="";
            	for (int i=0; i<eps.Length-1; i++) AppPath+=eps[i]+"\\";
            }
            //read app's settings, configuration (api key, url) and history
            Sets.ReadSets ();
            ReadConfig ();
            ReadHistory ();
            //a strange bug's fix
            if (Sets.Bug563Fix) Sets.Bug563Fix=false;
            else
            {
                Process []p=Process.GetProcessesByName (Process.GetCurrentProcess ().ProcessName);
                if (p.Length>1) Process.GetCurrentProcess ().Kill ();
            }
            //some init
            IsHistoryFormShowed=false;
            FilesToUpload=new List <string> ();
            MainClassInstance=new MainClass ();
            MainClassInstance.Wins=new IntPtr [3];
            MainClassInstance.Wins[0]=GetForegroundWindow ();
            MainClassInstance.Wins[1]=MainClassInstance.Wins[0];
            MainClassInstance.Wins[2]=MainClassInstance.Wins[1];
			InitAppFunctionalities ();
			Hotkeys.ReadHotkeySettings ();
			Hotkeys.RegisterHotkeys ();
            //initializing the formless notify icon and its menu
            checker=new Checker ();
            Application.Run ();
        }

        public static void ReadHistory ()
        {
        	/* this function reads the content of history.xml, which contains the links
        	 * to the photos you have uploaded in the past.*/
            History=new List <UploadedPhoto> ();
            if (File.Exists (AppPath+"history.xml")==false)
            {
                StreamWriter sw=new StreamWriter (AppPath+"history.xml");
                sw.WriteLine ("<xml><nr-of-files>0</nr-of-files><files></files></xml>");
                sw.Close ();
            }
            XmlTextReader reader=new XmlTextReader (AppPath+"history.xml");
            int n=0;
            string ReadedNode="";
            UploadedPhoto aux=new UploadedPhoto ();
            while (reader.Read ()) 
            {
                switch (reader.NodeType) 
                {
                    case XmlNodeType.Element:
                        ReadedNode=reader.Name;
                        if (reader.Name=="file") aux=new UploadedPhoto ();
                        break;
                    case XmlNodeType.Text:
                        //parse the content of a "photo"...
                        string s=reader.Value;
                        switch (ReadedNode)
                        {
                            case "nr-of-files": n=Convert.ToInt32 (s);      break;
                            case "id":          aux.Id=Convert.ToInt32 (s); break;
                            case "local-name":  aux.LocalName=s;            break;
                            case "server-name": aux.ServerName=s;           break;
                            case "direct-link": aux.DirectLink=s;           break;
                            case "short-url":   aux.ShortUrl=s;             break;
                            case "viewer":      aux.Viewer=s;               break;
                            case "mini":        aux.Miniatura=s;            break;
                            case "from-last-upload": aux.FromLastUpload=Convert.ToBoolean (s); break;
                            case "delete-link": aux.Delete=s;				break;
                            default: break;
                        }
                        break;
                    case XmlNodeType.EndElement:
                        //...and add that "photo" to the "history"
                        if (reader.Name=="file") History.Add (aux);
                        break;
                }
            }
            reader.Close ();
        }
        
        public static void WriteHistory ()
        {
        	/* this funtion writes the content of history.xml with the content of the history list*/
        	StreamWriter sw=new StreamWriter (AppPath+"history.xml");
        	sw.WriteLine ("<xml><nr-of-files>"+History.Count.ToString ());
        	sw.WriteLine ("</nr-of-files><files>");
        	foreach (UploadedPhoto p in History)
        	{
        		sw.WriteLine ("<file>");
        		sw.WriteLine ("<id>"+p.Id+"</id>");
        		sw.WriteLine ("<local-name>"+p.LocalName+"</local-name>");
        		sw.WriteLine ("<server-name>"+p.ServerName+"</server-name>");
        		sw.WriteLine ("<direct-link>"+p.DirectLink+"</direct-link>");
        		sw.WriteLine ("<short-url>"+p.ShortUrl+"</short-url>");
        		sw.WriteLine ("<viewer>"+p.Viewer+"</viewer>");
        		sw.WriteLine ("<mini>"+p.Miniatura+"</mini>");
        		sw.WriteLine ("<from-last-upload>"+p.FromLastUpload.ToString ()+"</from-last-upload>");
        		sw.WriteLine ("<delete-link>"+p.Delete+"</delete-link>");
        		sw.WriteLine ("</file>");
        	}
        	sw.WriteLine ("</files></xml>");
        	sw.Close ();
        }

        public static void ApplicationRestart ()
        {
        	/*funtion which correctly restarts the app (Application.Restart () didn't worked so well here).
        	  It is called each time the upload is done*/
            Sets.Bug563Fix=true;
            Process.Start (Application.ExecutablePath);
            Process.GetCurrentProcess ().Kill ();
        }
        
        public static void ReadConfig ()
        {
        	//read config
            if (File.Exists (AppPath+"config.ini")==false)
            {
            	MessageBox.Show ("config.ini file do not exist");
            	Application.Exit ();
            }
            //remember to set your url to api and the api key in config.ini!
            Ini i=new Ini (AppPath+"config.ini");
            Url=i.IniRead ("Api", "Url");
            Key=i.IniRead ("Api", "Key");
        }
		
		public static void InitAppFunctionalities ()
		{
			AppFunctionalities=new List <KeyValuePair <string, EventHandler>> ();
			AppFunctionalities.Add (new KeyValuePair <string, EventHandler> ("Upload files", new EventHandler (Program.MainClassInstance.uploadFilesToolStripMenuItem_Click)));
			AppFunctionalities.Add (new KeyValuePair <string, EventHandler> ("Drag and Drop files", new EventHandler (Program.MainClassInstance.dragDropFilesToolStripMenuItem_Click)));
			AppFunctionalities.Add (new KeyValuePair <string, EventHandler> ("Upload from clipboard", new EventHandler (Program.MainClassInstance.uploadFromClipboardToolStripMenuItem_Click)));
			AppFunctionalities.Add (new KeyValuePair <string, EventHandler> ("Upload desktop screenshot", new EventHandler (Program.MainClassInstance.uploadDesktopScreenshotToolStripMenuItem_Click)));
			AppFunctionalities.Add (new KeyValuePair <string, EventHandler> ("Upload cropped screenshot", new EventHandler (Program.MainClassInstance.uploadCroppedScreenshotToolStripMenuItem_Click)));
			AppFunctionalities.Add (new KeyValuePair <string, EventHandler> ("Upload active window screenshot", new EventHandler (Program.MainClassInstance.ScreenshotActiveWindow)));
			AppFunctionalities.Add (new KeyValuePair <string, EventHandler> ("Remote upload", new EventHandler (Program.MainClassInstance.UrlUpload)));
			AppFunctionalities.Add (new KeyValuePair <string, EventHandler> ("Settings", new EventHandler (Program.MainClassInstance.optionsToolStripMenuItem_Click)));
			AppFunctionalities.Add (new KeyValuePair <string, EventHandler> ("History", new EventHandler (Program.MainClassInstance.uploadedPhotosToolStripMenuItem_Click)));
			AppFunctionalities.Add (new KeyValuePair <string, EventHandler> ("About", new EventHandler (Program.MainClassInstance.aboutToolStripMenuItem_Click)));
			AppFunctionalities.Add (new KeyValuePair <string, EventHandler> ("Exit", new EventHandler (delegate
			{
				Program.checker.contextmenu.Dispose ();
            	Application.Exit ();
			})));
		}
    }
}
