using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Media;
using System.Runtime.Serialization.Formatters.Binary;


namespace Cricket_Game_client_
{
    public partial class PlayGame : Form
    {
        static string CurrentDirectory = Directory.GetCurrentDirectory();
        static string ClickSoundPath = CurrentDirectory + @"\Click.wav";
        static string ClapSoundPath = CurrentDirectory + @"\Clap.wav";

        static SoundPlayer ClickSound = new SoundPlayer(ClickSoundPath);
        static SoundPlayer ClapSound = new SoundPlayer(ClapSoundPath);

        private TcpClient c;
        private string playing, oponent;
        int ply;
        int _out = 0;
        int score = 0;
      
       
        public PlayGame(TcpClient c, string playing)
        {
            InitializeComponent();
            this.c = c;
            this.playing = playing;
            CheckForIllegalCrossThreadCalls = false;

        }
        public PlayGame()
        {
            InitializeComponent();

        }
        static object s;
        static EventArgs _e;

        private void PlayGame_Load(object sender, EventArgs e)
        {
            


            panel1.Hide();
            lb_tar.Hide();
            pic1.Image = null;
            pic2.Image = null;
            s = sender;
            _e = e;
            lb_bating.Text = "Pakistan";
            lb_bowling.Text = "India";
            lb_score.Text = score.ToString();
            lb_out.Text = _out.ToString();
            if (playing == "0")
            {
                label1.Text = "Bowling";
                label5.Text = "Batting";
                oponent = "1";
                DisableControls();

                new Thread(() => Read()).Start();

            }
            else
            {

                label1.Text = "Batting";
                label5.Text = "Bowling";
                oponent = "0";
            }
        }


        string msg;
        private void Read()
        {

           

                if (playing == "0" && msg!=null)
                {
                    RemovePics();
                }
            BinaryFormatter fobj = new BinaryFormatter();
            string d = (string)fobj.Deserialize(c.GetStream());
            string[] response = d.Split(',');
            if(response[0].Equals("win") || response[0].Equals("draw") || response[0].Equals("lose")) {
                pnl_target_lb.Hide();
                label6.Hide();
                if (response[0].Equals("win"))
                {

                    panel1.BackgroundImage = Properties.Resources.Winner;
                    panel1.Show();
                    panel1.BackgroundImage = Properties.Resources.Winner;
                    Thread.Sleep(4000);
                    panel1.Hide();
                    
                }
                else if (response[0].Equals("draw"))
                {

                   
                    panel1.Show();
                    panel1.BackgroundImage = Properties.Resources.Draw;
                    Thread.Sleep(4000);
                    panel1.Hide();
                   
                }
                else {
                    panel1.Show();
                    panel1.BackgroundImage = Properties.Resources.Loose;
                    Thread.Sleep(4000);
                    panel1.Hide();
                    
                }



            }
           
            else if (response[0] != "0" && response[0] != "1")
            {

                lb_score.Text = response[1].ToString();
                lb_out.Text = response[2].ToString();
                msg = response[0].ToString();
            }
            else
            {

                playing = response[0].ToString();
                lb_out.Text = response[2].ToString();
                lb_score.Text = response[1].ToString();
                
               // msg = null;

                _out = 0;
                score = 0;
                PlayGame_Load(s, _e);
                lb_tar.Show();
                
                lb_bating.Text = "India";
                lb_bowling.Text = "Pakistan";
                lb_tar.Text =response[3].ToString();
                pnl_target_lb.Text = lb_tar.Text;
                panel1.BackgroundImage = Properties.Resources.MAtch_Pause__1_;
                panel1.Show();
                Thread.Sleep(4000);
                panel1.Hide();

               

            }
                   
                


           
               
           
           

            if (playing == "1" )
            {
                ShowPlayerShort();
                Thread.Sleep(1000);
                pic1.Image = null;
                pic2.Image = null;
            }

            
            if (lb_out.Text != "5")
            {
                EnableControls();
            }
          
        }

        private void ShowPlayerShort()
        {
            if (msg.Equals("one"))
            {
                ply = 1;
                pic2.Image = Properties.Resources._1c;
            }
            else if (msg.Equals("two"))
            {
                ply = 2;
                pic2.Image = Properties.Resources._2c;
            }
            else if (msg.Equals("three"))
            {
                ply = 3;
                pic2.Image = Properties.Resources._31;
            }
            else if (msg.Equals("four"))
            {
                ply = 4;
                pic2.Image = Properties.Resources._4c;
            }
            else if (msg.Equals("six"))
            {
                ply = 6;
                pic2.Image = Properties.Resources._6c;
            }
            else
            {
                ply = 0;
                pic2.Image = Properties.Resources.sc;
            }
        }
        #region Enable/Disable Control
        private void EnableControls()
        {
            one.Enabled = true;
            two.Enabled = true;
            three.Enabled = true;
            four.Enabled = true;
            six.Enabled = true;
            dot.Enabled = true;
        }
        private void DisableControls()
        {
            one.Enabled = false;
            two.Enabled = false;
            three.Enabled = false;
            four.Enabled = false;
            six.Enabled = false;
            dot.Enabled = false;

        }
        #endregion
        private void checkstatus(int a)
        {
            

            if (playing == "0")
            {
                if (ply == a) {
                    ClapSound.Play();
                    _out++; }
                else {
                    if (ply == 4 || ply == 6) { ClapSound.Play(); }
                    score += ply; }
                lb_score.Text = score.ToString();
                lb_out.Text = _out.ToString();


            }



        }
        private void RemovePics()
        {

            Thread.Sleep(1000);

            pic1.Image = null;
            pic2.Image = null;



        }

