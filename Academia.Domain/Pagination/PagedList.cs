using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Domain.Pagination
{
    public class PagedList<T> 
    {
        public PagedList(IEnumerable<T>items,int currentPage, int pageSize, int totalCount)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount /(double) pageSize);
            Items = items;
        }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<T> Items { get; set; } = [];
    }
}
