using CoreLayer.Entities;
using CoreLayer.RepoContract;
using CoreLayer.Specifications;
using Microsoft.EntityFrameworkCore;
using RepoLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Contract
{
     public class GenericRepo<T>:IGenericRepo<T> where T : BaseEntity
    {
        private readonly AppDbContext context;

        public GenericRepo(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        

        public async Task<T?> GetAsync(int Id)
        {
            if (typeof(T) == typeof(Product))
            {
                return await context.Products.Include(p => p.Brand).Include(p => p.Category).FirstOrDefaultAsync(t => t.Id == Id) as T;
            }
            return await context.Set<T>().FirstOrDefaultAsync(t => t.Id == Id);

        }
        
        public async Task<T?> GetAsyncBySpec(IspecificationContract<T> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsyncBySpec(IspecificationContract<T> spec)
        {
            return await ApplySpecifications(spec).ToListAsync();
        }
       private IQueryable<T> ApplySpecifications(IspecificationContract<T> spec)
        {
            return QueryEvaluator<T>.MakeQuery(context.Set<T>(), spec);
        }

        public async Task<int> GetCountAsync(IspecificationContract<T> spec)
        {
            return await ApplySpecifications(spec).CountAsync();

        }
    }
}
