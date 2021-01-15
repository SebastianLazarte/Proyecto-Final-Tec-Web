using StreamingPlataforms.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamingPlataforms.Data.Repository
{
    public interface IPlataformRepository
    {
        Task<PlataformEntity> GetPlataformAsync(int id, bool showSeries = false);
        Task<IEnumerable<PlataformEntity>> GetPlataformsAsync(string orderBy, bool showSeries = false);
        void CreatePlataform(PlataformEntity newPlataform);
        bool UpdatePlataform(PlataformEntity plataform);
        Task<bool> DeletePlataform(int id);
        //Series
        Task<SerieEntity> GetSerieAsync(int id);
        Task<IEnumerable<SerieEntity>> GetSeriesAsync(int plataformId);
        void CreateSerie(SerieEntity newSerie);
        bool UpdateSerie(SerieEntity serie);
        Task<bool> DeleteSerieAsync(int id);
        //Task<IEnumerable<SerieEntity>>GetByPlataformIdAsync(int plataformId);


        Task<bool> SaveChangesAsync();
    }
}
