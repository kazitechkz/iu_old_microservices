﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectService.Application.DTO.ResponseDTO
{
    public class PagingRDTO<T>
    {
        public PagingRDTO(int pageIndex, int pageSize, int count, T data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public T Data { get; set; }
    }
}
