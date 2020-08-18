using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CareebizExam.Models
{
    public class Rectangle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
