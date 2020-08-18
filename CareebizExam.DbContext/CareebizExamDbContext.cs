using CareebizExam.Models;
using Microsoft.EntityFrameworkCore;

namespace CareebizExam.DbContext
{
    public class CareebizExamDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public CareebizExamDbContext(DbContextOptions<CareebizExamDbContext> options)
           : base(options)
        {

        }

        public DbSet<Rectangle> Rectangles { get; set; }
    }
}
