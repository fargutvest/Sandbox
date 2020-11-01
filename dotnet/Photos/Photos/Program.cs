using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace Photos
{
    class Program
    {
        //private static List<string> _templates = new List<string>()
        //{
        //    "IMG_YYYYMMDD_hhmmss",
        //    "IMG_{0}_{1}"
        //};


        static void Main(string[] args)
        {
            var root = args[0];
            var deniedDirectories = args.ToList().GetRange(1, args.Length - 1);
            var treeRoot = ReadTree(root, deniedDirectories);
            
            var allFiles = GetFiles(treeRoot);

            var groupedByLenght = new Dictionary<long, List<FileInfo>>();
            foreach (var item in allFiles)
            {
                if (groupedByLenght.ContainsKey(item.Length) == false)
                {
                    groupedByLenght[item.Length] = new List<FileInfo>();
                }
                groupedByLenght[item.Length].Add(item);
            }

            var dublicates = groupedByLenght.Where(_ => _.Value.Count() > 1).ToList();

            Console.WriteLine($"Detected {dublicates.Count()} dublicates");

            for (var i = 0; i < dublicates.Count(); i++)
            {
                var fileNames = dublicates[i].Value.Select(_ => Path.GetFileName(_.FullName)).ToList();
                if (fileNames.Distinct().Skip(1).Any() == false)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                var dublicate = dublicates[i].Value;

                dublicate.ForEach(_ => Console.WriteLine(_.FullName));
                Console.ForegroundColor = ConsoleColor.Gray;

                Console.WriteLine($"N - Next; B - Back; O - Open {i + 1}/{dublicates.Count()}");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.N)
                {
                    i -= i < dublicates.Count() - 1 ? 0 : 1;
                    continue;
                }
                if (key.Key == ConsoleKey.B)
                {
                    i -= i > 0 ? 2 : 1;
                    continue;
                }
                if (key.Key != ConsoleKey.O)
                {
                    continue;
                }

                var hotKeysSet = JsonConvert.DeserializeObject<KeyStructure>(File.ReadAllText("keys.json")).Keys[dublicate.Count() - 1];
               
                for (var j = 0; j < dublicate.Count; j++)
                {
                    OpenWindow(dublicate[j].FullName, hotKeysSet[j]);
                }
            }
        }

        private static void OpenWindow(string path, params VirtualKeyCode[] keys)
        {
            var s = new InputSimulator();
            Process.Start("explorer.exe", string.Format("/select,\"{0}\"", path));

            var delay1 = 1000;
            var delay2 = 500;

            Task.Delay(delay1).Wait();
            s.Keyboard.KeyDown(VirtualKeyCode.LWIN);
            foreach (var key in keys)
            {
                Task.Delay(delay2).Wait();
                s.Keyboard.KeyDown(key);
            }

            Task.Delay(delay2).Wait();
            s.Keyboard.KeyUp(VirtualKeyCode.LWIN);
            foreach (var key in keys)
            {
                s.Keyboard.KeyUp(key);
            }
        }

        public static List<FileInfo> GetFiles(MyDirectory dir)
        {
            var files = dir.Files;
            foreach (var item in dir.SubDirectories)
            {
                files.AddRange(GetFiles(item));
            }
            return files;
        }

        public static MyDirectory ReadTree(string root, List<string> deniedDirectories)
        {
            var treeRoot = new MyDirectory()
            {
                Path = root,
                Info = new DirectoryInfo(root),
                Files = Directory.GetFiles(root).Select(_ => new FileInfo(_)).ToList(),
                SubDirectories = Directory.GetDirectories(root).Where(_ => deniedDirectories.Any(denied => _.Contains(denied)) == false).Select(_ => ReadTree(_, deniedDirectories)).ToList()
            };
            return treeRoot;
        }

        //CreateShortcut("testShortCut", @"F:\", @"C:\lena.bmp");
        //public static void CreateShortcut(string shortcutName, string shortcutPath, string targetFileLocation)
        //{
        //    var shortcutLocation = Path.Combine(shortcutPath, shortcutName + ".lnk");
        //    var shell = new WshShell();
        //    var shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

        //    shortcut.Description = $"Created programmatically by {Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName)}";
        //    shortcut.IconLocation = targetFileLocation;
        //    shortcut.TargetPath = @"C:\lena.bmp";
        //    shortcut.Save();
        //}

        //private static void ReplaceDublicatesByLinks(string targetPath, string root, List<string> deniedDirectories)
        //{
        //    deniedDirectories.Add(targetPath);
        //    deniedDirectories.Add(@"D:\фотки для альбома");
        //    var treeRoot = ReadTree(root, deniedDirectories);
        //    var targetRoot = ReadTree(targetPath, new List<string>());
        //    var allFilesFromTarget = GetFiles(targetRoot);
        //    var allFilesFromRoot = GetFiles(treeRoot);

        //    foreach (var item in allFilesFromTarget)
        //    {
        //        var sources = allFilesFromRoot.Where(_ => item.Length == _.Length).ToList();
        //        try
        //        {
        //            CreateShortcut(item.Name, Path.GetDirectoryName(item.FullName), sources[0].FullName);
        //            System.IO.File.Delete(item.FullName);
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //    }
        //}
    }
}
