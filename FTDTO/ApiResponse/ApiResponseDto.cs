namespace FTDTO.ApiResponse
{
    public class ApiResponseDto
    {
        public int Status { get; set; }
        public dynamic ResponseData { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
        public bool IsSuccessful { get; set; }
    }

}
