using System.ComponentModel.DataAnnotations;

namespace CareebizExam.WebApi.ViewModels
{
    public class ExportRectanglesViewModel
    {
        [Required]
        public int Id { get; set; }

        public int Zoom { get; set; } = 10;

        public int ImageHeight { get; set; } = 800;

        public int ImageWidth { get; set; } = 800;

    }
}
