using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace CSharpToUmlConverter
{
    public class Converter
    {
        private readonly List<string> files = new List<string>();
        private string root;
        private readonly List<Node> nodes = new List<Node>();
        private string uml = string.Empty;

        public void Start(string root, string outputPath)
        {
            this.root = root;
            GetFiles();
            Console.WriteLine($"found {files.Count} files");
            Console.WriteLine("Generating...");
            
            Parse();

#if verbose
            Report();
#endif

            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }

            BuildUml();
            File.AppendAllLines(outputPath, new List<string> { uml });
            Console.WriteLine("Successful generated");
        }


        private void BuildUml()
        {
            foreach (var node in nodes)
            {
                var stereotype = node.Stereotype.Type.GetDescription();

                if (node.Parents != null)
                {
                    foreach (var parent in node.Parents)
                    {
                        var generics = parent.Generics != null ? $":{parent.Generics}" : string.Empty;
                        var prefix = node.Stereotype.Type != StereotypeType.Class ? $"{stereotype} " : string.Empty;
                        uml += $"{prefix}{node.Stereotype.Name}--|>{parent.Name}{generics}{Environment.NewLine}";
                    }
                }
                else
                {
                    uml += $"{stereotype} {node.Stereotype.Name}{Environment.NewLine}";
                }
            }
        }

        private void GetFiles()
        {
            files.Clear();

            void AddFiles(string directory)
            {
                files.AddRange(Directory.GetFiles(directory).Where(_ => _.EndsWith(".cs")));
                foreach (var subDir in Directory.GetDirectories(directory))
                {
                    AddFiles(subDir);
                }
            }

            AddFiles(root);
        }

        private void Report()
        {
            foreach (var node in nodes)
            {
                Console.WriteLine($"{new string('-', 200)}");
                Console.WriteLine("Stereotype:");
                Console.WriteLine(node.Stereotype.Name);
                Console.WriteLine("Generics:");
                Console.WriteLine(node.Stereotype.Generics ?? "null");
                Console.WriteLine("Parents:");
                if (node.Parents != null)
                {
                    foreach (var nodeParent in node.Parents)
                    {
                        var generics = nodeParent.Generics != null
                            ? $"has generics {nodeParent.Generics}" : "";
                        Console.WriteLine($"{nodeParent.Name} {generics}");
                    }
                }

                Console.WriteLine("Constraints:");
                Console.WriteLine(node.Constraints ?? "null");
                Console.WriteLine();
            }
        }

        private void Parse()
        {
            foreach (var file in files)
            {
                var stream = File.OpenText(file);
                var csFile = stream.ReadToEnd();
                csFile = csFile.TrimCommentedCode();

                var regularExpression = new Regex(Node.Template);
                var matches = regularExpression.Matches(csFile);

                for (var i = 0; i < matches.Count; i++)
                {
                    nodes.Add(Parse(matches[i].Value));
                }
            }
        }

        private Node Parse(string a)
        {
            var isClass = Regex.IsMatch(a, "^(.|\n)*?class .*?where") || !a.Contains("where") && a.Contains("class");
            var isInterface = a.Contains("interface");
            var isAbstract = a.Contains("abstract");

            a = isClass || isAbstract ? a.Remove(0, a.IndexOf("class", StringComparison.Ordinal) + "class".Length) : a;
            a = isInterface ? a.Remove(0, a.IndexOf("interface", StringComparison.Ordinal) + "interface".Length) : a;

            a = a.Trim("\r\n").Trim("\n\t").Trim("\r").Trim("\n").Trim("\t");
            var node = new Node();

            if (a.Contains("where")) //has constraint
            {
                if (a.IndexOf(':') < a.IndexOf("where", StringComparison.Ordinal)) //class has inheritance 
                {
                    if (a.Substring(0, a.IndexOf(':')).Contains('<') && a.Substring(0, a.IndexOf(':')).Contains('>')) // class has generics
                    {
                        node.Stereotype.Name = a.Substring(0, a.IndexOf('<'));
                        a = a.Remove(0, a.IndexOf('<'));
                        node.Stereotype.Generics = a.Substring(0, a.IndexOf('>') + 1);
                        a = a.Remove(0, a.IndexOf('>'));
                        a = a.Remove(0, a.IndexOf(':'));
                    }
                    else // class haven`t generics
                    {
                        node.Stereotype.Name = a.Substring(0, a.IndexOf(':'));
                        a = a.Remove(0, a.IndexOf(':'));
                        node.Stereotype.Generics = null;
                    }

                    node.ParentsFromString = a.Substring(0, a.IndexOf("where", StringComparison.Ordinal));
                    a = a.Remove(0, a.IndexOf("where", StringComparison.Ordinal));
                    node.Constraints = a;
                }
                else //class haven`t inheritance
                {
                    if (a.Substring(0, a.IndexOf("where", StringComparison.Ordinal)).Contains('<') && a.Substring(0, a.IndexOf("where", StringComparison.Ordinal)).Contains('>')
                    ) // class has generics
                    {
                        node.Stereotype.Name = a.Substring(0, a.IndexOf('<'));
                        a = a.Remove(0, a.IndexOf('<'));
                        node.Stereotype.Generics = a.Substring(0, a.IndexOf('>') + 1);
                        a = a.Remove(0, a.IndexOf('>'));
                        a = a.Remove(0, a.IndexOf("where", StringComparison.Ordinal));
                    }
                    else // class haven`t generics
                    {
                        node.Stereotype.Name = a.Substring(0, a.IndexOf("where", StringComparison.Ordinal));
                        node.Stereotype.Generics = null;
                        a = a.Remove(0, a.IndexOf("where", StringComparison.Ordinal));
                    }

                    node.Parents = null;
                    node.Constraints = a;
                }
            }
            else // haven`t constraint
            {
                if (a.Contains(':')) //class has inheritance 
                {
                    if (a.Substring(0, a.IndexOf(':')).Contains('<') && a.Substring(0, a.IndexOf(':')).Contains('>')) // class has generics
                    {
                        node.Stereotype.Name = a.Substring(0, a.IndexOf('<'));
                        a = a.Remove(0, a.IndexOf('<'));
                        node.Stereotype.Generics = a.Substring(0, a.IndexOf('>') + 1);
                        a = a.Remove(0, a.IndexOf('>'));
                        a = a.Remove(0, a.IndexOf(':'));
                    }
                    else // class haven`t generics
                    {
                        node.Stereotype.Name = a.Substring(0, a.IndexOf(':'));
                        node.Stereotype.Generics = null;
                        a = a.Remove(0, a.IndexOf(':'));
                    }

                    node.ParentsFromString = a;
                    node.Constraints = null;
                }
                else // class haven`t inheritance
                {
                    if (a.Contains('<') && a.Contains('>')) // class has generics
                    {
                        node.Stereotype.Name = a.Substring(0, a.IndexOf('<'));
                        a = a.Remove(0, a.IndexOf('<'));
                        node.Stereotype.Generics = a.Substring(0, a.IndexOf('>') + 1);
                    }
                    else // class haven`t generics
                    {
                        node.Stereotype.Name = a;
                        node.Stereotype.Generics = null;
                        node.Parents = null;
                        node.Constraints = null;
                    }
                }
            }

            if (isClass)
                node.Stereotype.Type = StereotypeType.Class;
            if (isAbstract)
                node.Stereotype.Type = StereotypeType.AbstractClass;
            if (isInterface)
                node.Stereotype.Type = StereotypeType.Interface;

            return node;
        }
    }
}


