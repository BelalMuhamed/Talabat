using CoreLayer.Entities;
using CoreLayer.RepoContract;
using RepoLayer.Contract;
using RepoLayer.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.UnitOfWork
{
    public class UnitOfWork
    {
        private readonly AppDbContext context;
        private readonly ConcurrentDictionary<string, object> Repos;
        public UnitOfWork(AppDbContext _context)
        {

            context = _context;
            Repos = new();
        }
        public IGenericRepo<T> Repository<T>() where T : BaseEntity
        {


            return (IGenericRepo<T>)Repos.GetOrAdd(typeof(T).Name, new GenericRepo<T>(context));
        }


    }
}
