using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CareebizExam.WebApi.ViewModels
{
    public class RectangleViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public float North { get; set; }

        [Required]
        public float South { get; set; }

        [Required]
        public float East { get; set; }

        [Required]
        public float West { get; set; }
    }
}
