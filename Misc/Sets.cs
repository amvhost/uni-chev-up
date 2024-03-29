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

namespace Universal_Chevereto_Uploadr
{
    public static class Sets
    {
    	//static class containing application's global settings
        private static Ini s;
        private static bool _StartOnStartup, _CopyAfterUpload, _ProxyOn, _Bug563Fix, _SaveScreenshots;
        private static string _ProxyServer, _ProxyPort;

        public static void ReadSets ()
        {
            if (System.IO.File.Exists (".\\sets")==false)
            {
                System.IO.File.WriteAllBytes (".\\sets", Properties.Resources.sets);
            }
            s=new Ini (".\\sets");
            //read the settings
            _StartOnStartup=Convert.ToBoolean (s.IniRead ("General", "startup"));
            _CopyAfterUpload=Convert.ToBoolean (s.IniRead ("General", "copyafter"));
            _ProxyOn=Convert.ToBoolean (s.IniRead ("Proxy", "on"));
            _ProxyServer=s.IniRead ("Proxy", "adress");
            _ProxyPort=s.IniRead ("Proxy", "port");
            _Bug563Fix=Convert.ToBoolean (s.IniRead ("Bugfix", "Bug563"));
            _SaveScreenshots=Convert.ToBoolean (s.IniRead ("General", "savescr"));
        }

        public static bool CopyAfterUpload
        {
            get {return _CopyAfterUpload;}
            set {_CopyAfterUpload=value; s.IniWrite ("General", "copyafter", value.ToString ());}
        }

        public static bool SaveScreenshots
        {
            get {return _SaveScreenshots;}
            set {_SaveScreenshots=value; s.IniWrite ("General", "savescr", value.ToString ());}
        }

        public static bool StartOnStartup
        {
            get {return _StartOnStartup;}
            set {_StartOnStartup=value; s.IniWrite ("General", "startup", value.ToString ());}
        }

        public static bool ProxyOn
        {
            get {return _ProxyOn;}
            set {_ProxyOn=value; s.IniWrite ("Proxy", "on", value.ToString ());}
        }

        public static string ProxyServer
        {
            get {return _ProxyServer;}
            set {_ProxyServer=value; s.IniWrite ("Proxy", "adress", value.ToString ());}
        }

        public static string ProxyPort
        {
            get {return _ProxyPort;}
            set {_ProxyPort=value; s.IniWrite ("Proxy", "port", value.ToString ());}
        }

        public static bool Bug563Fix
        {
            get {return _Bug563Fix;}
            set {_Bug563Fix=value; s.IniWrite ("Bugfix", "Bug563", value.ToString ());}
        }
    }
}
