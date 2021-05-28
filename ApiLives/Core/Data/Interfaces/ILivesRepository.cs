using ApiLives.Core.Domain.Entities.ApiLives;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLives.Services.Interfaces
{
    public interface ILiveRepository
    {
        Task<Live> GetById(int id);
        Task<List<Live>> GetByToday();
        Task<List<Live>> GetByNext();
        Task<List<Live>> GetByPrevious();
        Task<List<Live>> GetAll();
        Task<Live> Add(Live model);
        Task<Live> Update(Live model);
        Task DeleteById(int id);
    }
}
