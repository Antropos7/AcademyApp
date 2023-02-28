using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Data.Contexts.Repositories.Abstract
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Add(T group);
        List<T> GetAll();
        T Get(int id);
      
        void Update(T group);
        void Delete(T group);
    }
}
