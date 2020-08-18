using AutoMapper;
using CareebizExam.DbContext;
using CareebizExam.WebApi.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CareebizExam.WebApi.Services.Rectangle
{
    public class RectangleService : IRectangleService
    {
        private readonly CareebizExamDbContext _dbContext;
        private readonly IMapper _mapper;

        public RectangleService(IMapper mapper, CareebizExamDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }

        public async Task<RectangleViewModel> AddAsync(RectangleViewModel rect)
        {
            var rectangle = _mapper.Map<Models.Rectangle>(rect);

            await _dbContext.Rectangles.AddAsync(rectangle);

            if (await _dbContext.SaveChangesAsync() > 0)
            {
                return _mapper.Map<RectangleViewModel>(rectangle);
            }

            return null;
        }

        public async Task<int> UpdateAsync(RectangleViewModel rect)
        {
            var currentRect = await _dbContext.Rectangles
                .FirstOrDefaultAsync(x => x.Id == rect.Id);

            if (currentRect == null)
                return 0;

            currentRect.Name = rect.Name;
            currentRect.North = rect.North;
            currentRect.South = rect.South;
            currentRect.West = rect.West;

            _dbContext.Rectangles.Update(currentRect);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> RemoveAsync(int id)
        {
            var currentRect = await _dbContext.Rectangles
                .FirstOrDefaultAsync(x => x.Id == id);

            if (currentRect == null)
                return 0;

            _dbContext.Rectangles.Remove(currentRect);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<RectangleViewModel>> GetAll()
        {
            return await _mapper
                .ProjectTo<RectangleViewModel>(_dbContext.Rectangles)
                .ToListAsync();
        }

        public async Task<Models.Rectangle> Get(int id)
        {
            return await _dbContext.Rectangles.FirstAsync(x => x.Id == id);
        }
    }
}
