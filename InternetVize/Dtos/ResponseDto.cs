namespace InternetVize.Dtos
{
    public class ResponseDto
    {
        public ResponseDto() { }
        public ResponseDto(bool succeded, string? body) {
            Succeded = succeded;
            Body = body;
        }
        public string? Body {  get; set; }
        public bool Succeded {  get; set; }
    }
}
