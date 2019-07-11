using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MES.Table;
namespace MES.Config
{
    public class PageHelper<T> 
    {
        public List<T> List { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get;  set; }
        public int TotalCount { get;  set; }
        public int TotalPages { get;  set; }
        public PageHelper(IQueryable<T> source, int pageIndex, int pageSize)
        {
            List<T> dataList = new List<T>();
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = source.Count();
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            dataList.AddRange(source.Skip(PageIndex * PageSize).Take(PageSize));
            List = dataList;
        }
        public PageHelper(IEnumerable<T> toolEquipment, int pageIndex, int pageSize)
        {
            List<T> dataList = new List<T>();
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = toolEquipment.Count();
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            dataList.AddRange(toolEquipment.Skip(PageIndex * PageSize).Take(PageSize));
            List = dataList;
        }
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 0);
            }
        }
        public bool HasNextPage
        {
            get
            {
                return (PageIndex + 1 < TotalPages);
            }
        }
    }
    
}