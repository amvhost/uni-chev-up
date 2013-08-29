using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Net;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using System.Media;

namespace Universal_Chevereto_Uploadr
{
    public partial class PhotoViewer : Form
    {
    	//q=the photo which this form has to treat
    	UploadedPhoto q;
    	
        public PhotoViewer (UploadedPhoto p)
        {
        	q=p;
            InitializeComponent ();
            Thread t=new Thread ((ThreadStart)delegate
            {
                this.Invoke ((MethodInvoker)delegate
                {
                    //show the photo inside a browser (IE based WebBrowsed control was used)
                    //by navigating to its DirectLink URL
                    webBrowser1.Navigate (p.DirectLink);
                });
            });
            //delay a moment before the thread which shows the picture runs,
            //just to let the form's control to be initialized
            //(if I was running the thread without a delay, it would raised an object not set
            //exception <<refering to the webBrowser1 object>>, so I need for this function
            //to finish before shoing the picture)
            System.Windows.Forms.Timer te=new System.Windows.Forms.Timer ();
            te.Interval=10;
            te.Tick+=delegate
            {
                t.Start ();
                te.Stop ();
            };
            te.Start ();
            //initializing
            this.ShowInTaskbar=true;
            this.Icon=Properties.Resources.favicon;
            this.Text=q.LocalName+" - Photo viewer";
            textBox1.Text=q.DirectLink;
			textBox2.Text=q.ShortUrl;
			textBox3.Text=q.Viewer;
			textBox4.Text="<img src=\""+q.DirectLink+"\" alt=\""+q.ServerName+"\" border=\"0\" />";
			textBox5.Text="[img]"+q.DirectLink+"[/img]";
			textBox8.Text=q.Miniatura;
			textBox7.Text="<a href=\""+q.ShortUrl+"\"><img src=\""+q.Miniatura+"\" alt=\""+q.ServerName+"\" border=\"0\" /></a>";
			textBox6.Text="[url="+q.ShortUrl+"][img]"+q.Miniatura+"[/img][/url]";
			label2.Text="Local name: "+q.LocalName;
			label3.Text="Server name: "+q.ServerName;
        }

        private void groupBox1_Enter (object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked (object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
        
        void PhotoViewer_MouseEnter (object sender, EventArgs e)
        {
        }
        
        void Button3Click (object sender, EventArgs e)
        {
        	//if "delete from history" is clicked,
            this.button3.Enabled=true;
            DeleteFromHistory ();
            MessageBox.Show ("Picture deleted from history.");
            this.Close ();
        }

        void DeleteFromHistory ()
        {
        	foreach (UploadedPhoto k in Program.History)
        	if (k.Id==q.Id)
        	{
                Program.History.Remove (k);
	        	break;
            }
        	Program.WriteHistory ();
            this.DialogResult=DialogResult.Cancel;
            this.Close ();
        }
        
        void Button1Click(object sender, EventArgs e)
        {
        	//save the photo to a local disk
            this.button1.Enabled=true;
        	WebClient Client=new WebClient ();
        	SaveFileDialog s=new SaveFileDialog ();
            s.Filter="Images (*.jpg, *.png, *.bmp, *.gif)|*.jpg; *.png; *.bmp; *.gif";
        	if (s.ShowDialog ()==DialogResult.OK)
        	{
        		//download the photo from the website
            	Client.DownloadFile (q.DirectLink, s.FileName);
            	string []w=s.FileName.Split ("\\".ToCharArray ());
            	string path="";
            	for (int i=0; i<w.Length-1; i++) path+=w[i]+"\\";
            	Process.Start (path);
        	}
            this.button1.Enabled=true;
        }

        private void button2_Click (object sender, EventArgs e)
        {
        	//if "delete from server" is clicked,
            this.button2.Enabled=false;
            Thread.Sleep (10);
            string response="";
            Thread t=new Thread ((ThreadStart)delegate
            {
                //make the request to the photo's delete url
                response=Request.Post (q.Delete, "", "null", null);
                //verify if 200 status code was returned
                string []p=response.Split (new string [] {"\"status_code\":", ","}, StringSplitOptions.None);
                if (Convert.ToInt32 (p[1])!=200) 
                {
                	//something's wrong...
                    new Thread ((ThreadStart)delegate {MessageBox.Show ("Error. Status code: "+p[1]);}).Start ();
                    return;
                }
            });
            t.Start ();
            var te=new System.Windows.Forms.Timer ();
            te.Interval=10;
            te.Tick+=delegate
            {
                if (t.IsAlive==false)
                {
                    te.Stop ();
                    //after I delete the photo from server, I delete it from the local History "database"
                    DeleteFromHistory ();
                    MessageBox.Show ("Picture deleted.");
                    this.Invoke ((MethodInvoker)delegate {this.Close ();});
                }
            };
            te.Start ();
        }

        private void label1_Click (object sender, EventArgs e)
        {
        }
        
        void TextBox1TextChanged(object sender, EventArgs e)
        {
        	
        }
        
        void LinkLabel1LinkClicked (object sender, LinkLabelLinkClickedEventArgs e)
        {
        	
        }

        private void button4_Click (object sender, EventArgs e)
        {
        	//if "view on browser" is clicked,
            try
            {
                Process.Start (q.Viewer);
            }
            catch 
            {
                MessageBox.Show ("A unexpected error occured.");
            }
        }
    }
}
