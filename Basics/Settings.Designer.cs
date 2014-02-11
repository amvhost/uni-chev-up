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

namespace Universal_Chevereto_Uploadr
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        	this.checkBox7 = new System.Windows.Forms.CheckBox();
        	this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
        	this.textBox1 = new System.Windows.Forms.TextBox();
        	this.label2 = new System.Windows.Forms.Label();
        	this.label1 = new System.Windows.Forms.Label();
        	this.tabControl1 = new System.Windows.Forms.TabControl();
        	this.tabPage1 = new System.Windows.Forms.TabPage();
        	this.checkBox3 = new System.Windows.Forms.CheckBox();
        	this.checkBox1 = new System.Windows.Forms.CheckBox();
        	this.checkBox2 = new System.Windows.Forms.CheckBox();
        	this.tabPage3 = new System.Windows.Forms.TabPage();
        	this.button1 = new System.Windows.Forms.Button();
        	this.checkBox4 = new System.Windows.Forms.CheckBox();
        	this.comboBox1 = new System.Windows.Forms.ComboBox();
        	this.label4 = new System.Windows.Forms.Label();
        	this.tabPage2 = new System.Windows.Forms.TabPage();
        	this.label3 = new System.Windows.Forms.Label();
        	this.label5 = new System.Windows.Forms.Label();
        	this.linkLabel1 = new System.Windows.Forms.LinkLabel();
        	((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
        	this.tabControl1.SuspendLayout();
        	this.tabPage1.SuspendLayout();
        	this.tabPage3.SuspendLayout();
        	this.tabPage2.SuspendLayout();
        	this.SuspendLayout();
        	// 
        	// checkBox7
        	// 
        	this.checkBox7.AutoSize = true;
        	this.checkBox7.Location = new System.Drawing.Point(3, 15);
        	this.checkBox7.Name = "checkBox7";
        	this.checkBox7.Size = new System.Drawing.Size(322, 17);
        	this.checkBox7.TabIndex = 5;
        	this.checkBox7.Text = "Check this if you connect to the internet through a proxy server";
        	this.checkBox7.UseVisualStyleBackColor = true;
        	this.checkBox7.CheckedChanged += new System.EventHandler(this.checkBox7_CheckedChanged);
        	// 
        	// numericUpDown1
        	// 
        	this.numericUpDown1.Location = new System.Drawing.Point(61, 77);
        	this.numericUpDown1.Maximum = new decimal(new int[] {
        	        	        	10000,
        	        	        	0,
        	        	        	0,
        	        	        	0});
        	this.numericUpDown1.Name = "numericUpDown1";
        	this.numericUpDown1.Size = new System.Drawing.Size(49, 20);
        	this.numericUpDown1.TabIndex = 7;
        	this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
        	// 
        	// textBox1
        	// 
        	this.textBox1.Location = new System.Drawing.Point(61, 51);
        	this.textBox1.Name = "textBox1";
        	this.textBox1.Size = new System.Drawing.Size(99, 20);
        	this.textBox1.TabIndex = 6;
        	this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
        	// 
        	// label2
        	// 
        	this.label2.AutoSize = true;
        	this.label2.Location = new System.Drawing.Point(5, 79);
        	this.label2.Name = "label2";
        	this.label2.Size = new System.Drawing.Size(29, 13);
        	this.label2.TabIndex = 2;
        	this.label2.Text = "Port:";
        	// 
        	// label1
        	// 
        	this.label1.AutoSize = true;
        	this.label1.Location = new System.Drawing.Point(3, 54);
        	this.label1.Name = "label1";
        	this.label1.Size = new System.Drawing.Size(42, 13);
        	this.label1.TabIndex = 1;
        	this.label1.Text = "Adress:";
        	// 
        	// tabControl1
        	// 
        	this.tabControl1.Controls.Add(this.tabPage1);
        	this.tabControl1.Controls.Add(this.tabPage3);
        	this.tabControl1.Controls.Add(this.tabPage2);
        	this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.tabControl1.Location = new System.Drawing.Point(0, 0);
        	this.tabControl1.Name = "tabControl1";
        	this.tabControl1.SelectedIndex = 0;
        	this.tabControl1.Size = new System.Drawing.Size(352, 199);
        	this.tabControl1.TabIndex = 8;
        	// 
        	// tabPage1
        	// 
        	this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
        	this.tabPage1.Controls.Add(this.checkBox3);
        	this.tabPage1.Controls.Add(this.checkBox1);
        	this.tabPage1.Controls.Add(this.checkBox2);
        	this.tabPage1.Location = new System.Drawing.Point(4, 22);
        	this.tabPage1.Name = "tabPage1";
        	this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
        	this.tabPage1.Size = new System.Drawing.Size(344, 173);
        	this.tabPage1.TabIndex = 0;
        	this.tabPage1.Text = "General";
        	// 
        	// checkBox3
        	// 
        	this.checkBox3.AutoSize = true;
        	this.checkBox3.Location = new System.Drawing.Point(6, 64);
        	this.checkBox3.Name = "checkBox3";
        	this.checkBox3.Size = new System.Drawing.Size(111, 17);
        	this.checkBox3.TabIndex = 5;
        	this.checkBox3.Text = "Save screenshots";
        	this.checkBox3.UseVisualStyleBackColor = true;
        	this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged_1);
        	// 
        	// checkBox1
        	// 
        	this.checkBox1.AutoSize = true;
        	this.checkBox1.Location = new System.Drawing.Point(6, 18);
        	this.checkBox1.Name = "checkBox1";
        	this.checkBox1.Size = new System.Drawing.Size(200, 17);
        	this.checkBox1.TabIndex = 0;
        	this.checkBox1.Text = "After upload, copy link(s) to clipboard";
        	this.checkBox1.UseVisualStyleBackColor = true;
        	this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
        	// 
        	// checkBox2
        	// 
        	this.checkBox2.AutoSize = true;
        	this.checkBox2.Location = new System.Drawing.Point(6, 41);
        	this.checkBox2.Name = "checkBox2";
        	this.checkBox2.Size = new System.Drawing.Size(166, 17);
        	this.checkBox2.TabIndex = 4;
        	this.checkBox2.Text = "Run this application at startup";
        	this.checkBox2.UseVisualStyleBackColor = true;
        	this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
        	// 
        	// tabPage3
        	// 
        	this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
        	this.tabPage3.Controls.Add(this.linkLabel1);
        	this.tabPage3.Controls.Add(this.label5);
        	this.tabPage3.Controls.Add(this.button1);
        	this.tabPage3.Controls.Add(this.checkBox4);
        	this.tabPage3.Controls.Add(this.comboBox1);
        	this.tabPage3.Controls.Add(this.label4);
        	this.tabPage3.Location = new System.Drawing.Point(4, 22);
        	this.tabPage3.Name = "tabPage3";
        	this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
        	this.tabPage3.Size = new System.Drawing.Size(344, 173);
        	this.tabPage3.TabIndex = 2;
        	this.tabPage3.Text = "Hotkeys";
        	// 
        	// button1
        	// 
        	this.button1.Location = new System.Drawing.Point(8, 91);
        	this.button1.Name = "button1";
        	this.button1.Size = new System.Drawing.Size(193, 23);
        	this.button1.TabIndex = 4;
        	this.button1.Text = "(Press to assign hotkey)";
        	this.button1.UseVisualStyleBackColor = true;
        	this.button1.Click += new System.EventHandler(this.Button1Click);
        	// 
        	// checkBox4
        	// 
        	this.checkBox4.Location = new System.Drawing.Point(8, 61);
        	this.checkBox4.Name = "checkBox4";
        	this.checkBox4.Size = new System.Drawing.Size(104, 24);
        	this.checkBox4.TabIndex = 2;
        	this.checkBox4.Text = "Hotkey on";
        	this.checkBox4.UseVisualStyleBackColor = true;
        	this.checkBox4.CheckedChanged += new System.EventHandler(this.CheckBox4CheckedChanged);
        	// 
        	// comboBox1
        	// 
        	this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.comboBox1.FormattingEnabled = true;
        	this.comboBox1.Location = new System.Drawing.Point(8, 34);
        	this.comboBox1.Name = "comboBox1";
        	this.comboBox1.Size = new System.Drawing.Size(193, 21);
        	this.comboBox1.TabIndex = 1;
        	this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1SelectedIndexChanged);
        	// 
        	// label4
        	// 
        	this.label4.Location = new System.Drawing.Point(8, 13);
        	this.label4.Name = "label4";
        	this.label4.Size = new System.Drawing.Size(88, 18);
        	this.label4.TabIndex = 0;
        	this.label4.Text = "App function:";
        	// 
        	// tabPage2
        	// 
        	this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
        	this.tabPage2.Controls.Add(this.label3);
        	this.tabPage2.Controls.Add(this.checkBox7);
        	this.tabPage2.Controls.Add(this.numericUpDown1);
        	this.tabPage2.Controls.Add(this.label1);
        	this.tabPage2.Controls.Add(this.textBox1);
        	this.tabPage2.Controls.Add(this.label2);
        	this.tabPage2.Location = new System.Drawing.Point(4, 22);
        	this.tabPage2.Name = "tabPage2";
        	this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
        	this.tabPage2.Size = new System.Drawing.Size(344, 173);
        	this.tabPage2.TabIndex = 1;
        	this.tabPage2.Text = "Proxy";
        	// 
        	// label3
        	// 
        	this.label3.AutoSize = true;
        	this.label3.Location = new System.Drawing.Point(3, 35);
        	this.label3.Name = "label3";
        	this.label3.Size = new System.Drawing.Size(109, 13);
        	this.label3.TabIndex = 8;
        	this.label3.Text = "Set here server\'s info:";
        	// 
        	// label5
        	// 
        	this.label5.Location = new System.Drawing.Point(8, 139);
        	this.label5.Name = "label5";
        	this.label5.Size = new System.Drawing.Size(328, 29);
        	this.label5.TabIndex = 5;
        	this.label5.Text = "In order to apply any changes made to theese settings, the program needs to be re" +
        	"started. Click          to restart uni-chev-up";
        	// 
        	// linkLabel1
        	// 
        	this.linkLabel1.Location = new System.Drawing.Point(139, 152);
        	this.linkLabel1.Name = "linkLabel1";
        	this.linkLabel1.Size = new System.Drawing.Size(28, 15);
        	this.linkLabel1.TabIndex = 6;
        	this.linkLabel1.TabStop = true;
        	this.linkLabel1.Text = "here";
        	this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1LinkClicked);
        	// 
        	// Settings
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.ClientSize = new System.Drawing.Size(352, 199);
        	this.Controls.Add(this.tabControl1);
        	this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
        	this.Name = "Settings";
        	this.Text = "Settings";
        	((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
        	this.tabControl1.ResumeLayout(false);
        	this.tabPage1.ResumeLayout(false);
        	this.tabPage1.PerformLayout();
        	this.tabPage3.ResumeLayout(false);
        	this.tabPage2.ResumeLayout(false);
        	this.tabPage2.PerformLayout();
        	this.ResumeLayout(false);
        }
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TabPage tabPage3;

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox3;
    }
}
