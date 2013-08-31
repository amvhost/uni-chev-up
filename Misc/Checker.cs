/*
* Copyright (c) 2013 Dobrescu Andrei
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
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

/* This class is taken from http://www.codeproject.com/Articles/18507/Formless-Notify-Icon-Application
 * Author's quote:
"This class creates the notification icon that dotnet 2.0 offers.
It will be displaying the status of the application with appropiate icons.
It will have a contextmenu that enables the user to open the form or exit the application.
The form could be used to change settings of the app which in turn are saved in the app.config or some other file.
This formless, useless, notification sample does only chane the icon and balloontext.
NOTE:Chacker is a Singleton class so it will only allow to be instantiated once, and therefore only one instance.
I have done this to prevent more then one icon on the tray and to share data with the form (if any)*/

namespace Universal_Chevereto_Uploadr
{
    public class Checker
    {
        public NotifyIcon notify;
        private ContextMenu contextmenu=new ContextMenu ();
        
        public void BuildContextMenu ()
        {
        	//build the main menu
            contextmenu.MenuItems.Clear ();
            MenuItem item=new MenuItem ("Upload files", new EventHandler (Program.MainClassInstance.uploadFilesToolStripMenuItem_Click));
            contextmenu.MenuItems.Add (item);
            item=new MenuItem ("Drag && Drop files", new EventHandler (Program.MainClassInstance.dragDropFilesToolStripMenuItem_Click));
            contextmenu.MenuItems.Add (item);
            item=new MenuItem ("Upload from clipboard", new EventHandler (Program.MainClassInstance.uploadFromClipboardToolStripMenuItem_Click));
            contextmenu.MenuItems.Add (item);
            item=new MenuItem ("Upload desktop screenshot", new EventHandler (Program.MainClassInstance.uploadDesktopScreenshotToolStripMenuItem_Click));
            contextmenu.MenuItems.Add (item);
            item=new MenuItem ("Upload cropped screenshot", new EventHandler (Program.MainClassInstance.uploadCroppedScreenshotToolStripMenuItem_Click));
            contextmenu.MenuItems.Add (item);
            item=new MenuItem ("Upload active window screenshot", new EventHandler (Program.MainClassInstance.ScreenshotActiveWindow));
            contextmenu.MenuItems.Add (item);
            item=new MenuItem ("Remote upload", new EventHandler (Program.MainClassInstance.UrlUpload));
            contextmenu.MenuItems.Add (item);
            item=new MenuItem ("Settings", new EventHandler (Program.MainClassInstance.optionsToolStripMenuItem_Click));
            contextmenu.MenuItems.Add (item);
            item=new MenuItem ("History", new EventHandler (Program.MainClassInstance.uploadedPhotosToolStripMenuItem_Click));
            contextmenu.MenuItems.Add (item);
            item=new MenuItem ("About", new EventHandler (Program.MainClassInstance.aboutToolStripMenuItem_Click));
            contextmenu.MenuItems.Add (item);
            item=new MenuItem ("Exit", new EventHandler (Menu_OnExit));
            contextmenu.MenuItems.Add (item);
            //and set the icon of the NotifyIcon control
            try {notify.Icon=Properties.Resources.favicon;}
            catch {}
        }

        public void CancelTheUpload ()
        {
        	//if I choose to cancel...
            contextmenu.MenuItems.Clear ();
            contextmenu.MenuItems.Add (new MenuItem ("Cancel the upload?", delegate
            {
                Program.ApplicationRestart ();
            }));
            notify.Icon=Properties.Resources.uploadicon;
        }

        public void ClearMenu ()
        {
            contextmenu.MenuItems.Clear ();
        }

        public Checker () 
        {
        	//here is the constructor of the class
            BuildContextMenu ();
            //initialize the notify icon
            notify=new NotifyIcon ();
            notify.Text="Universal Chevereto Uploadr";
            notify.ContextMenu = contextmenu;
            notify.Icon=Properties.Resources.favicon;
            notify.Visible = true;
        }

        void Menu_OnExit (Object sender, EventArgs e)
        {
            Program.checker.contextmenu.Dispose ();
            Application.Exit ();
        }
    }
}
