using System.Collections.Generic;
using System.Linq;

namespace CSharpToUmlConverter
{
    public class Node
    {
        public static readonly string Template = ".* (class|interface) ([^\"]|\n)*?(?={)";


        public StereotypeInfo Stereotype { get; set; }

        public List<StereotypeInfo> Parents { get; set; }

        public string ParentsFromString
        {
            set
            {
                var a = value.TrimColons()?.Trim(' ');
                var parents = new List<StereotypeInfo>();
                string GetGenerics()
                {
                    var opening = 0;
                    var closing = 0;
                    var index = 0;
                    foreach (var symbol in a)
                    {
                        if (symbol.Equals('<'))
                            opening++;
                        if (symbol.Equals('>'))
                            closing++;
                        if (opening == closing)
                            break;
                        index++;
                    }
                    return a.Substring(0, index + 1);
                }

                void Parse()
                {
                    var info = new StereotypeInfo();
                    if (a.Contains('<') && a.Contains('>')) //has generics
                    {
                        if (a.Contains(',')) //has commas
                        {
                            if (a.IndexOf('<') < a.IndexOf(',')) // first parent has generics
                            {
                                info.Name = a.Substring(0, a.IndexOf('<'));
                                a = a.Remove(0, a.IndexOf('<'));
                                info.Generics = GetGenerics();
                                a = a.Replace(info.Generics, string.Empty);
                                a = a.Remove(0, a.IndexOf(',') + 1);
                            }
                            else // first parent haven`t generics
                            {
                                info.Name = a.Substring(0, a.IndexOf(','));
                                info.Generics = null;
                                a = a.Remove(0, a.IndexOf(',') + 1);
                            }
                        }
                        else // haven`t commas
                        {
                            info.Name = a.Substring(0, a.IndexOf('<'));
                            a = a.Remove(0, a.IndexOf('<'));
                            info.Generics = GetGenerics();
                            a = a.Replace(info.Generics, string.Empty);
                        }
                    } // haven`t generics
                    else
                    {
                        if (a.Contains(',')) //has commas
                        {
                            info.Name = a.Substring(0, a.IndexOf(','));
                            info.Generics = null;
                            a = a.Remove(0, a.IndexOf(',') + 1);
                        }
                        else // haven`t commas
                        {
                            info.Name = a;
                            a = string.Empty;
                            info.Generics = null;
                        }
                    }
                    parents.Add(info);
                    if (a.Length > 0)
                        Parse();
                }
                Parse();
                Parents = parents;
            }
        }


        private string constraints;
        public string Constraints
        {
            get => constraints;
            set => constraints = value?.Trim(' ');
        }

        public Node()
        {
            Stereotype = new StereotypeInfo();
            Parents = new List<StereotypeInfo>();
        }
    }
}