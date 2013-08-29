using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;

namespace Universal_Chevereto_Uploadr
{
	//about
    partial class AboutBox : Form
    {
        public AboutBox ()
        {
            InitializeComponent ();
            this.FormClosing+=delegate {Program.checker.BuildContextMenu ();};
        }
    }
}
