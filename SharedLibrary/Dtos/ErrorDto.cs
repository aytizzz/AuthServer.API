using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Dtos
{
    public class ErrorDto
    {
        // 1den fazla hata done bilmek icin list kullancaz
        public List<string> Errors { get; private set; }
        public bool IsShow { get; private set; } // bazi hatalar ola bilirki sadece cliente donmek isteriz user-e gostermek istemeyiz onun icin


        public ErrorDto()
        {
            Errors = new List<string>();
        }

        public ErrorDto(string error,bool isShow )
        {
            Errors.Add(error);
            isShow = true;

        }
        public ErrorDto(List<string> errors,bool isShow)
        {
            Errors = Errors;
            IsShow = isShow;
        }


    }
}
