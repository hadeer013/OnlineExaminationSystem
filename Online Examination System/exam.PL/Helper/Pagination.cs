﻿using exam.DAL.Entities;

namespace exam.PL.Helper
{
    public class Pagination<T> where T : BaseEntity
    {
        public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; } = 0;
        public IReadOnlyList<T> Data { get; set; }
    }
}