        #region Buttons
        private void one_Click(object sender, EventArgs e)
        {
            ClickSound.Play();
            if (playing == "0" && msg!=null)
            {
                ShowPlayerShort();

            }

            BinaryFormatter fobj;
            checkstatus(1);
            fobj = new BinaryFormatter();
            string details = "one" + "," + lb_score.Text + "," + lb_out.Text;
            fobj.Serialize(c.GetStream(), details);
            pic1.Image = Properties.Resources._1c;
            DisableControls();


            new Thread(() => Read()).Start();


        }

        private void two_Click(object sender, EventArgs e)
        {
            ClickSound.Play();
            if (playing == "0" && msg!= null)
            {
                ShowPlayerShort();
            }

            BinaryFormatter fobj;
            checkstatus(2);
            fobj = new BinaryFormatter();
            string details = "two" + "," + lb_score.Text + "," + lb_out.Text;
            fobj.Serialize(c.GetStream(), details);
            pic1.Image = Properties.Resources._2c;
            DisableControls();


            new Thread(() => Read()).Start();
        }

        private void three_Click(object sender, EventArgs e)
        {
            ClickSound.Play();
            if (playing == "0"&& msg!= null)
            {
                ShowPlayerShort();
            }

            BinaryFormatter fobj;
            checkstatus(3);
            fobj = new BinaryFormatter();
            string details = "three" + "," + lb_score.Text + "," + lb_out.Text;
            fobj.Serialize(c.GetStream(), details);
            pic1.Image = Properties.Resources._31;
            DisableControls();


            new Thread(() => Read()).Start();
        }

        private void four_Click(object sender, EventArgs e)
        {
            ClickSound.Play();
            if (playing == "0" && msg!= null)
            {
                ShowPlayerShort();
            }

            BinaryFormatter fobj;
            checkstatus(4);
            fobj = new BinaryFormatter();
            string details = "four" + "," + lb_score.Text + "," + lb_out.Text;
            fobj.Serialize(c.GetStream(), details);
            pic1.Image = Properties.Resources._4c;
            DisableControls();

            new Thread(() => Read()).Start();
        }

        private void six_Click(object sender, EventArgs e)
        {
            ClickSound.Play();
            if (playing == "0" && msg != null)
            {
                ShowPlayerShort();
            }

            BinaryFormatter fobj;
            checkstatus(6);
            fobj = new BinaryFormatter();
            string details = "six" + "," + lb_score.Text + "," + lb_out.Text;
            fobj.Serialize(c.GetStream(), details);
            pic1.Image = Properties.Resources._6c;
            DisableControls();


            new Thread(() => Read()).Start();
        }

        private void lb_out_Click(object sender, EventArgs e)
        {

        }

        private void dot_Click(object sender, EventArgs e)
        {
            ClickSound.Play();
            if (playing == "0" && msg != null)
            {
                ShowPlayerShort();
            }

            BinaryFormatter fobj;
            checkstatus(0);
            fobj = new BinaryFormatter();
            string details = "dot" + "," + lb_score.Text + "," + lb_out.Text;
            fobj.Serialize(c.GetStream(), details);
            pic1.Image = Properties.Resources.sc;
            DisableControls();


            new Thread(() => Read()).Start();
        }

        #endregion

        #region animation
        private void picof1_MouseEnter(object sender, EventArgs e)
        {

            one.Visible = true;
        }
        private void picof2_MouseEnter(object sender, EventArgs e)
        {
            two.Visible = true;
        }

        private void picof3_MouseEnter(object sender, EventArgs e)
        {
            three.Visible = true;
        }

        private void picofdot_MouseEnter(object sender, EventArgs e)
        {
            dot.Visible = true;
        }

        private void picof4_MouseEnter(object sender, EventArgs e)
        {
            four.Visible = true;
        }

        private void picof6_MouseEnter(object sender, EventArgs e)
        {
            six.Visible = true;
        }
        private void one_MouseLeave(object sender, EventArgs e)
        {
            one.Visible = false;
        }
        private void two_MouseLeave(object sender, EventArgs e)
        {
            two.Visible = false;
        }

        private void three_MouseLeave(object sender, EventArgs e)
        {
            three.Visible = false;
        }

        private void dot_MouseLeave(object sender, EventArgs e)
        {
            dot.Visible = false;
        }

        private void four_MouseLeave(object sender, EventArgs e)
        {
            four.Visible = false;
        }

        private void six_MouseLeave(object sender, EventArgs e)
        {
            six.Visible = false;
        }

        private void Bumps()
        {

            one.Visible = false;
            two.Visible = false;
            three.Visible = false;
            four.Visible = false;
            dot.Visible = false;
            six.Visible = false;
        }
        #endregion
    }
}
