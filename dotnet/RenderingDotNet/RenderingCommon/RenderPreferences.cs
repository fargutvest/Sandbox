namespace RenderingCommon
{
    public static class RenderPreferences
    {
        public const int Width = 1920;
        public const int Height = 1080;

        public static readonly int MaxBufferSize = Height
                                                   * 85; /*experimental found coeficient*/
    }
}
