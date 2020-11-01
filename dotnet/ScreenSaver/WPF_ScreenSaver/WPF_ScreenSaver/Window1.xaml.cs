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
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.IO;

namespace WPF_ScreenSaver
{
    /// <summary>
    /// The actual screen saver. Picks a working set of n-many images
    /// from the total list of images, and cycles through these. And then
    /// repeats this process
    /// </summary>
    public partial class Window1 : System.Windows.Window
    {
        #region Data
        private Random rand = new Random(15);
        private DispatcherTimer timer = new DispatcherTimer();
        private Double currentChangeCount = 0;
        private Boolean isActive;
        private Point mousePosition;

        #endregion

        #region Ctor
        public Window1()
        {
            InitializeComponent();
        }
        #endregion

        #region Private Methods
        private void OnLoaded(object sender, EventArgs e)
        {

#if !DEBUG
            Topmost = true;
            MouseMove += new MouseEventHandler(PhotoStack_MouseMove);
            MouseDown += new MouseButtonEventHandler(PhotoStack_MouseDown);
            KeyDown += new KeyEventHandler(PhotoStack_KeyDown);
#endif

            Storyboard sb = this.TryFindResource("sbLoaded") as Storyboard;
            timer.Interval = sb.Duration.TimeSpan;
            timer.IsEnabled = true;
            timer.Tick += new EventHandler(timer_Tick);


            ReadInCurrentFiles();
            CreateWorkingSetOfFiles();
            sb.Begin(this);

        }
        
        /// <summary>
        /// Creates a window of n-many images from the total list of
        /// images available. If none are available create a working
        /// set of place holder (she-hulk images)
        /// </summary>
        private void CreateWorkingSetOfFiles()
        {
            //grab n-many random images
            Int32 currentSetIndex = 0;
            Globals.WorkingSetOfImages.Clear();

            if (Globals.Files.Count > 0)
            {
                while (currentSetIndex < Globals.WorkingSetLimit)
                {
                    Int32 randomIndex = rand.Next(0, Globals.Files.Count);
                    String imageUrl = Globals.Files[randomIndex].FullName;
                    if (!Globals.WorkingSetOfImages.Contains(imageUrl))
                    {
                        Globals.WorkingSetOfImages.Add(imageUrl);
                        currentSetIndex++;
                    }
                }
            }
            else
            {
                for (int i = 0; i < Globals.WorkingSetLimit; i++)
                {
                    Globals.WorkingSetOfImages.Add("Images/NoImage.jpg");
                }
            }

            //create ItemsControl
            itemsCurrentImages.Items.Clear();
            foreach (String imageUrl in Globals.WorkingSetOfImages)
            {
                SelectableImageUrl selectableImageUrl = new SelectableImageUrl();
                selectableImageUrl.ImageUrl = imageUrl;
                selectableImageUrl.IsSelected = false;
                itemsCurrentImages.Items.Add(selectableImageUrl);

            }

        }

        /// <summary>
        /// Read in all images from users currently selected directories
        /// from the file stored on disk
        /// </summary>
        private void ReadInCurrentFiles()
        {

            Globals.Files.Clear();
            
            String fullFileName = System.IO.Path.Combine(
                 Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                 Globals.TempFileName);


            //populate the listbox by reading the file on disk if it exists
            String directoryLineRead;
            try
            {
                using (StreamReader reader = File.OpenText(fullFileName))
                {
                    directoryLineRead = reader.ReadLine();
                    while (directoryLineRead != null)
                    {
                        try
                        {
                            foreach (var file in
                                new DirectoryInfo(directoryLineRead).GetFiles().IsImageFile())
                            {
                                Globals.Files.Add(file);
                            }
                        }
                        catch (DirectoryNotFoundException dex)
                        {
                        }
                        catch (ArgumentException ax)
                        {
                        }
                        directoryLineRead = reader.ReadLine();
                    }
                    reader.Close();
                }
            }
            catch (FileNotFoundException fex)
            {
            }
        }

        /// <summary>
        /// Assign new image, and if at end of working set of images
        /// get a new working set of images
        /// </summary>
        private void timer_Tick(object sender, EventArgs e)
        {

            Int32 randomIndex = rand.Next(0, Globals.WorkingSetOfImages.Count);
            String imageUrl = Globals.WorkingSetOfImages[randomIndex];

            foreach (SelectableImageUrl selectableImageUrl in itemsCurrentImages.Items)
            {
                if (selectableImageUrl.ImageUrl == imageUrl)
                    selectableImageUrl.IsSelected = true;
                else
                    selectableImageUrl.IsSelected = false;
            }
            
            //update 3d cube images
            img1.Source = new BitmapImage(new Uri(imageUrl, UriKind.RelativeOrAbsolute));
            img2.Source = new BitmapImage(new Uri(imageUrl, UriKind.RelativeOrAbsolute));
            img3.Source = new BitmapImage(new Uri(imageUrl, UriKind.RelativeOrAbsolute));
            img4.Source = new BitmapImage(new Uri(imageUrl, UriKind.RelativeOrAbsolute));
            img5.Source = new BitmapImage(new Uri(imageUrl, UriKind.RelativeOrAbsolute));
            img6.Source = new BitmapImage(new Uri(imageUrl, UriKind.RelativeOrAbsolute));
            //do we need to create a new working set of images
            currentChangeCount++;
            if (currentChangeCount == Globals.WorkingSetLimit)
            {
                CreateWorkingSetOfFiles();
                currentChangeCount = 0;
            }
        }

        /// <summary>
        /// If key down, exit, like a good screen saver should
        /// </summary>
        private void PhotoStack_KeyDown(object sender, KeyEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// If MouseDown, exit, like a good screen saver should
        /// </summary>
        private void PhotoStack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Work out whether to quit based on a mouse move. If movement is large
        /// enough to warrant quiting exit.
        /// </summary>
        private void PhotoStack_MouseMove(object sender, MouseEventArgs e)
        {
            Point currentPosition = e.MouseDevice.GetPosition(this);
            // Set IsActive and MouseLocation only the first time this event is called.
            if (!isActive)
            {
                mousePosition = currentPosition;
                isActive = true;
            }
            else
            {
                // If the mouse has moved significantly since first call, close.
                if ((Math.Abs(mousePosition.X - currentPosition.X) > 10) ||
                    (Math.Abs(mousePosition.Y - currentPosition.Y) > 10))
                {
                    Application.Current.Shutdown();
                }
            }
        }
        #endregion
    }
}