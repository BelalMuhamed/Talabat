using CoreLayer.Entities;
using CoreLayer.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.RepoContract
{
   public interface IGenericRepo<T> where T:BaseEntity
    
    {
        public Task<T?> GetAsync(int Id);
        public Task<IReadOnlyList<T>> GetAllAsync();
        public Task<T?> GetAsyncBySpec(IspecificationContract<T> spec);
        public Task<IReadOnlyList<T>> GetAllAsyncBySpec(IspecificationContract<T> spec);
        public Task<int> GetCountAsync(IspecificationContract<T> spec);
    }
}
