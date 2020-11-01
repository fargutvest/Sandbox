using System.ComponentModel;

namespace CSharpToUmlConverter
{
    public class StereotypeInfo
    {
        public StereotypeType Type { get; set; }

        private string name;
        public string Name
        {
            get => name;
            set => name = value.TrimWhitespaces();
        }
        
        public string Generics { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public enum StereotypeType
    {
        [Description("class")]
        Class,
        [Description("abstract class")]
        AbstractClass,
        [Description("interface")]
        Interface
    }
}