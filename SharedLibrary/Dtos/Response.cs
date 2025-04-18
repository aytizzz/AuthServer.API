using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Dtos
{

    
    public class Response<T> where T:class
    {
        // apiye kullanca datayi donmek
        public T Data { get; private set; }
        public int StatusCode { get; private set; }
        public bool IsSuccesful { get; set; }
        public ErrorDto Error { get; set; }
                                                  // update delete yaptigimizda geriye data donmeyimize gerek yokk
        public static Response<T>Success (T data,int statusCode)
        {
            return new Response<T> { Data = data, StatusCode = statusCode };
        }
        public static Response<T>Success(int statusCode)
        {
            return new Response<T> { Data=default,StatusCode = statusCode };// data yazmasaydi nolurdu
        }
        public static Response <T> Fail(ErrorDto errorDto,int statusCode,bool isShow)
        {
            return new Response<T>
            {
                StatusCode = statusCode,
                Error = errorDto
            };
        }


        public static Response<T> Fail(string errorMessage,int statusCode,bool isShow)
        {
            return new Response<T>
            {
                ///yazilmalii
            };
        }
    }
}
