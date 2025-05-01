using AuthServer.Core.DTOs.CreateUser;
using AuthServer.Core.DTOs.UserApp;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.Services
{
    public interface IUserService
    {
        Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);   // yeni uyelere kaydetme
        Task<Response<UserAppDto>> GetUserByNameAsync(string userName);   // username gore  databaseden kullanici bilgilerini geri dondurme
    }
}


// userle ilgili repository olusturmadik cunki identity kitabxanasindan gelir