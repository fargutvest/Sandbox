using System;

namespace _70_483
{
    public class checked_test
    {
        public void RunAll()
        {
            Run();
            Run1();
            Run2();
            Run3();
        }

        public void Run()
        {
            try
            {
                int a = int.MaxValue;
                a = checked(a + 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void Run1()
        {
            try
            {
                checked
                {
                    int a = int.MaxValue;
                    a = a + 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public void Run2()
        {
            checked
            {
                Run3();
            }
        }

        public void Run3()
        {
            int a = int.MaxValue;
            a = a + 1;
        }


    }
}
