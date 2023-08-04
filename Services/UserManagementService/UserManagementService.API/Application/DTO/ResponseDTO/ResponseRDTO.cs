namespace UserManagementService.API.Application.DTO.ResponseDTO
{
    public class ResponseRDTO<T>
    {
        public int StatusCode { get; set; }
        public bool Success { get; set; }

        public string? Message { get; set; }

        public string? Detail { get; set; }

        public T Data { get; set; }

    }
}
