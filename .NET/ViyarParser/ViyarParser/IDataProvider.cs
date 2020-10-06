namespace ViyarParser
{
    public interface IDataProvider
    {
        T GetData<T>();
    }

    public class ViyarByDataProvider : IDataProvider
    {
        public ViyarByDataEntry GetData<ViyarByDataEntry>()
        {
            throw new System.NotImplementedException();
        }
    }

    public class ViyarByDataEntry
    {
        public int Code { get; set; }
        public string ImgUrl { get; set; }
        public string Price { get; set; }
    }

    public class Remains
}
