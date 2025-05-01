namespace SharedLibrary.Dtos
{
    public class ErrorDto
    {
        // 1den fazla hata done bilmek icin list kullancaz
        public List<string> Errors { get; private set; }
        public bool IsShow { get; private set; } // client  erroru usere  gostermek=true , eksi =false

        public ErrorDto()
        {
            Errors = new List<string>();
        }

        public ErrorDto(string error, bool isShow)
        {
            Errors = new List<string>();  // Errors listini hər zaman yenidən yaratdıq
            Errors.Add(error);  // Erroru Errors listinə əlavə et
            IsShow = isShow;  // Burada isShow-u IsShow property-sinə təyin edirik
        }

        public ErrorDto(List<string> errors, bool isShow)
        {
            Errors = errors;  // Burada parametr olaraq gələn 'errors' listini təyin edirik
            IsShow = isShow;
        }
    }
}
