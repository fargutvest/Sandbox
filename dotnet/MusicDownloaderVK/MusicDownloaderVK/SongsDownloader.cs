using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO;
using Newtonsoft.Json.Linq;
using VkNet.Model.Attachments;

namespace MusicDownloaderVK
{
    public class SongsDownloader : IDisposable
    {
        private int _songsDownloaded = 0;
        private bool _enabledDownload = false;

        private WebClient _webClient;
        private Progress _progress;
        private IList<Audio> _songsInfo = new List<Audio>();

        public event Action<Progress> DownloadProgressChenged;

        public struct Progress
        {
            public int PercerntCurrent;
            public int PercerntAll;
            public string Name;
        }
        public SongsDownloader()
        {
            _webClient = new WebClient();
            _webClient.DownloadProgressChanged += OnDownloadProgressChanged;
        }
        public void Dispose()
        {
            _webClient.CancelAsync();
            _webClient.Dispose();
        }

        public void Start(IList<Audio> songsInfo)
        {
            _enabledDownload = true;
            _songsInfo = songsInfo;
            NextDownload(this, null);
        }

        public void Stop()
        {
            _enabledDownload = false;
            _webClient.CancelAsync();
        }

        private bool CheckFileIsFull(string path)
        {
            try
            {
                bool result = false;
                if (File.Exists(path))
                {
                    if (GetFileSize(_songsInfo[_songsDownloaded].Url) == new FileInfo(path).Length)
                    {
                        result = true;
                    }
                    else
                    {
                        File.Delete(path);
                        result = false;
                    }
                }
                else
                {
                    result = false;
                }
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private async void NextDownload(object sender, AsyncCompletedEventArgs e)
        {
            _songsDownloaded++;
            if (_songsDownloaded < _songsInfo.Count & _enabledDownload)
            {
                string path = string.Format(@"{0}\{1}{2}.mp3",
                    Properties.Settings.Default.DownloadDirectory,
                    _songsInfo[_songsDownloaded].Artist,
                    _songsInfo[_songsDownloaded].Title);
                if (CheckFileIsFull(path))
                {
                    _songsDownloaded++;
                    NextDownload(sender, e);
                }

                await _webClient.DownloadFileTaskAsync(_songsInfo[_songsDownloaded].Url, path);
                NextDownload(sender, e);
            }
        }

        private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            int r = e.ProgressPercentage;
            _progress = new Progress();
            _progress.PercerntCurrent = e.ProgressPercentage;

            _progress.PercerntAll = (int)((double)_songsDownloaded / (double)_songsInfo.Count * 100);
            _progress.Name = string.Format("{0}{1}.mp3", _songsInfo[_songsDownloaded].Artist, _songsInfo[_songsDownloaded].Title);

            if (DownloadProgressChenged != null)
                DownloadProgressChenged(_progress);

        }
        private long GetFileSize(Uri uri)
        {
            HttpWebRequest r0 = (HttpWebRequest)HttpWebRequest.Create(uri);
            r0.Method = "GET";
            HttpWebResponse res = (HttpWebResponse)r0.GetResponse();
            return res.ContentLength;
        }

    }
}
