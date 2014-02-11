using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Universal_Chevereto_Uploadr
{
	public class Hotkeys
	{
		//the key=index in Program.AppFunctionalities collection
		public static List <object> ActiveHotkeys;
		
		public static void WriteHotkeySetting (int AppFunctionIndex)
		{
			Ini i=new Ini (Program.AppPath+"hotkeys.ini");
			HotkeyData hkd=(HotkeyData)ActiveHotkeys[AppFunctionIndex];
			i.IniWrite (AppFunctionIndex.ToString (), "On", hkd.IsActive.ToString ());
			i.IniWrite (AppFunctionIndex.ToString (), "Value", hkd.Hotkey.ToString ());
		}
		
		public static void ReadHotkeySettings ()
		{
			if (File.Exists (Program.AppPath+"hotkeys.ini")==false)
            {
                StreamWriter sw=new StreamWriter (Program.AppPath+"hotkeys.ini");
				for (int i=0; i<=10; i++)
				{
                	sw.WriteLine ("["+i.ToString ()+"]");
					sw.WriteLine ("On=False");
					sw.WriteLine ("Value=");
				}
                sw.Close ();
            }
			ActiveHotkeys=new List <object> ();
			Ini ini=new Ini (Program.AppPath+"hotkeys.ini");
			for (int i=0; i<=10; i++)
			{
				HotkeyData hkd=new HotkeyData ();
				hkd.IsActive=Convert.ToBoolean (ini.IniRead (i.ToString (), "On"));
				if (hkd.IsActive)
				{
					Keys k=Keys.None;
					Enum.TryParse <Keys> (ini.IniRead (i.ToString (), "Value"), true, out k);
					if (k!=Keys.None) hkd.Hotkey=k;
				}
				ActiveHotkeys.Add (hkd);
			}
		}
	}
	
	class HotkeyData
	{
		public bool IsActive;
		public Keys Hotkey;
	}
}
