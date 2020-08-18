using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace CareebizExam.WebApi.Services.Zip
{
    public class ZipService : IZipService
    {
        public async Task<Stream> MakeZip(params Stream[] streamsToZip)
        {
            var memoryStream = new MemoryStream();


            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                for (int i = 0; i < streamsToZip.Length; i++)
                {
                    var file = archive.CreateEntry($"{i + 1}.pdf");

                    using (var entryStream = file.Open())
                    {
                        await streamsToZip[i].CopyToAsync(entryStream);
                    }
                }
            }

            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;

        }
    }
}
