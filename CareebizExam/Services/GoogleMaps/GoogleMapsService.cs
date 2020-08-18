using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CareebizExam.WebApi.Services.GoogleMaps
{
    public class GoogleMapsService : IGoogleMapsService
    {
        private readonly GoogleMapsOptions _options;
        public GoogleMapsService(IOptions<GoogleMapsOptions> options)
        {
            _options = options.Value;
        }

        public async Task<Stream> GetMap(Models.Rectangle rect, int width, int height, int zoom)
        {

            float lat = (rect.North + rect.South) / 2;
            float lng = (rect.East + rect.West) / 2;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_options.StaticApiEndpoint);
            string url = $"staticmap?center={FormatNumber(lat)},{FormatNumber(lng)}" +
                $"&zoom={zoom}&size={width}x{height}" +
                $"&path=color:0xff0000ff|weight:2|{RectToPath(rect)}" +
                $"&key={_options.ApiKey}";

            var result = await client.GetAsync(url);

            var arr = await result.Content.ReadAsStreamAsync();

            return arr;
        }

        private string RectToPath(Models.Rectangle rect)
        {
            var a = $"{FormatNumber(rect.North)},{FormatNumber(rect.East)}";
            var b = $"{FormatNumber(rect.North)},{FormatNumber(rect.West)}";
            var c = $"{FormatNumber(rect.South)},{FormatNumber(rect.West)}";
            var d = $"{FormatNumber(rect.South)},{FormatNumber(rect.East)}";
            var e = $"{FormatNumber(rect.North)},{FormatNumber(rect.East)}";

            return $"{a}|{b}|{c}|{d}|{e}";
        }

        private static string FormatNumber(float number)
        {
            return number.ToString(CultureInfo.InvariantCulture);
        }
    }
}
