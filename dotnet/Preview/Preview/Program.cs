using Microsoft.Win32;
var target = Environment.GetCommandLineArgs()[1];

var classesRoot = Registry.ClassesRoot;

string perceivedType = "PerceivedType";
string text = "text";


if (classesRoot.GetSubKeyNames().Contains(target))
{
    var targetSk = classesRoot.OpenSubKey(target, true);
    targetSk.SetValue(perceivedType, text);
    Console.WriteLine($"Added value {perceivedType}={text} to subkey {target}");
}
else
{
    var targetSk = classesRoot.CreateSubKey(target, true);
    targetSk.SetValue(perceivedType, text);
    Console.WriteLine($"Created subkey {target} with value {perceivedType}={text}");
}