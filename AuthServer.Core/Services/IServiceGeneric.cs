using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

// /*service katmaninda core katmanindan gelen datayi dto cevirib ele gondermek*/
// struktur: burda duzeldib apiye gondermek
namespace AuthServer.Core.Services
{
   public interface IServiceGeneric<TEntity,TDto> where TEntity:class where TDto:class
    {
        Task<Response<TDto>> GetByIdAsync(int id); // apide kullanicamiz datayi donucez
        Task<Response<IEnumerable<TDto>>> GetAllAsync();
        Response<IEnumerable<TDto>> Where(Expression<Func<TEntity, bool>> predicate); // apide islem yapmiyacagiz diye ienumarable done bilirz
        Task <Response<TDto>> AddAsync(TEntity entity);
       Task<Response<NoDataDto>>Remove(TEntity entity); // geriye bos klass donuruk
        Task<Response<NoDataDto>>Update(TEntity entity);

    }
}
