using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UserManagementService.API.Application.DTO.ResponseDTO
{
    public class Pagination<T> where T : class
    {
        public Pagination(int pageIndex, int pageSize, int count, ICollection<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            LastPage = PageSize > 0 ? (Count / PageSize) + 1 : 1;
            Data = data;
        }

        public int FirstPage { get; set; } = 1;
        public int LastPage { get; set; } = 1;

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public ICollection<T> Data { get; set; }
    }
}
