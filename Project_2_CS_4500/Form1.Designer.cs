using System.Windows.Forms;
using System.Xml.Linq;

namespace Project_2_CS_4500
{
    partial class Project2
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Project2));
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            tBoxMsg = new TextBox();
            tBox1 = new TextBox();
            bSpades = new Button();
            bDiamonds = new Button();
            bHearts = new Button();
            bClubs = new Button();
            b2 = new Button();
            b9 = new Button();
            b8 = new Button();
            b7 = new Button();
            b6 = new Button();
            b5 = new Button();
            b4 = new Button();
            b3 = new Button();
            b10 = new Button();
            bAce = new Button();
            bKing = new Button();
            bQueen = new Button();
            bJack = new Button();
            bChoose = new Button();
            panel1 = new Panel();
            button0 = new Button();
            bNewHand = new Button();
            tBoxRecord = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(7, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(198, 320);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(211, 12);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(198, 320);
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Location = new Point(415, 12);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(198, 320);
            pictureBox3.TabIndex = 2;
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox3_Click;
            // 
            // pictureBox4
            // 
            pictureBox4.Location = new Point(619, 12);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(198, 320);
            pictureBox4.TabIndex = 3;
            pictureBox4.TabStop = false;
            // 
            // tBoxMsg
            // 
            tBoxMsg.Location = new Point(5, 338);
            tBoxMsg.Multiline = true;
            tBoxMsg.Name = "tBoxMsg";
            tBoxMsg.ReadOnly = true;
            tBoxMsg.Size = new Size(530, 34);
            tBoxMsg.TabIndex = 24;
            // 
            // tBox1
            // 
            tBox1.Location = new Point(5, 382);
            tBox1.Multiline = true;
            tBox1.Name = "tBox1";
            tBox1.ReadOnly = true;
            tBox1.Size = new Size(530, 32);
            tBox1.TabIndex = 25;
            // 
            // bSpades
            // 
            bSpades.Location = new Point(13, 28);
            bSpades.Name = "bSpades";
            bSpades.Size = new Size(80, 48);
            bSpades.TabIndex = 6;
            bSpades.Text = "Spades";
            bSpades.UseVisualStyleBackColor = true;
            bSpades.Click += bSpades_Click;
            // 
            // bDiamonds
            // 
            bDiamonds.Location = new Point(271, 28);
            bDiamonds.Name = "bDiamonds";
            bDiamonds.Size = new Size(80, 48);
            bDiamonds.TabIndex = 7;
            bDiamonds.Text = "Diamonds";
            bDiamonds.UseVisualStyleBackColor = true;
            bDiamonds.Click += bDiamonds_Click;
            // 
            // bHearts
            // 
            bHearts.Location = new Point(185, 28);
            bHearts.Name = "bHearts";
            bHearts.Size = new Size(80, 48);
            bHearts.TabIndex = 8;
            bHearts.Text = "Hearts";
            bHearts.UseVisualStyleBackColor = true;
            bHearts.Click += bHearts_Click;
            // 
            // bClubs
            // 
            bClubs.Location = new Point(99, 28);
            bClubs.Name = "bClubs";
            bClubs.Size = new Size(80, 48);
            bClubs.TabIndex = 9;
            bClubs.Text = "Clubs";
            bClubs.UseVisualStyleBackColor = true;
            bClubs.Click += bClubs_Click;
            // 
            // b2
            // 
            b2.Location = new Point(5, 101);
            b2.Name = "b2";
            b2.Size = new Size(38, 34);
            b2.TabIndex = 10;
            b2.Text = "2";
            b2.UseVisualStyleBackColor = true;
            b2.Click += b2_Click;
            // 
            // b9
            // 
            b9.Location = new Point(313, 101);
            b9.Name = "b9";
            b9.Size = new Size(38, 34);
            b9.TabIndex = 11;
            b9.Text = "9";
            b9.UseVisualStyleBackColor = true;
            b9.Click += b9_Click;
            // 
            // b8
            // 
            b8.Location = new Point(269, 101);
            b8.Name = "b8";
            b8.Size = new Size(38, 34);
            b8.TabIndex = 12;
            b8.Text = "8";
            b8.UseVisualStyleBackColor = true;
            b8.Click += b8_Click;
            // 
            // b7
            // 
            b7.Location = new Point(225, 101);
            b7.Name = "b7";
            b7.Size = new Size(38, 34);
            b7.TabIndex = 13;
            b7.Text = "7";
            b7.UseVisualStyleBackColor = true;
            b7.Click += b7_Click;
            // 
            // b6
            // 
            b6.Location = new Point(181, 101);
            b6.Name = "b6";
            b6.Size = new Size(38, 34);
            b6.TabIndex = 14;
            b6.Text = "6";
            b6.UseVisualStyleBackColor = true;
            b6.Click += b6_Click;
            // 
            // b5
            // 
            b5.Location = new Point(137, 101);
            b5.Name = "b5";
            b5.Size = new Size(38, 34);
            b5.TabIndex = 15;
            b5.Text = "5";
            b5.UseVisualStyleBackColor = true;
            b5.Click += b5_Click;
            // 
            // b4
            // 
            b4.Location = new Point(93, 101);
            b4.Name = "b4";
            b4.Size = new Size(38, 34);
            b4.TabIndex = 16;
            b4.Text = "4";
            b4.UseVisualStyleBackColor = true;
            b4.Click += b4_Click;
            // 
            // b3
            // 
            b3.Location = new Point(49, 101);
            b3.Name = "b3";
            b3.Size = new Size(38, 34);
            b3.TabIndex = 17;
            b3.Text = "3";
            b3.UseVisualStyleBackColor = true;
            b3.Click += b3_Click;
            // 
            // b10
            // 
            b10.Location = new Point(357, 101);
            b10.Name = "b10";
            b10.Size = new Size(47, 34);
            b10.TabIndex = 18;
            b10.Text = "10";
            b10.UseVisualStyleBackColor = true;
            b10.Click += b10_Click;
            // 
            // bAce
            // 
            bAce.Location = new Point(245, 141);
            bAce.Name = "bAce";
            bAce.Size = new Size(38, 34);
            bAce.TabIndex = 19;
            bAce.Text = "A";
            bAce.UseVisualStyleBackColor = true;
            bAce.Click += bAce_Click;
            // 
            // bKing
            // 
            bKing.Location = new Point(201, 141);
            bKing.Name = "bKing";
            bKing.Size = new Size(38, 34);
            bKing.TabIndex = 20;
            bKing.Text = "K";
            bKing.UseVisualStyleBackColor = true;
            bKing.Click += bKing_Click;
            // 
            // bQueen
            // 
            bQueen.Location = new Point(155, 141);
            bQueen.Name = "bQueen";
            bQueen.Size = new Size(38, 34);
            bQueen.TabIndex = 21;
            bQueen.Text = "Q";
            bQueen.UseVisualStyleBackColor = true;
            bQueen.Click += bQueen_Click;
            // 
            // bJack
            // 
            bJack.Location = new Point(111, 141);
            bJack.Name = "bJack";
            bJack.Size = new Size(38, 34);
            bJack.TabIndex = 22;
            bJack.Text = "J";
            bJack.UseVisualStyleBackColor = true;
            bJack.Click += bJack_Click;
            // 
            // bChoose
            // 
            bChoose.Location = new Point(410, 19);
            bChoose.Name = "bChoose";
            bChoose.Size = new Size(111, 66);
            bChoose.TabIndex = 23;
            bChoose.Text = "Choose";
            bChoose.UseVisualStyleBackColor = true;
            bChoose.Click += bChoose_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(button0);
            panel1.Controls.Add(bChoose);
            panel1.Controls.Add(bJack);
            panel1.Controls.Add(bQueen);
            panel1.Controls.Add(bKing);
            panel1.Controls.Add(bAce);
            panel1.Controls.Add(b10);
            panel1.Controls.Add(b3);
            panel1.Controls.Add(b4);
            panel1.Controls.Add(b5);
            panel1.Controls.Add(b6);
            panel1.Controls.Add(b7);
            panel1.Controls.Add(b8);
            panel1.Controls.Add(b9);
            panel1.Controls.Add(b2);
            panel1.Controls.Add(bClubs);
            panel1.Controls.Add(bHearts);
            panel1.Controls.Add(bDiamonds);
            panel1.Controls.Add(bSpades);
            panel1.Location = new Point(5, 420);
            panel1.Name = "panel1";
            panel1.Size = new Size(530, 197);
            panel1.TabIndex = 26;
            panel1.Paint += panel1_Paint;
            // 
            // button0
            // 
            button0.Location = new Point(0, 0);
            button0.Name = "button0";
            button0.Size = new Size(75, 23);
            button0.TabIndex = 0;
            // 
            // bNewHand
            // 
            bNewHand.Location = new Point(550, 569);
            bNewHand.Name = "bNewHand";
            bNewHand.Size = new Size(269, 48);
            bNewHand.TabIndex = 27;
            bNewHand.Text = "New Hand";
            bNewHand.UseVisualStyleBackColor = true;
            bNewHand.Click += bNewHand_Click;
            // 
            // tBoxRecord
            // 
            tBoxRecord.Location = new Point(541, 342);
            tBoxRecord.Multiline = true;
            tBoxRecord.Name = "tBoxRecord";
            tBoxRecord.ReadOnly = true;
            tBoxRecord.ScrollBars = ScrollBars.Vertical;
            tBoxRecord.Size = new Size(278, 221);
            tBoxRecord.TabIndex = 28;
            tBoxRecord.TextChanged += tBoxRecord_TextChanged;
            // 
            // Project2
            // 
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(827, 645);
            Controls.Add(tBoxRecord);
            Controls.Add(bNewHand);
            Controls.Add(panel1);
            Controls.Add(tBox1);
            Controls.Add(tBoxMsg);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Name = "Project2";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private TextBox tBoxMsg;
        private TextBox tBox1;
        private Button bSpades;
        private Button bDiamonds;
        private Button bHearts;
        private Button bClubs;
        private Button b2;
        private Button b9;
        private Button b8;
        private Button b7;
        private Button b6;
        private Button b5;
        private Button b4;
        private Button b3;
        private Button b10;
        private Button bAce;
        private Button bKing;
        private Button bQueen;
        private Button bJack;
        private Button bChoose;
        private Panel panel1;
        private Button bNewHand;
        private TextBox tBoxRecord;
        private Button button0;
    }
}