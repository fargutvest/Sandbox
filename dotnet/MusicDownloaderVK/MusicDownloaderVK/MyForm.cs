using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.Filters;

namespace MusicDownloaderVK
{
    public partial class MyForm : Form
    {
        private Controller _controller;

        public MyForm()
        {
            InitializeComponent();

            tbPageId.Text = Properties.Settings.Default.PageId.ToString();
            rbGroup.Checked = !Properties.Settings.Default.UserNoGroup;
            rbUser.Checked = Properties.Settings.Default.UserNoGroup;
            tbPathFolder.Text = Properties.Settings.Default.DownloadDirectory;
            tbPhone.Text = Properties.Settings.Default.Phone;
            tbPassword.Text = Properties.Settings.Default.Password;

            _controller = new Controller();
            _controller.InfoUpdated += OnDataObtained;
            _controller.DownloadProgressChanged += OnDownloadProgressChanged;
        }

        private void OnDataObtained(List<string> songs)
        {
            lvMusicList.BeginInvoke(new Action(() =>
            {
                lvMusicList.Clear();
                foreach (var song in songs)
                {
                    lvMusicList.Items.Add(new ListViewItem(song));
                }
            }));

            lbMusicList.BeginInvoke(new Action(() =>
            {
                lbMusicList.Invoke(new Action<string>(s => lbMusicList.Text = string.Format("Audio List <{0}>", s)), songs.Count.ToString());
            }));
        }


        private void Download_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.PageId = Convert.ToUInt32(tbPageId.Text);
            Properties.Settings.Default.Save();
            _controller.StartDownloadSongsByPage(Properties.Settings.Default.PageId);
        }

        private void OnNewDir_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.DownloadDirectory = folderBrowserDialog1.SelectedPath;
                tbPathFolder.Text = Properties.Settings.Default.DownloadDirectory;
            }
        }

        private void Authorise_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Phone = tbPhone.Text;
            Properties.Settings.Default.Password = tbPassword.Text;
            Properties.Settings.Default.UserNoGroup = rbUser.Checked;
            Properties.Settings.Default.Save();
            lvMusicList.Clear();
            _controller.Authorise();
        }



        private void MyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
            _controller.Dispose();
        }

        private void OnStopDownload_Click(object sender, EventArgs e)
        {
            _controller.StopDownloadSongs();
        }

        private void OnDownloadProgressChanged(SongsDownloader.Progress e)
        {
            tbProgressCurrent.BeginInvoke(new Action<bool>(s => tbProgressCurrent.Text =
                string.Format("{0} - {1}%   All progress  - {2}%", e.Name, e.PercerntCurrent, e.PercerntAll)), true);

            pbCurrent.BeginInvoke(new Action<int>(a => pbCurrent.Value = a), e.PercerntCurrent);
            pbAll.BeginInvoke(new Action<int>(a => pbAll.Value = a), e.PercerntAll);
        }

    }
}
