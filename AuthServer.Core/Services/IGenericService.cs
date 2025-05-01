using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace AuthServer.Core.Services
{
   public interface IGenericService<TEntity,TDto> where TEntity:class where TDto:class
    {
        // apide kullanicamiz datayi donucez
        Task<Response<TDto>> GetByIdAsync(int id);   
        Task<Response<IEnumerable<TDto>>> GetAllAsync();
       Task< Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate); // apide islem yapmiyacagiz diye ienumarable done bilirz
        Task <Response<TDto>> AddAsync(TDto dto); // veritabanindaki datayi dtoya donusturub gondercyik
       Task<Response<NoDataDto>>Remove(int id); 
        Task<Response<NoDataDto>>Update(TDto dto,int id);

    }
} 
// dtoya cevirme isleri burda gedecek -burda direkt apiye kullancamiz datayi donecyik 
// bu servis hem entity hemde dto alir ve geriye dto dondurur