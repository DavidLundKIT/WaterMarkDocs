using System.IO;

namespace WatermarkService
{
    public interface IWaterStampService
    {
        void AddWatermarkEveryPage(string source, string dest, string phrase);
        void AddWatermarkEveryPage(Stream source, Stream dest, string phrase);
        void AddWatermarkLastPage(string source, string dest, string phrase);
        void AddWatermarkLastPage(Stream source, Stream dest, string phrase);
    }
}
