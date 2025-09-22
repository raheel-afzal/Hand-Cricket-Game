using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Cricket_Game_client_
{
    public partial class Form1 : Form
    {
        static string CurrentDirectory = Directory.GetCurrentDirectory();
        static string SoundPath = CurrentDirectory + @"\Play.wav";
        TcpClient c = new TcpClient();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnplay_Click(object sender, EventArgs e)
        {

            SoundPlayer Sound = new SoundPlayer(SoundPath);
            Sound.Play();
            //  c.Connect("192.168.210.251", 9999);
            c.Connect("127.0.0.1", 9999);
            btnplay.Image = Properties.Resources.btnplay;
          
            BinaryFormatter fobj = new BinaryFormatter();
            string symbol = (string)fobj.Deserialize(c.GetStream());

            PlayGame f = new PlayGame(c, symbol);

            this.Hide();
            f.Show();
            
        }
        private void btnplay_MouseEnter(object sender, EventArgs e)
        {
            btnplay.Image = Properties.Resources.btnplay;
        }

        private void btnplay_MouseLeave(object sender, EventArgs e)
        {
            btnplay.Image = Properties.Resources.Play_button_icon_in_yellow_color_on_transparent_background_PNG1;

        }
    }
}
