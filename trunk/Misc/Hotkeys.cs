using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using Universal_Chevereto_Uploadr.KeyboardHook;

namespace Universal_Chevereto_Uploadr
{
	public class Hotkeys
	{
		//the key=index in Program.AppFunctionalities collection
		public static List <object> hHotkeys;
		
		public static void WriteHotkeySetting (int AppFunctionIndex)
		{
			Ini i=new Ini (Program.AppPath+"hotkeys.ini");
			HotkeyData hkd=(HotkeyData)hHotkeys[AppFunctionIndex];
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
			hHotkeys=new List <object> ();
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
				hHotkeys.Add (hkd);
			}
		}
		
		public static void RegisterHotkeys ()
		{
			foreach (object hkd_obj in hHotkeys)
			{
				HotkeyData hkd=(HotkeyData)hkd_obj;
				if (hkd.IsActive)
				{
					hkd.Hook=new cKeyboardHook ();
					//detect modifyers
					ModifierKeys modifyers=ModifierKeys.None;
					Keys ctrl=hkd.Hotkey&Keys.Control, alt=hkd.Hotkey&Keys.Alt, shift=hkd.Hotkey&Keys.Shift;
					if (ctrl==Keys.Control) modifyers|=ModifierKeys.Control;
					if (alt==Keys.Alt) modifyers|=ModifierKeys.Alt;
					if (shift==Keys.Shift) modifyers|=ModifierKeys.Shift;
					//detect the base key
					Keys base_key=Keys.None;
					Enum.TryParse <Keys> (hkd.Hotkey.ToString ().Split (", ".ToCharArray ())[0], true, out base_key);
					if (modifyers!=ModifierKeys.None&&base_key!=Keys.None)
					{
						hkd.Hook.RegisterHotKey (modifyers, base_key);
						hkd.Hook.KeyPressed+=delegate (object sender, KeyPressedEventArgs e)
						{
							cKeyboardHook ckh=(cKeyboardHook)sender;
							for (int i=0; i<hHotkeys.Count; i++)
							{
								if (((HotkeyData)(hHotkeys[i])).Hook==ckh)
								{
									Program.AppFunctionalities[i].Value.Invoke (sender, (EventArgs)e);
									break;
								}
							}
						};
					}
				}
				else hkd.Hook=null;
			}
		}
	}
	
	class HotkeyData
	{
		public bool IsActive;
		public Keys Hotkey;
		public cKeyboardHook Hook;
	}
}
