using SQ_DB_Framework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SQ_DB_Framework.EntityConfigures
{
    [DataContract]
    public class PageHelper<T> where T : EntityBase
    {
        [DataMember]
        public List<T> List { get; set; }
        [DataMember]
        public int PageIndex { get; set; }
        [DataMember]
        public int PageSize { get; set; }
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public int TotalPages { get; set; }
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
        public PageHelper(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            List<T> dataList = new List<T>();
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = source.Count();
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            dataList.AddRange(source.Skip(PageIndex * PageSize).Take(PageSize));
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
