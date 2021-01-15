using StreamingPlataforms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamingPlataforms.Services
{
    public interface IPlataformService
    {
        Task<PlataformModel> GetPlataformAsync(int id);
        Task<IEnumerable<PlataformModel>> GetPlataformsAsync(string orderBy = "id", bool showSeries = false);
        Task<PlataformModel> CreatePlataformAsync(PlataformModel newPlataform);
        Task<bool> UpdatePlataformAsync(int id, PlataformModel plataform);
        Task<bool> DeletePlataformAsync(int id);
    }
}
