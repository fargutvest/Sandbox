using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.AccessControl;

class Program
{
    public static void Main()
    {
        Process.GetCurrentProcess().StartInfo.UseShellExecute = true;
        Process.GetCurrentProcess().StartInfo.Verb = "runas";

        string[] matches = new string[] { "Xamarin", "xamarin" };
        Action<RegistryKey> find = null;
        Action<RegistryKey> valueFind = null;

        valueFind = new Action<RegistryKey>((rsubkey) =>
        {
            var vn = rsubkey.GetValueNames();

            foreach (var name in vn)
            {
                var value = rsubkey.GetValue(name);

                foreach (var match in matches)
                {
                    if (value is string && (value as string).Contains(match))
                        rsubkey.DeleteValue(name);
                }
            }
        });

        find = new Action<RegistryKey>((rkey) =>
        {
            valueFind(rkey);

            if (rkey.SubKeyCount==0)
                return;

            var arr = rkey.GetSubKeyNames();

            foreach (var n in arr)
            {
                RegistryKey rsubkey = null;
                try
                {
                    rsubkey = rkey.OpenSubKey(n, false);
                }
                catch { continue; }

                if (rsubkey == null)
                    continue;

                find(rsubkey);
            }
        });

        //find(Registry.ClassesRoot);
        //find(Registry.CurrentUser);
        find(Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Installer", true));
        //find(Registry.LocalMachine);
        //find(Registry.Users);
        //find(Registry.CurrentUser);
    }
}