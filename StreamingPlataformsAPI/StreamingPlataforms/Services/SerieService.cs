using AutoMapper;
using StreamingPlataforms.Exceptions;
using Microsoft.AspNetCore.Mvc;
using StreamingPlataforms.Data.Entities;
using StreamingPlataforms.Data.Repository;
using StreamingPlataforms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamingPlataforms.Services
{
    public class SerieService : ISerieService
    {
        private IPlataformRepository repository;
        private IMapper mapper;

        public SerieService(IPlataformRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<SerieModel> CreateSerieAsync(int PlataformId, SerieModel newSerie)
        {
            await ValidatePlataformAsync(PlataformId);
            var num = Convert.ToString(PlataformId);
            newSerie.PlataformId = num;
            
            var serieEntity = mapper.Map<SerieEntity>(newSerie);

            repository.CreateSerie(serieEntity);

            var deal = await repository.SaveChangesAsync();
            if (deal!=false)
            {
                return mapper.Map<SerieModel>(serieEntity);
            }

            throw new Exception("Database Exception");
        }

        public async Task<bool> DeleteSerieAsync(int PlataformId, int SerieId)
        {
            var serie = await GetSerieAsync(PlataformId, SerieId);
            if (serie == null)
            {
                throw new NotFoundException($"The serie with {SerieId} does not exist in the plataform.");
            }
            await repository.DeleteSerieAsync(SerieId);
            var deal = await repository.SaveChangesAsync();

            if (deal)
            {
                return true;
            }

            throw new Exception("Database Exception");
        }

        public async Task<SerieModel> GetSerieAsync(int PlataformId, int id)
        {
            await ValidatePlataformAsync(PlataformId);
            var serie = await repository.GetSerieAsync(id);

            if (serie == null ||serie.Plataform.Id != PlataformId)
            {
                throw new NotFoundException($"The id :{id} doesn't exist.");
            }

            return mapper.Map<SerieModel>(serie);
        }

        public async Task<IEnumerable<SerieModel>> GetSeriesAsync(int PlataformId)
        {
            await ValidatePlataformAsync(PlataformId);
            return mapper.Map<IEnumerable<SerieModel>>(await repository.GetSeriesAsync(PlataformId));
        }

        public async Task<bool> UpdateSerieAsync(int PlataformId, int serieId, SerieModel Serie)
        {
                Serie.SerieId = serieId;
            await GetSerieAsync(PlataformId, serieId);

            repository.UpdateSerie(mapper.Map<SerieEntity>(Serie));

            var deal = await repository.SaveChangesAsync();
            if (deal)
            {
                return true;
            }

            throw new Exception("Database Exception");
        }

        private async Task ValidatePlataformAsync(int plataformId)
        {
            var plataformEntity = await repository.GetPlataformAsync(plataformId);
            if (plataformEntity == null)
            {
                throw new NotFoundException($"the id :{plataformId} does not exist for plataform");
            }
        }
    }
}
