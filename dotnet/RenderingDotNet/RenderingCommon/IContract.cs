using System.ServiceModel;

namespace RenderingCommon
{
    [ServiceContract]
    public interface IContract
    {
        [OperationContract]
        void NewVerticalLine(int[] generatedVerticalLine);
    }
}
