using System;
using System.Threading;
using System.Windows;
using System.Windows.Media;


namespace Share
{
    public partial class Share
    {

        private Star[] stars;
        private Random random = new Random();
        private Timer timer;
       
        public Brush StarColor { get; set; } = Brushes.YellowGreen;
        public int CountOfStars { get; set; } = 15000;
        public int StarSize { get; set; } = 7;
        public int StarSpeed { get; set; } = 30;

        public event EventHandler<Color> Clear;
        public event EventHandler<EllipseItem> DrawEllipse;

        public Size AreaSize { get; set; }



        public Share(Size areaSize)
        {
            AreaSize = areaSize;
            stars = new Star[CountOfStars];
            
            for (int i = 0; i < CountOfStars; i++)
            {
                stars[i] = new Star()
                {
                    X = random.Next(-AreaSize.Width.ToInt(), AreaSize.Width.ToInt()),
                    Y = random.Next(-AreaSize.Height.ToInt(), AreaSize.Height.ToInt()),
                    Z = random.Next(1, AreaSize.Width.ToInt())
                };
            }
            timer = new Timer(timer_Tick, null, 0, 1);
        }
        

        private void timer_Tick(object state)
        {
            Clear?.Invoke(null, Colors.Black);
           
            for (int i = 0; i < CountOfStars; i++)
            {
                var star = stars[i];
                DrawStar(star);
                MoveStar(star);
            }
        }

        private void MoveStar(Star star)
        {
            star.Z -= StarSpeed;
            if (star.Z < 1)
            {
                star.X = random.Next(-AreaSize.Width.ToInt(), AreaSize.Width.ToInt());
                star.Y = random.Next(-AreaSize.Height.ToInt(), AreaSize.Height.ToInt());
                star.Z = random.Next(1, AreaSize.Width.ToInt());
            }
        }

        private void DrawStar(Star star)
        {
            var starSize = Map(star.Z, 0, AreaSize.Width.ToInt(), StarSize, 0);

            float x = Map(star.X / star.Z, 0, 1, 0, AreaSize.Width.ToInt()) + AreaSize.Width.ToInt() / 2;
            float y = Map(star.Y / star.Z, 0, 1, 0, AreaSize.Height.ToInt()) + AreaSize.Height.ToInt() / 2;

            DrawEllipse?.Invoke(null, new EllipseItem()
            {
                X = x,
                Y = y,
                Width = starSize,
                Height = starSize
            });
        }

        private float Map(float n, float start1, float stop1, float start2, float stop2)
        {
            return ((n - start1) / (stop1 - start1)) * (stop2 - start2) + start2;
        }
        

    }
}
