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
   public interface IGenericService<TEntity,TDto> where TEntity:class where TDto:class
    {
        Task<Response<TDto>> GetByIdAsync(int id); // apide kullanicamiz datayi donucez
        Task<Response<IEnumerable<TDto>>> GetAllAsync();
       Task< Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate); // apide islem yapmiyacagiz diye ienumarable done bilirz
        Task <Response<TDto>> AddAsync(TDto dto);
       Task<Response<NoDataDto>>Remove(int id); 
        Task<Response<NoDataDto>>Update(TDto dto,int id);

    }
}
