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
using System.Windows.Forms;
using Microsoft.Win32;
using System.Media;

namespace Universal_Chevereto_Uploadr
{
	//class dealing with app's settings
    public partial class Settings : Form
    {
    	private int SelectedHotkey;
    	
        public Settings ()
        {
            InitializeComponent ();
            this.ShowInTaskbar=true;
            this.Icon=Properties.Resources.favicon;
            checkBox1.Checked=Sets.CopyAfterUpload;
            checkBox2.Checked=Sets.StartOnStartup;
            checkBox7.Checked=Sets.ProxyOn;
            label1.Enabled=Sets.ProxyOn;
            label2.Enabled=Sets.ProxyOn;
            textBox1.Enabled=Sets.ProxyOn;
            numericUpDown1.Enabled=Sets.ProxyOn;
            textBox1.Text=Sets.ProxyServer;
            checkBox3.Checked=Sets.SaveScreenshots;
            for (int i=0; i<Program.AppFunctionalities.Count; i++)
            	comboBox1.Items.Add (Program.AppFunctionalities[i].Key);
            comboBox1.Text=Program.AppFunctionalities[0].Key;
            SelectedHotkey=0;
            RefreshHotKeySettingsPanel ();
            this.FormClosing+=delegate {Program.checker.BuildContextMenu ();};
            try
            {
                numericUpDown1.Value=Convert.ToInt32 (Sets.ProxyPort);
            }
            catch
            {
                numericUpDown1.Value=0;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Sets.CopyAfterUpload=checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged (object sender, EventArgs e)
        {
        	//run @ startup
            Sets.StartOnStartup=checkBox2.Checked;
            RegistryKey rk=Registry.CurrentUser.OpenSubKey ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            try
            {
                if (Sets.StartOnStartup) rk.SetValue ("UniversalCheveretoUploadr", Application.ExecutablePath.ToString ());
                else rk.DeleteValue ("UniversalCheveretoUploadr", true);
            }
            catch 
            {
                MessageBox.Show ("Unexpected error."); 
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            Sets.ProxyOn=checkBox7.Checked;
            label1.Enabled=Sets.ProxyOn;
            label2.Enabled=Sets.ProxyOn;
            textBox1.Enabled=Sets.ProxyOn;
            numericUpDown1.Enabled=Sets.ProxyOn;
        }

        private void textBox1_TextChanged (object sender, EventArgs e)
        {
            Sets.ProxyServer=textBox1.Text;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Sets.ProxyPort=numericUpDown1.Value.ToString ();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged_1 (object sender, EventArgs e)
        {
            Sets.SaveScreenshots=checkBox3.Checked;
        }
        
        private void RefreshHotKeySettingsPanel ()
        {
        	HotkeyData hkd=(HotkeyData)Hotkeys.hHotkeys[SelectedHotkey];
        	if (hkd.IsActive)
        	{
        		checkBox4.Checked=true;
        		button1.Visible=true;
        		if (hkd.Hotkey==Keys.None) button1.Text="(Press to assign hotkey)";
        		else button1.Text=HKeySelector.ParseKey (hkd.Hotkey.ToString ());
        	}
        	else
        	{
        		checkBox4.Checked=false;
        		button1.Visible=false;
        	}
        }
        
        void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
        {
        	for (int i=0; i<Program.AppFunctionalities.Count; i++)
        	{
        		if (Program.AppFunctionalities[i].Key==comboBox1.Text)
        		{
        			SelectedHotkey=i;
        			break;
        		}
        	}
        	RefreshHotKeySettingsPanel ();
        }
        
        void CheckBox4CheckedChanged(object sender, EventArgs e)
        {
        	if (checkBox4.Checked)
        	{
        		HotkeyData hkd=(HotkeyData)Hotkeys.hHotkeys[SelectedHotkey];
        		hkd.IsActive=true;
        		Hotkeys.hHotkeys[SelectedHotkey]=(object)hkd;
				button1.Visible=true;
        	}
        	else
        	{
        		HotkeyData hkd=(HotkeyData)Hotkeys.hHotkeys[SelectedHotkey];
        		hkd.IsActive=false;
        		hkd.Hotkey=Keys.None;
        		Hotkeys.hHotkeys[SelectedHotkey]=(object)hkd;
        		button1.Visible=false;
        	}
        	Hotkeys.WriteHotkeySetting (SelectedHotkey);
        }
        
        void Button1Click (object sender, EventArgs e)
        {
        	HKeySelector hks=new HKeySelector (SelectedHotkey, this.Location);
        	if (hks.ShowDialog ()==DialogResult.OK)
        	{
        		button1.Text=HKeySelector.ParseKey (hks.output.ToString ());
        		HotkeyData hkd=(HotkeyData)Hotkeys.hHotkeys[SelectedHotkey];
        		hkd.Hotkey=hks.output;
        		Hotkeys.WriteHotkeySetting (SelectedHotkey);
        	}
        }
        
        void LinkLabel1LinkClicked (object sender, LinkLabelLinkClickedEventArgs e)
        {
        	Program.ApplicationRestart ();
        }
    }
}
