using Microsoft.EntityFrameworkCore;
using StreamingPlataforms.Contexts;
using StreamingPlataforms.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamingPlataforms.Data.Repository
{
    public class PlataformRepository : IPlataformRepository
    {
        //private const EntityState unchanged = EntityState.Unchanged;
        private readonly PlataformDbContext dbContext;
        public PlataformRepository(PlataformDbContext context)
        {
            this.dbContext = context;
        }
        public void CreatePlataform(PlataformEntity newPlataform)
        {
            dbContext.Plataform.Add(newPlataform);
        }

        public void CreateSerie(SerieEntity newSerie)
        {
            
               //dbContext.Entry(newSerie.Plataform).State = EntityState.Unchanged;
            
                dbContext.Series.Add(newSerie);
        }

        public async  Task<bool> DeletePlataform(int id)
        {
            var plataformDelete = await dbContext.Plataform.FirstOrDefaultAsync(d => d.Id == id);
            dbContext.Plataform.Remove(plataformDelete);
            return true;
        }

        public async Task<bool> DeleteSerieAsync(int id)
        {
            var serie = await GetSerieAsync(id);
            dbContext.Series.Remove(serie);
            return true;
        }

        

        public async Task<PlataformEntity> GetPlataformAsync(int id, bool showSeries = false)
        {
            IQueryable<PlataformEntity> query = dbContext.Plataform;
            query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<PlataformEntity>> GetPlataformsAsync(string orderBy, bool showSeries = false)
        {
            IQueryable<PlataformEntity> query = dbContext.Plataform;

            var plataform = query.FirstOrDefault();

            switch (orderBy)
            {
                case "id":
                    query = query.OrderBy(r => r.Id);
                    break;
                case "plataformName":
                    query = query.OrderBy(r => r.PlataformName);
                    break;
                case "fundationYear":
                    query = query.OrderBy(r => r.FundationYear);
                    break;
                default:
                    break;
            }

            if (showSeries)
            {
                query = query.Include(d => d.Series);
            }

            query = query.AsNoTracking();

            return await query.ToArrayAsync();
        }
        
        public async Task<SerieEntity> GetSerieAsync(int id)
        {

            Console.WriteLine(id);
            IQueryable<SerieEntity> query = dbContext.Series;
            query = query.Include(c => c.Plataform);
            query = query.AsNoTracking();
            return await query.SingleOrDefaultAsync(c => c.SerieId == id);
        }

        public async Task<IEnumerable<SerieEntity>> GetSeriesAsync(int plataformId)
        {
            IQueryable<SerieEntity> query = dbContext.Series;
            query = query.Where(r => r.PlataformId==plataformId);
            query = query.AsNoTracking();
            return await query.ToArrayAsync();
        }
        public  async Task<bool> SaveChangesAsync()
        {
            var res = await dbContext.SaveChangesAsync();
            return res > 0;
        }

        public bool UpdatePlataform(PlataformEntity plataform)
        {
            dbContext.Plataform.Update(plataform);
            return true;
        }

        public bool UpdateSerie(SerieEntity serie)
        {
            if (serie == null) {
                throw new Exception("Entity Exception");
            }
            //dbContext.Entry(serie.Plataform).State = EntityState.Unchanged;
            dbContext.Series.Update(serie);
            return true;
        }
    }
}
