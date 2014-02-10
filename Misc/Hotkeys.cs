using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Universal_Chevereto_Uploadr
{
	public class Hotkeys
	{
		//the key=index in Program.AppFunctionalities collection
		public static object /*Dictionary <int, HotkeyData>*/ Active;
		
		public static void WriteHotkeySetting (int AppFunctionIndex)
		{
			/*Ini i=new Ini (Program.AppPath+"hotkeys.ini");
			i.IniWrite (AppFunctionIndex, "On", Active[AppFunctionIndex].IsActive.ToString ());
			i.IniWrite (AppFunctionIndex, "Value", Active[AppFunctionIndex].Hotkey.ToString ());*/
		}
		
		public static void ReadHotkeySettings ()
		{
			/*if (File.Exists (AppPath+"hotkeys.ini")==false)
            {
                StreamWriter sw=new StreamWriter (AppPath+"hotkeys.ini");
				for (int i=0; i<=7; i++)
				{
                	sw.WriteLine ("["+i.ToString ()+"]");
					sw.WriteLine ("On="+false.ToString ());
					sw.WriteLine ("Value=");
				}
                sw.Close ();
            }
			Ini ini=new Ini (Program.AppPath+"hotkeys.ini");
			for (int i=0; i<=7; i++)
			{
				HotkeyData hkd=new HotkeyData ();
				hkd.IsActive=Convert.ToBoolean (ini.IniRead (i.ToString (), "On"));
				if (hkd.IsActive)
				{
					Keys k=null;
					Enum.TryParse <Keys> (ini.IniRead (i.ToString (), "Value"), true, k);
					if (k!=null) hkd.Hotkey=k;
				}
				Active.Add (i, hkd);
			}*/
		}
	}
	
	class HotkeyData
	{
		public bool IsActive;
		public Keys Hotkey;
	}
}
