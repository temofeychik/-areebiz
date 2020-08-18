using System.IO;

namespace CareebizExam.WebApi.Services.Pdf
{
    public interface IPdfService
    {
        Stream ImageToPdf(Stream imageStream, int pageWidth);
    }
}