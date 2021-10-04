using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace University.BL.Services.Implements
{
    public class CosmosService
    {
        private static CosmosClient _cosmosClient;

        public CosmosService(string cosmosConfigKey)
        {
            _cosmosClient = new CosmosClient(cosmosConfigKey);
        }

        public async Task<int> Insert(string database,
            string container,
            dynamic entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            var _container = _cosmosClient.GetDatabase(database).GetContainer(container);
            var result = await _container.CreateItemAsync(entity);
            return (int)result.StatusCode;
        }

        public async Task<int> Update(string database,
            string container,
            dynamic entity)
        {
            var _container = _cosmosClient.GetDatabase(database).GetContainer(container);
            var result = await _container.ReplaceItemAsync(entity, entity.Id);
            return (int)result.StatusCode;
        }

        public async Task<int> Delete(string database,
            string container,
            string id)
        {
            var _container = _cosmosClient.GetDatabase(database).GetContainer(container);
            var result = await _container.DeleteItemAsync<object>(id, new PartitionKey(id));
            return (int)result.StatusCode;
        }

        public async Task<IEnumerable<T>> GetAll<T>(string database,
            string container)
        {
            var data = new List<T>();
            var _container = _cosmosClient.GetDatabase(database).GetContainer(container);
            var query = _container.GetItemLinqQueryable<T>().ToFeedIterator();

            while (query.HasMoreResults)
                foreach (var item in await query.ReadNextAsync())
                    data.Add(item);

            return data;
        }

        public async Task<T> GetById<T>(string database,
            string container,
            string id)
        {            
            var _container = _cosmosClient.GetDatabase(database).GetContainer(container);
            var query = await _container.ReadItemAsync<T>(id, new PartitionKey(id));            

            return query.Resource;
        }
    }
}
