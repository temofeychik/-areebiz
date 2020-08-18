using System.IO;
using System.Threading.Tasks;

namespace CareebizExam.WebApi.Services.Zip
{
    public interface IZipService
    {
        Task<Stream> MakeZip(params Stream[] streamsToZip);
    }
}