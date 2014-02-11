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

//this class is taked from http://www.codeproject.com/Articles/18507/Formless-Notify-Icon-Application

namespace Universal_Chevereto_Uploadr
{
    public class Checker
    {
        public NotifyIcon notify;
        public ContextMenu contextmenu=new ContextMenu ();
        
        public void BuildContextMenu ()
        {
        	//build the main menu
            contextmenu.MenuItems.Clear ();
            for (int i=0; i<Program.AppFunctionalities.Count; i++)
            {
            	var functionality=Program.AppFunctionalities[i];
            	HotkeyData hotkey=(HotkeyData)Hotkeys.hHotkeys[i];
            	string msg=functionality.Key;
            	if (hotkey.IsActive==true&&hotkey.Hotkey!=Keys.None)
            		msg+="\t\t\t\t\t["+HKeySelector.ParseKey (hotkey.Hotkey.ToString ())+"]";
            	MenuItem item=new MenuItem (msg, functionality.Value);
            	contextmenu.MenuItems.Add (item);
			}
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
    }
}
