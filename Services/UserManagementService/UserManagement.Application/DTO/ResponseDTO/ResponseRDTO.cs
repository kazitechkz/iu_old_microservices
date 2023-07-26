using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.DTO.ResponseDTO
{
    public class ResponseRDTO<T>
    {
        public int StatusCode { get; set; }
        public int Success { get; set; }

        public string? Message { get; set; }

        public string? Detail { get; set; }

        public T Data {get;set;}


    }
}
