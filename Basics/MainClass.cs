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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Reflection;

namespace Universal_Chevereto_Uploadr
{
	//the main class of the program, containing on-click events to context menu's items
    public partial class MainClass
    {
    	//some interop used for screenshooting a active window
        [DllImport ("user32.dll")] 
            public static extern IntPtr GetForegroundWindow (); 
        [DllImport ("user32.dll")] 
            private static extern int GetWindowText (IntPtr hWnd, StringBuilder text, int count);   
        [DllImport ("user32.dll")] [return: MarshalAs (UnmanagedType.Bool)]
            private static extern bool GetWindowRect (HandleRef hWnd, out RECT lpRect);
        public IntPtr []Wins;

        public MainClass ()
        {
            var t=new System.Windows.Forms.Timer ();
            t.Interval=100;
            t.Tick+=delegate
            {
            	//@ every 100 ms, get the active window
                IntPtr i=GetForegroundWindow ();
                if (i==IntPtr.Zero) return;
                if (i!=Wins[0])
                {
                	//I memorize 3 windows for the "Active window screenshot" feature
            		//when launching the feature from the context menu, Wins[2] is the "active" window
                    Wins[2]=Wins[1];
                    Wins[1]=Wins[0];
                    Wins[0]=i;
                }
            };
            t.Start ();
        }

        public void uploadFilesToolStripMenuItem_Click (object sender, EventArgs e)
        {
        	//upload files from local disk
            Program.checker.CancelTheUpload ();
            ResetArrays ();
            OpenFileDialog ofn=new OpenFileDialog ();
            ofn.Filter="Images (*.jpg, *.png, *.bmp, *.gif)|*.jpg; *.png; *.bmp; *.gif";
            ofn.Multiselect=true;
            if (ofn.ShowDialog ()==DialogResult.OK)
            {
                int invalids=0;
                foreach (string s in ofn.FileNames)
                {
                    if (CheckForValidFile (s)) 
                    {
                        if (CheckForValidImage (s)) 
                        {
                            Program.FilesToUpload.Add (s); 
                            continue;
                        }
                    }
                    invalids++;
                }
                if (invalids<ofn.FileNames.Length)
                {
                    if (invalids>0) MessageBox.Show ("Some files are invalid");
                    RunUploader ();
                }
                else
                {
                    Program.checker.BuildContextMenu ();
                    if (invalids==1) MessageBox.Show ("Invalid file.");
                    else if (invalids>1) MessageBox.Show ("All files are invalid.");
                }
            }
            else Program.checker.BuildContextMenu ();
        }

        public void RunUploader ()
        {
        	//upload
            Uploadr.StartUpload ();
        }

