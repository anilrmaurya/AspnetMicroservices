using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        private readonly IConfiguration configuration;

        public CatalogContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            MongoClient client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);
            IMongoDatabase database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
            Products = database.GetCollection<Product>(configuration["DatabaseSettings:CollectionName"]);
            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
