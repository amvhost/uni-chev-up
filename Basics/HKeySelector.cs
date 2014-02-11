using System;
using System.Drawing;
using System.Windows.Forms;

namespace Universal_Chevereto_Uploadr
{
	public partial class HKeySelector : Form
	{
		public Keys output;
		
		public HKeySelector (int SelectedHotkey, Point location)
		{
			InitializeComponent ();
			this.Location=location;
			this.button1.Enabled=false;
			this.output=Keys.None;
		}
		
		void Button2Click (object sender, EventArgs e)
		{
			this.DialogResult=DialogResult.Cancel;
			this.Close ();
		}
		
		void Button1Click (object sender, EventArgs e)
		{
			this.DialogResult=DialogResult.OK;
			this.Close ();
		}
		
		void HKeySelectorKeyDown (object sender, KeyEventArgs e)
		{
			//verify if at least one modifyer key (ctrl, alt, shift) was pressed
			bool ctrl=(e.Modifiers&Keys.Control)!=0, alt=(e.Modifiers&Keys.Alt)!=0, shift=(e.Modifiers&Keys.Shift)!=0;
			//and verify if at least one non-modifyer key was pressed (eg Ctrl+Alt is not a combination, Ctrl+Alt+R is a valid combination)
			string s=e.KeyData.ToString ();
			bool nonmodifyer=!(s.Contains ("ControlKey")||s.Contains ("Menu")||s.Contains ("ShiftKey"));
			if ((ctrl||alt||shift)&&nonmodifyer)
			{
				this.output=e.KeyData;
				//output.tostring () looks like this: Z, Alt, Ctrl so it is parsed to look like Ctrl+Alt+Z
				this.label2.Text=ParseKey (s);
				this.button1.Enabled=true;
			}
			else
			{
				label2.Text="I've said a combination of keys.";
				this.button1.Enabled=false;
			}
		}
		
		public static string ParseKey (string s)
		{
			string []ss=s.Split (", ".ToCharArray (), StringSplitOptions.RemoveEmptyEntries);
			string r="";
			for (int i=ss.Length-1; i>=1; i--) r+=ss[i]+"+";
			r+=ss[0];
			return r;
		}
	}
}