//public Node Parse1(string a)
//{
//a = a.Replace("class", "").Trim('{').Replace("\r\n", "");
//var node = new Node();



//// has constraint, class has inheritance, class has generics
//var assertion = "(?=^.*<.*> *:.*where).*";
////  Abc<T>  :  Base1<T2, T3>, IBase2<T2, T1>, ICouple where T1 : IBase3 where T2 : new(), class
//var className = @"^.*?(?=<)";
//var classGenerics = "<.*?>(?= *?:)";
//var parrents = "(?<=> *:).*? (?=where)";
//var constraints = "(?<=^.*)(?=where).*";

//// has constraint, class has inheritance, class haven`t generics
//assertion = "(?=^.*:.*where).*";
////  Abc  :  Base1<T2, T3>, IBase2<T2, T1>, ICouple where T1 : IBase3 where T2 : new(), class where T3 : IBase
//className = "^.*?(?=:)";
//classGenerics = "";
//parrents = "(?<=^:).*?(?=where)";
//constraints = "(?=^ *where).*";

//// has constraint, class haven`t inheritance, class has generics
//assertion = "";

//// has constraint, class haven`t inheritance, class haven`t generics
//// haven`t constraint, class has inheritance, class has generics
//// haven`t constraint, class has inheritance, class haven`t generics
//// haven`t constraint, class haven`t inheritance, class has generics
//// haven`t constraint, class haven`t inheritance, class haven`t generics

//return node;
//}

//private void Process()
//{
//    foreach (var node in nodes)
//    {
//        if (node.Parents != null)
//        {
//            foreach (var parent in node.Parents)
//            {
//                var type = nodes.FirstOrDefault(_ => _.Stereotype.Name == parent.Name)?.Stereotype.Type;
//                if (type != null)
//                {
//                    parent.Type = type.Value;
//                }
//            }
//        }
//    }
//}