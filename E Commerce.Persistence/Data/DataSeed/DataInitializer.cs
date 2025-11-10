using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.DataSeed
{
    public class DataInitializer : IDataInitializer
    {
        private readonly StoreDbContext _dbContexts;

        public DataInitializer(StoreDbContext dbContexts)
        {
            _dbContexts = dbContexts;
        }


        public async Task InitializeAsync()
        {

            try
            {
                var hasProducts =  await _dbContexts.Products.AnyAsync();
                var hasBrands = await   _dbContexts.ProductBrands.AnyAsync();
                var hasTypes = await _dbContexts.ProductTypes.AnyAsync();


                if (hasProducts && hasBrands && hasTypes) return;
                if (!hasBrands)
                {
                   await  SeedDataaFromJSONAsync<ProductBrand, int>("brands.json", _dbContexts.ProductBrands);


                }
                if (!hasTypes)
                {
                 await SeedDataaFromJSONAsync<ProductType, int>("types.json", _dbContexts.ProductTypes);
               await     _dbContexts.SaveChangesAsync();
                }
                if (!hasProducts)
                {
                  await  SeedDataaFromJSONAsync<Product, int>("products.json", _dbContexts.Products);
               await     _dbContexts.SaveChangesAsync();
                }
            }

            catch(Exception ex) 
            {

                Console.WriteLine($"Data Seedin Faild{ex}");
            
            }

            
            
            
        }
            


    public async Task SeedDataaFromJSONAsync<T, TKey>(string fileName, DbSet<T> dbset) where T : BaseEntity<TKey>
        {

            var filePath = @"../E-Commerce.Persistence/Data/DataSeed/SeedData/" + fileName;
            if (!File.Exists(filePath)) throw new FileNotFoundException();

            try
            {
            
             using var dataStream= File.OpenRead(filePath);
                var Data = await JsonSerializer.DeserializeAsync<List<T>>(dataStream, new JsonSerializerOptions()
                {

                    PropertyNameCaseInsensitive = true


                });


                if (Data != null) {

                    await dbset.AddRangeAsync(Data);
                
                
                }
            
            }
            catch(Exception ex)
            {

                Console.WriteLine($"Error while Reading Json File{ex}");
                return;
            
            }
            
            

        }
    }    
}
