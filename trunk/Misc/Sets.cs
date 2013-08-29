﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Universal_Chevereto_Uploadr
{
    public static class Sets
    {
    	//static class containing application's global settings
        private static Ini s;
        private static bool _StartOnStartup, _CopyAfterUpload, _AutoUpdateCheck, _ProxyOn, _Bug563Fix, _SaveScreenshots;
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
            _AutoUpdateCheck=Convert.ToBoolean (s.IniRead ("General", "autocheck"));
            _ProxyOn=Convert.ToBoolean (s.IniRead ("Proxy", "on"));
            _ProxyServer=s.IniRead ("Proxy", "adress");
            _ProxyPort=s.IniRead ("Proxy", "port");
            _Bug563Fix=Convert.ToBoolean (s.IniRead ("Bugfix", "Bug563"));
            _SaveScreenshots=Convert.ToBoolean (s.IniRead ("General", "savescr"));
        }

        public static bool CopyAfterUpload
        {
            get {return _CopyAfterUpload;}
            set {_CopyAfterUpload=value; s.IniWrite ("General", "startup", value.ToString ());}
        }

        public static bool SaveScreenshots
        {
            get {return _SaveScreenshots;}
            set {_SaveScreenshots=value; s.IniWrite ("General", "savescr", value.ToString ());}
        }

        public static bool StartOnStartup
        {
            get {return _StartOnStartup;}
            set {_StartOnStartup=value; s.IniWrite ("General", "sendto", value.ToString ());}
        }

        public static bool AutoUpdateCheck
        {
            get {return _AutoUpdateCheck;}
            set {_AutoUpdateCheck=value; s.IniWrite ("General", "autocheck", value.ToString ());}
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
