using System.Collections.Generic;
using System.Threading.Tasks;
using CareebizExam.WebApi.ViewModels;

namespace CareebizExam.WebApi.Services.Rectangle
{
    public interface IRectangleService
    {
        Task<RectangleViewModel> AddAsync(RectangleViewModel rect);
        Task<IEnumerable<RectangleViewModel>> GetAll();
        Task<int> RemoveAsync(int id);
        Task<int> UpdateAsync(RectangleViewModel rect);

        Task<Models.Rectangle> Get(int id);
    }
}