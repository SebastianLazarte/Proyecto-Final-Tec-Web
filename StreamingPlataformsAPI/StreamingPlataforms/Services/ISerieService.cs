using StreamingPlataforms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamingPlataforms.Services
{
    public interface ISerieService
    {
        Task<SerieModel> GetSerieAsync(int PlataformId, int id);
        Task<IEnumerable<SerieModel>> GetSeriesAsync(int PlataformId);
        Task<SerieModel> CreateSerieAsync(int PlataformId, SerieModel newSerie);
        Task<bool> UpdateSerieAsync(int PlataformId, int id, SerieModel Serie);
        Task<bool> DeleteSerieAsync(int PlataformId, int id);
        //Task<IEnumerable<SerieModel>> GetByPlataformIdAsync(int plataformId);
    }
}
