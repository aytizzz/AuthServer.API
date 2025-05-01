using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity:class
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();

       // IQueryable<TEntity> GetAllAsync();//? 
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);   // where(x=>x.id>5) x=tentity x.id>5 bool donur 

        Task AddAsync(TEntity entity);
        void Remove(TEntity entity); // niye asinxron yoxdu
        TEntity Update(TEntity entity);
    }
}

// biz get all coxlu islemler gorenden sonra en axirda aldgimiz sonuca gore bir sonuc donurukse iquerabyle yaxsidi o o en sonda veritabanina yazir