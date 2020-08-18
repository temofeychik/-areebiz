using System.IO;
using System.Threading.Tasks;

namespace CareebizExam.WebApi.Services.GoogleMaps
{
    public interface IGoogleMapsService
    {
        Task<Stream> GetMap(Models.Rectangle rect, int width, int height, int zoom);
    }
}