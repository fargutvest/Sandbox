using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Threading;
using System.Windows.Threading;

namespace AsyncProgrammingAlexDevis.WPF
{
    public class Chapter9 : Base
    {
        public Chapter9(WrapPanel wrapPanel) : base(wrapPanel) { }

        public async void OnClick(string[] s_Domains)
        {
            AlexsMethod();
            Task.Delay(20000).Wait();
        }

        async Task Catcher()
        {
            // Здесь AlexsException никогда не возбуждается
            Task task = Thrower();
            Task.Delay(1000).Wait();
            try
            {
                await task;
            }
            catch (Exception)
            {
                // Исключение обрабатывается здесь
            }
        }
        async Task Thrower()
        {
            await Task.Delay(100);
            throw new Exception();
        }

        async void AlexsMethod()
        {
            try
            {
                var task = DelayForever();
                //var task = DelayForOneSecond();
                await task;
            }
            finally
            {
                // Сюда мы никогда не попадем
            }
        }
        Task DelayForever()
        {
            return new TaskCompletionSource<object>().Task;
        }

        Task DelayForOneSecond()
        {
            return Task.Run(() =>
            {
                Task.Delay(1000).Wait();
            });
        }




    }
}
