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
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web;
using System.IO;
using System.Media;
using System.Windows.Forms;
using System.Diagnostics;

namespace Universal_Chevereto_Uploadr
{
	//this is a helper class for making HTTP requests to the site
    public static class Request
    {
        public static bool Verify (string url)
        {
        	//function to verify if a site is up or not
        	//I used this to verify if the computer is connected to the internet, making a request
        	//to google.com (yes, if google.com is down... but usually it is not...)
            try
            {
                WebRequest webRequest=WebRequest.Create (url);  
                WebResponse webResponse=webRequest.GetResponse ();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string Post (string url, string args, string file="null", byte []bytes=null, string urlupload=null)
        {
        	//the main funtion that makes requests
        	/* Arguments:
        	 * url=the url of the api.php
        	 * arg(s)=the arguments, this usually contains the "?key=<<api key>>"
        	 * The next args deals with the data you want to upload:
        	 * * Upload a local file with:
        	 * * * file [optional]=the location of the file ou want to upload
        	 * * * OR btes [optional]=the bytes contained by a file/picture created in a memory stream
        	 * * * Upload a remote file with:
        	 * * * urlupload [optional]=the url of the picture you want to upload*/
            try
            {
                string arg=args;
                //baza64s is the string that contains the url or byte array (converted to base64)
                //string baza64s="";
                if (file=="null"&&bytes!=null&&urlupload==null)
                {
                	//upload a byte array
                	/*old method
                    baza64s=ToBase64 (bytes);
                    arg+="&upload=";*/
                    arg+=BuildAnonymousUploadString (ToBase64 (bytes));
                }
                else if (file!="null"&&bytes==null&&urlupload==null)
                {
                	//upload a local file
                    byte[] filebytes=File.ReadAllBytes (file);
                    /*baza64s=ToBase64 (filebytes);
                    arg+="&upload=";*/
                    arg+=BuildAnonymousUploadString (ToBase64 (filebytes));
                }
                else if (file=="null"&&bytes==null&&urlupload!=null)
                {
                	//remote upload
                    /*arg+="&upload="+urlupload;
                    baza64s="";*/
                    arg+=BuildAnonymousUploadString (urlupload);
                }
                //arg+=baza64s;
                
                //make the request...
                if (Program.Chevereto3==false)
                {
	                HttpWebRequest req=(HttpWebRequest)WebRequest.Create (url);
		            req.KeepAlive=true;
		            req.UserAgent="Mozilla/5.0";
		            req.ProtocolVersion=HttpVersion.Version10;
		            req.Headers.Add ("Cache-Control: no-cache");
		            req.Method="POST";
		            if (Sets.ProxyOn) req.Proxy=new WebProxy (Sets.ProxyServer, Convert.ToInt32 (Sets.ProxyPort));
	                req.ContentType="multipart/form-data";
	                byte[] argb=Encoding.ASCII.GetBytes (arg);
	                req.ContentLength=argb.Length;
	                String sResponse="";
	                using (Stream r=req.GetRequestStream ())
	                {
	                	r.Write (argb, 0, argb.Length);
		            	using (WebResponse response = req.GetResponse ())
		            	{
		            		using (Stream data = response.GetResponseStream())
		            		{
		            			response.Close ();
		            			using (StreamReader sReader = new StreamReader(data))
		            			{
		            				data.Close ();
		            				sResponse = sReader.ReadToEnd();
		            				sReader.Close ();
		            			}
		            		}
		            	}
	                }
	                return sResponse;
                }
                else
                {
                	WebRequest request = WebRequest.Create (url+arg);
		            WebResponse response = request.GetResponse ();
		            Stream dataStream = response.GetResponseStream ();
		            StreamReader reader = new StreamReader (dataStream);
		            string responseFromServer = reader.ReadToEnd ();
		            reader.Close ();
		            response.Close ();
		            return responseFromServer;
                }
            }
            catch (Exception ee)
            {
            	//something's wrong
                DialogResult dr=MessageBox.Show ("Error data: "+ee.Message, "Error", MessageBoxButtons.AbortRetryIgnore);
                if (dr==DialogResult.Abort) 
                {
                    Program.ApplicationRestart ();
                    return "{Abort}";
                }
                else if (dr==DialogResult.Retry)  return "{Retry}";
                else if (dr==DialogResult.Ignore) return "{Ignore}";
                else return "{Unexpected}";
            }
        }

        private static string ToBase64 (byte [] s)
        {
        	//convert a byte array to a base64 string
            string k=Convert.ToBase64String (s, Base64FormattingOptions.None);
            //url encodes the string - because in the url you don't want things like " " instead of "%20"
            string str=HttpUtility.UrlEncode (k);
            return str;
        }
        
        public static string BuildAnonymousUploadString (string content)
        {
        	if (Program.Chevereto3) return "&source="+content;
        	else return "&upload="+content;
        }
    }
}
