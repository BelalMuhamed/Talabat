using CoreLayer.Entities;
using CoreLayer.RepoContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Contract
{
     public class GenericRepo<T>:IGenericRepo<T> where T : BaseEntity
    {
    }
}
