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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Universal_Chevereto_Uploadr
{
	//class used to select a region from the screen in order to crop the screenshot according to that region
    public partial class CropArea : Form
    {
        public Rectangle Output;

        public CropArea ()
        {
            InitializeComponent ();
            //make the form transparent
            this.BackColor=Color.Pink;
            this.TransparencyKey=Color.Pink;
            this.ShowInTaskbar=true;
            this.Icon=Properties.Resources.favicon;
            this.FormClosing+=delegate
            {
                this.DialogResult=DialogResult.OK;
                //set the output rectangle
                Point p=this.PointToScreen (new Point (this.ClientRectangle.X, this.ClientRectangle.Y));
                Size sz=new Size (this.ClientRectangle.Width, this.ClientRectangle.Height);
                Output=new Rectangle (p, sz);
            };
        }
    }
}
