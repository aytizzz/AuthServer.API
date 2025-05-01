using AuthServer.Core.Repositories;
using AuthServer.Core.Services;
using AuthServer.Core.UnitofWork;
using AuthServer.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


// /*service katmaninda core katmanindan gelen datayi dto cevirib ele gondermek*/
// struktur: burda duzeldib apiye gondermek

namespace AuthServer.Service.Services
{
    public class GenericService<TEntity, TDto> : IGenericService<TEntity, TDto> where TEntity : class where TDto : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly GenericRepository<TEntity> _genericRepository;

        public GenericService(IUnitOfWork unitOfWork, GenericRepository<TEntity> genericRepository)
        {
            _unitOfWork = unitOfWork;
            _genericRepository = genericRepository;
        }

        public async Task<Response<TDto>> AddAsync(TDto dto)
        {
            var newEntity = ObjectMapper.mapper.Map<TEntity>(dto); 
            await _genericRepository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();

            var newDto = ObjectMapper.mapper.Map<TDto>(newEntity);
            return Response<TDto>.Success(newDto, 200);

        }

        public async Task<Response<IEnumerable<TDto>>> GetAllAsync()
        {
            var products = ObjectMapper.mapper.Map<List<TDto>>(await _genericRepository.GetAllAsync());
            return Response<IEnumerable<TDto>>.Success(products, 200);
        }

        public async Task<Response<TDto>> GetByIdAsync(int id)
        {
            var product = await _genericRepository.GetByIdAsync(id);
            if (product != null)
            {
                return Response<TDto>.Fail("Id not found", 404, true);//cliente gostersin
            }
            return Response<TDto>.Success(ObjectMapper.mapper.Map<TDto>(product), 200);
        }

        public async Task<Response<NoDataDto>> Remove(int id)
        {
            var IsexistEntity = await _genericRepository.GetByIdAsync(id);
            if (IsexistEntity != null)
            {
                return Response<NoDataDto>.Fail("Id not found",404,true);
            }
            
                 _genericRepository.Remove(IsexistEntity);
                 await _unitOfWork.CommitAsync();
                 return Response<NoDataDto>.Success(204);
        }

        public async Task<Response<NoDataDto>> Update(TDto dto,int id)
        {
            var IsexistEntity = await _genericRepository.GetByIdAsync(id);
            if (IsexistEntity != null)
            {
                return Response<NoDataDto>.Fail("Id not found", 404, true);
            }
            var updatedEntity = ObjectMapper.mapper.Map<TEntity>(dto);
            _genericRepository.Update(updatedEntity);
            _unitOfWork.CommitAsync();
             // update edende tdto dondurmeme 
            //204-No Content =>Response bodysinde hec bir data olmuyacaq

            return Response<NoDataDto>.Success(204);
        }

        public async Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            //var list = _genericRepository.Where(predicate);// geriye Iquerayble donur
            //return Response<IEnumerable<TDto>>.Success(ObjectMapper.mapper.Map<IEnumerable<TDto>>(await list.ToListAsync()), 200);

            var queryable = _genericRepository.Where(predicate); // IQueryable<TEntity> dönür
            var entities = await queryable.ToListAsync(); // Async şəkildə listə çeviririk
            var dtos = ObjectMapper.mapper.Map<IEnumerable<TDto>>(entities); // Map edirik
            return Response<IEnumerable<TDto>>.Success(dtos, 200); // Uğurlu cavab qaytarırıq


        }

      
    }
}
