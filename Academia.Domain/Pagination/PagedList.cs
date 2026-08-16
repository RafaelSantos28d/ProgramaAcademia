using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Range(1,int.MaxValue,ErrorMessage ="O número deve ser maior que 0")]
        public int CurrentPage { get; set; }
        [Range(1, 50, ErrorMessage = "O número deve ser maior que 0 no máximo 50")]
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<T> Items { get; set; } = [];
    }
}