        public bool CheckForValidImage (string s)
        {
        	//check if the file contains an image
            try
            {
                Image img=Image.FromFile (s);
                if (img.RawFormat.Equals (ImageFormat.Jpeg)) {}
                else if (img.RawFormat.Equals (ImageFormat.Png)) {}
                else if (img.RawFormat.Equals (ImageFormat.Bmp)) {}
                else if (img.RawFormat.Equals (ImageFormat.Gif)) {}
                else 
                {
                    Debug.WriteLine ("Image "+s+" - not recognised the image format");
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckForValidFile (string s)
        {
        	//checking if the file really is a file
        	//and checking if the file is not too big in size
            if (File.Exists (s)==false) 
            {
                Debug.WriteLine ("File "+s+" does not exist");
                return false;
            }
            FileInfo fi=new FileInfo (s);
            if (fi.Length>6.291e+6) return false;
            return true;
        }

        public void exitToolStripMenuItem_Click (object sender, EventArgs e)
        {
        }

        public void uploadFromClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
        	//upload from clipboard - upload if the clipboard contains the PATH to a file or a bitmap
        	//if the clipboard contains a URL, see the UrlUpload class
            Program.checker.CancelTheUpload ();
            ResetArrays ();
            if (Clipboard.GetDataObject ().GetDataPresent (DataFormats.Text))
            {
                string s=Clipboard.GetText (TextDataFormat.Text);
                if (CheckForValidFile (s)) 
                {
                    if (CheckForValidImage (s)) 
                    {
                    	//if there is a path to an image file
                        Program.FilesToUpload.Add (s);
                        Program.checker.CancelTheUpload ();
                        RunUploader ();
                    }
                    else 
                    {
                    	//if there is a path, but not to an image file
                        Program.checker.BuildContextMenu ();
                        MessageBox.Show ("Invalid image path in url.");
                    }
                }
                else 
                {
                	//if there is a text but not a path
                    Program.checker.BuildContextMenu ();
                    MessageBox.Show ("Unknown data in clipboard");
                }
            }
            else if (Clipboard.GetDataObject ().GetDataPresent (DataFormats.Bitmap))
            {
            	//if threre is a bitmap
                Image img=Clipboard.GetImage ();
                Bitmap b=new Bitmap (img);
                MemoryStream ms=new MemoryStream ();
                //"burn" the image to the memory - just like saving it as a file,
                //the bitmap contains the same data if I save it to a local disk or to memory
                b.Save (ms, ImageFormat.Png);
                //and upload it as a bte array
                Uploadr.StartUpload (ms.ToArray ());
                //CancelTheUpload ()=the context menu is replaced by a menu in which you can choose to cancel your upload
                Program.checker.CancelTheUpload ();
            }
        }

        public void uploadDesktopScreenshotToolStripMenuItem_Click (object sender, EventArgs e)
        {
        	//upload a full desktop screenshot
            Program.checker.CancelTheUpload ();
            ResetArrays ();
            //screenshot it...
            Bitmap b=Screenshot ();
            string filepath="";
            //...save it to memory...
            MemoryStream ms=new MemoryStream ();
            b.Save (ms, ImageFormat.Png);
            if (Sets.SaveScreenshots)
            {
            	//..if the picture must be saved to the disk, do it...
                if (Directory.Exists (".\\Screenshots")==false) Directory.CreateDirectory (".\\Screenshots");
                filepath=".\\Screenshots\\Desktop Screenshot "+DateTime.Now.Day.ToString ()+"."+DateTime.Now.Month+"."+
                    DateTime.Now.Year+"."+DateTime.Now.Hour+"."+DateTime.Now.Minute+"."+DateTime.Now.Second;
                for (int x=0; File.Exists (filepath); x++) filepath+=x.ToString ();
                filepath+=".png";
                b.Save (filepath);
                if (CheckForValidImage (filepath))
                {
                	//...upload it if the above operation is successfull
                    Program.FilesToUpload.Add (filepath);
                    RunUploader ();
                }
                else Uploadr.StartUpload (ms.ToArray ());
                //or upload the memory stream turned into a byte array
            }
            else Uploadr.StartUpload (ms.ToArray ());
            //upload as a byte array
            Program.checker.CancelTheUpload ();
        }

        public void uploadedPhotosToolStripMenuItem_Click (object sender, EventArgs e)
        {
        	//show hostory
        	Program.ReadHistory ();
        	//prevent user from opening history twice
       		Program.HistoryForm=new History (true);
       		Program.HistoryForm.ShowDialog ();
       		Program.HistoryForm.IsShowed=true;
        }

        public void aboutToolStripMenuItem_Click (object sender, EventArgs e)
        {
        	//show about window
            Program.checker.ClearMenu ();
            new AboutBox ().Show ();
        }

        public void optionsToolStripMenuItem_Click (object sender, EventArgs e)
        {
        	//show settings window
            Program.checker.ClearMenu ();
            Settings s=new Settings ();
            s.Show ();
        }

        public void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        public void mouseclick_notify(object sender, MouseEventArgs e)
        {
        }

        [StructLayout (LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        
            public int Top;         
            public int Right;       
            public int Bottom;      
        }

        private void ResetArrays ()
        {
            Program.FilesToUpload.Clear ();
        }

        public void uploadCroppedScreenshotToolStripMenuItem_Click (object sender, EventArgs e)
        {
        	//upload a cropped screenshot:
            Program.checker.CancelTheUpload ();
            ResetArrays ();
            //open the cropper
            CropArea ca=new CropArea ();
            if (ca.ShowDialog ()==DialogResult.OK)
            {
            	//screenshot
                Bitmap b=Screenshot ();
                string filepath="";
                if (Sets.SaveScreenshots)
                {
                    if (Directory.Exists (".\\Screenshots")==false) Directory.CreateDirectory (".\\Screenshots");
                    filepath=".\\Screenshots\\Cropped Screenshot "+DateTime.Now.Day.ToString ()+"."+DateTime.Now.Month+"."+
                    DateTime.Now.Year+"."+DateTime.Now.Hour+"."+DateTime.Now.Minute+"."+DateTime.Now.Second;
                    for (int x=0; File.Exists (filepath); x++) filepath+=x.ToString ();
                    filepath+=".png";
                }
                //crop it
                Rectangle z=ca.Output;
                Bitmap Output=new Bitmap (z.Width, z.Height);
                Graphics g = Graphics.FromImage (Output);
                g.DrawImage (b, new Rectangle (0, 0, Output.Width, Output.Height), ca.Output, GraphicsUnit.Pixel);
                //and upload it
                if (Sets.SaveScreenshots)
                {
                    Output.Save (filepath);
                    Program.FilesToUpload.Add (filepath);
                    RunUploader ();
                }
                else 
                {
                    MemoryStream ms=new MemoryStream ();
                    Output.Save (ms, ImageFormat.Png);
                    Uploadr.StartUpload (ms.ToArray ());
                }
            }
        }

        public Bitmap Screenshot ()
        {
        	//makes a full desktop screenshot
            Bitmap b=new Bitmap (Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            Graphics g=Graphics.FromImage (b);
            g.CopyFromScreen (Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, 
                              Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            return b;
        }

        public void dragDropFilesToolStripMenuItem_Click (object sender, EventArgs e)
        {
        	//opens the drag and drop tool to upload files
        	//useful if ou have to upload photos from different locations
        	// - just drag and drop them from Windows Explorer
            Program.checker.CancelTheUpload ();
            ResetArrays ();
            DragDropFiles ddf=new DragDropFiles ();
            if (ddf.ShowDialog ()==DialogResult.OK)
                RunUploader ();
        }

        public void ScreenshotActiveWindow (object sender, EventArgs e)
        {
        	ScreenshotWindow (Wins[2]);
        }
        
        public void ScreenshotWindow (IntPtr window)
        {
        	//get the window handle
            RECT r=new RECT ();
            GetWindowRect (new HandleRef (this, window), out r);
            ResetArrays ();
            //screenshot
            Bitmap b=Screenshot ();
            string filepath="";
            if (Sets.SaveScreenshots)
            {
                if (Directory.Exists (".\\Screenshots")==false) Directory.CreateDirectory (".\\Screenshots");
                filepath=".\\Screenshots\\Active Window Screenshot "+DateTime.Now.Day.ToString ()+"."+DateTime.Now.Month+"."+
                    DateTime.Now.Year+"."+DateTime.Now.Hour+"."+DateTime.Now.Minute+"."+DateTime.Now.Second;
                for (int x=0; File.Exists (filepath); x++) filepath+=x.ToString ();
                filepath+=".png";
            }
            //crop it
            Rectangle z=new Rectangle (r.Left, r.Top, r.Right-r.Left+1, r.Bottom-r.Top+1);
            Bitmap Output=new Bitmap (z.Width, z.Height);
            Graphics g=Graphics.FromImage (Output);
            g.DrawImage (b, new Rectangle (0, 0, Output.Width, Output.Height), z, GraphicsUnit.Pixel);
            Program.checker.CancelTheUpload ();
            //and upload it
            if (Sets.SaveScreenshots)
            {
                Output.Save (filepath);
                Program.FilesToUpload.Add (filepath);
                RunUploader ();
            }
            else 
            {
                MemoryStream ms=new MemoryStream ();
                Output.Save (ms, ImageFormat.Png);
                Uploadr.StartUpload (ms.ToArray ());
            }
        }

        public void UrlUpload (object sender, EventArgs e)
        {
        	//remote upload
            Program.checker.CancelTheUpload ();
            UrlUpload uu=new UrlUpload ();
            if (uu.ShowDialog ()==DialogResult.Cancel) Program.checker.BuildContextMenu ();
        }
    }
}
