using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;
using Mp3Class;

namespace Mp3PlayerApplication
{
    public partial class Form1 : Form
    {
        WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
        Mp3Player mp3player = new Mp3Player();
        double time = 0;
        bool playClicked = false;
        bool scroll = false;
        int tempValue;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Mp3 files|*.mp3|All Files(*.*)|*.*";
            openFile.FilterIndex = 1;
            if(openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txbFileLocation.Text = openFile.FileName;
            }
            
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            player.URL = txbFileLocation.Text;
            player.controls.currentPosition = time;
            player.controls.play();
            playClicked = true;
            lblName.Text = player.currentMedia.name;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            time = player.controls.currentPosition;
            player.controls.pause();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            time = 0;
            player.controls.stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (playClicked && !scroll)
            {
                lblTime.Text = player.controls.currentPositionString; //(player.controls.currentPosition).ToString();
                trbBar.Value = (int)((trbBar.Maximum / player.currentMedia.duration) * player.controls.currentPosition);
            }
        }

        private void trbBar_Scroll(object sender, EventArgs e)
        {
            scroll = true;
            player.controls.currentPosition = (player.currentMedia.duration / trbBar.Maximum) * trbBar.Value;
            lblTime.Text = player.controls.currentPositionString;
            scroll = false;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            player.settings.volume = trbVolume.Value * 10;
        }

        
    }
}
