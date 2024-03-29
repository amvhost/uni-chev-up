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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace Universal_Chevereto_Uploadr
{
	//"History" form
    public partial class History : Form
    {
        public History (bool All)
        {
            InitializeComponent ();
            PopulateList (All);
            listView1.MultiSelect=false;
            listView1.DoubleClick+=new EventHandler (listView1_DoubleClick);
            Program.IsHistoryFormShowed=true;
            this.ShowInTaskbar=true;
            this.Icon=Properties.Resources.favicon;
            if (All) comboBox1.SelectedItem=comboBox1.Items[0];
            else comboBox1.SelectedItem=comboBox1.Items[1];
            ImageList il=new ImageList ();
            il.Images.Add (Properties.Resources.favicon);
            listView1.LargeImageList=il;
            //this.FormClosing+=delegate {Program.checker.BuildContextMenu ();};
            this.FormClosing+=delegate {Program.IsHistoryFormShowed=false;};
            this.VisibleChanged+=delegate {Program.IsHistoryFormShowed=this.Visible;};
        }

        void listView1_DoubleClick (object sender, EventArgs e)
        {
        	//if I double-click a image from the history...
            if (listView1.SelectedItems.Count==1)
            {
                string []s=listView1.SelectedItems[0].Name.Split (":".ToCharArray ());
                int id=Convert.ToInt32 (s[1]);
                //..get the details about it...
                UploadedPhoto uf=new UploadedPhoto ();
                foreach (UploadedPhoto up in Program.History)
                    if (id==up.Id)
                    {
                        uf=up;
                        break;
                    }
                //...and then upload it
                PhotoViewer pv=new PhotoViewer (uf);
                this.Hide ();
                if (pv.ShowDialog ()==DialogResult.Cancel)
                {
                    button2_Click (null, null);
                    this.Show ();
                }
            }
        }

        public void PopulateList (bool All)
        {
        	//this funtion is used to build the list control
        	//with items representing the photos from
        	//History List <UploadedPhoto> (that one from the Program.cs)
        	listView1.Items.Clear ();
            foreach (UploadedPhoto up in Program.History)
            {
                bool test=true;
                //if "Show photos from last upload" is selected,
                if (!All) test=up.FromLastUpload;
                if (test)
                {
                    ListViewItem lvi=new ListViewItem ();
                    lvi.Text=up.LocalName;
                    lvi.Name="lvi-id:"+up.Id;
                    lvi.ImageIndex=0;
                    listView1.Items.Add (lvi);
                }
            }
        }
        
        void Button1Click (object sender, EventArgs e)
        {
        	//clear history
        	Program.History.Clear ();
        	Program.HistoryForm.PopulateList (true);
        	Program.WriteHistory ();
        }
        
        void ComboBox1SelectedIndexChanged (object sender, EventArgs e)
        {
        	//change from "show all" to "show from last upload" and viceversa
        	if (comboBox1.SelectedIndex==0)
        	  	 PopulateList (true);
        	else PopulateList (false);
        }

        private void listView1_SelectedIndexChanged (object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        	//refresh
            if (comboBox1.SelectedIndex==0) PopulateList (true);
            else PopulateList (false);
        }
    }
}
