namespace NaiveLogger
{
    public static class NaiveLogger
    {
        public static void Debug(string messge)
        {
            using (var sw = System.IO.File.AppendText(@"C:\NaiveLogger.txt"))
            {
                sw.WriteLine(messge);
            }
        }
    }
}
