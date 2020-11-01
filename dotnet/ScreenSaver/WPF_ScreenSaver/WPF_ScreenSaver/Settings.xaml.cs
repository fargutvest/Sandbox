using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Forms;

namespace WPF_ScreenSaver
{
    /// <summary>
    /// Allows user to pick directories of images for use with
    /// the screen saver
    /// </summary>
    public partial class Settings : System.Windows.Window
    {
        #region Ctor
        public Settings()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Settings_Loaded);
            this.Closing += new System.ComponentModel.CancelEventHandler(Settings_Closing);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Populate the listbox by reading the file on disk if it exists
        /// </summary>
        private void Settings_Loaded(object sender, RoutedEventArgs e)
        {
            String fullFileName = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Globals.TempFileName);

            //populate the listbox by reading the file on disk if it exists
            String line;
            try
            {
                using (StreamReader reader = File.OpenText(fullFileName))
                {
                    line = reader.ReadLine();
                    while (line != null)
                    {
                        lstFolders.Items.Add(line);
                        line = reader.ReadLine();
                    }
                    reader.Close();
                }
            }
            catch (FileNotFoundException fex)
            {
            }
            

        }

        /// <summary>
        /// Persist selected directories to file on close
        /// </summary>
        private void Settings_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DealWithLocationFile();
        }

        /// <summary>
        /// Pick another image location to use within the screen saver
        /// </summary>
        private void btnPick_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (fd.SelectedPath != String.Empty)
                {
                    if (!lstFolders.Items.Contains(fd.SelectedPath))
                        lstFolders.Items.Add(fd.SelectedPath);
                }
            }
        }
        

        /// <summary>
        /// Delete directory file on disk if it exists, and recreate
        /// the file based on the new listbox folders that the user
        /// picked
        /// </summary>
        private void DealWithLocationFile()
        {
            String fullFileName = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), 
                Globals.TempFileName);

            //Delete existing file if it exists
            if (File.Exists(fullFileName))
            {
                File.Delete(fullFileName);
            }

            //re-create file, and the in memory collection of images
            using (TextWriter tw = new StreamWriter(fullFileName))
            {
                Globals.Files.Clear();
                //process each foldername, extracting the image files
                foreach (String folderName in lstFolders.Items)
                {
                    try
                    {
                        foreach (var file in
                            new DirectoryInfo(folderName).GetFiles().IsImageFile())
                        {
                            Globals.Files.Add(file);
                        }
                        tw.WriteLine(folderName);
                    }
                    catch (DirectoryNotFoundException dex)
                    {
                    }
                    catch (ArgumentException ax)
                    {
                    }
                }
                tw.Close();
            }
        }
        #endregion


    }
}