namespace FTDTO.ApiResponse
{
    public class ResponseDTO<T>
    {
        public int Status { get; set; } = 1;
        public string Message { get; set; } = "Success";
        public T Data { get; set; }
    }
}
