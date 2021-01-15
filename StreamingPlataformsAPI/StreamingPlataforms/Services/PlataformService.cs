using AutoMapper;
using StreamingPlataforms.Exceptions;
using StreamingPlataforms.Data.Entities;
using StreamingPlataforms.Data.Repository;
using StreamingPlataforms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamingPlataforms.Services
{
    public class PlataformService : IPlataformService
    {
        private List<string> allowedSortValues = new List<string>() { "id", "plataformName", "fundationYear" };
        private IPlataformRepository repository;
        private readonly IMapper mapper;

        public PlataformService(IPlataformRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<PlataformModel> CreatePlataformAsync(PlataformModel newPlataform)
        {
            var plataformEntity = mapper.Map<PlataformEntity>(newPlataform);
            repository.CreatePlataform(plataformEntity);
            var res = await repository.SaveChangesAsync();
            if (res)
            {
                return mapper.Map<PlataformModel>(plataformEntity);
            }

            throw new Exception("Database exception");
        }

        public async Task<bool> DeletePlataformAsync(int id)
        {
            var dealerToDelete = await GetPlataformAsync(id);
            await repository.DeletePlataform(id);

            var deal = await repository.SaveChangesAsync();
            if (deal)
            {
                return true;
            }

            throw new Exception("Database Exception");

        }

        public async Task<PlataformModel> GetPlataformAsync(int id)
        {
            var plataformEntity = await repository.GetPlataformAsync(id);
            if (plataformEntity == null)
            {
                throw new NotFoundException($"the id :{id} not exist");
            }
            else
            {
                return mapper.Map<PlataformModel>(plataformEntity); ;
            }

        }

        public async Task<IEnumerable<PlataformModel>> GetPlataformsAsync(string orderBy, bool showCars)
        {
            if (!allowedSortValues.Contains(orderBy.ToLower()))
            {
                throw new BadOperationRequest($"bad sort value: { orderBy } allowed values are: { String.Join(",", allowedSortValues)}");
            }
            var dealerEntities = await repository.GetPlataformsAsync(orderBy, showCars);
            return mapper.Map<IEnumerable<PlataformModel>>(dealerEntities);
        }

        public async Task<bool> UpdatePlataformAsync(int id, PlataformModel dealer)
        {
            await GetPlataformAsync(id);
            dealer.Id = id;

            repository.UpdatePlataform(mapper.Map<PlataformEntity>(dealer));
            var deal = await repository.SaveChangesAsync();
            if (deal)
            {
                return true;
            }

            throw new Exception("Database Exception");
        }
    }
}
