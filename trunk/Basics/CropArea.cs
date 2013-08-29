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
