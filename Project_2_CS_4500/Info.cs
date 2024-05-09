//Info Window which opens upon startup.
//Author: Jonny Stadter


using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW_4_CS_4500
{
    public partial class Info : Form
    {
        string filepath = "./LastWon.txt";
        public Info()
        {
            InitializeComponent();
            bRestart.Visible = false;
            bResume.Visible = false;
            tBoxInfo.Visible = false;
            bStart.Visible = false;
            Label label1 = new Label();
           
            

            if (File.Exists(filepath))
            {
                string p = "0";
                
                StreamReader sr = new StreamReader(filepath);
                try
                {
                    p = sr.ReadLine();
                  
                    if (p != "0")
                    {
                        bRestart.Visible = true;
                        bResume.Visible = true;
                        tBoxInfo.Visible = true;
                        tBoxInfo.Text = "You have made progress in a prior session: Last pattern solved: " + p + ". Resume, or restart?";
                    }
                    else
                        bStart.Visible = true;
                }
                //If something goes wrong with the read, we will just start from scratch    
                catch
                {
                    
                    this.Close();
                }
                sr.Close();                
            }
            else
            {
                File.Create(filepath).Close();
                bStart.Visible = true;
            }
        }
        private void label1_Click()
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void bRestart_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(filepath, false);
            sw.Write("0");
            sw.Close();
            this.Close();
            
        }

        private void bResume_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}