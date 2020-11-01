using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model.Attachments;

namespace MusicDownloaderVK
{
    public class Controller : IDisposable
    {
        private SongsDownloader _songsDownloader;
        private VkApi _vkApi;


        public Action<List<string>> InfoUpdated = delegate { };
        public event Action<SongsDownloader.Progress> DownloadProgressChanged;

        public Controller()
        {
            _songsDownloader = new SongsDownloader();
            _vkApi = new VkApi();
        }

        private void InfoUpdate(ICollection<Audio> songsInfo)
        {
            List<string> songs = new List<string>();
            foreach (var songInfo in songsInfo)
            {
                songs.Add(songInfo.Artist + " " + songInfo.Title);
            }

            InfoUpdated(songs);
        }

        public void StartDownloadSongsByPage(uint pageId)
        {
            Properties.Settings.Default.DownloadDirectory = string.Format(@"{0}\{1}{2}",
                Properties.Settings.Default.ParentDownloadDirectory, 
                Properties.Settings.Default.PrefixDownloadDirectory,
                pageId.ToString());

            if (!Directory.Exists(Properties.Settings.Default.DownloadDirectory))
            {
                Directory.CreateDirectory(Properties.Settings.Default.DownloadDirectory);
            }

            var songsInfo = _vkApi.Audio.Get(pageId);
            InfoUpdate(songsInfo);
            _songsDownloader.DownloadProgressChenged += DownloadProgressChanged;
            _songsDownloader.Start(songsInfo);

        }

        public void StopDownloadSongs()
        {
            _songsDownloader.Stop();
        }

        public void Authorise()
        {
            try
            {
                _vkApi.Authorize(Convert.ToInt32(Properties.Settings.Default.AppId),
                    Properties.Settings.Default.Phone,
                    Properties.Settings.Default.Password, Settings.All);
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public void Dispose()
        {
            _songsDownloader.Dispose();
        }
    }
}
