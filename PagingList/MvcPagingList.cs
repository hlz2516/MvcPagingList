using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MvcPagingListDesign.PagingList
{
    /// <summary>
    /// 
    /// </summary>
    public class MvcPagingList<TContext,TClass> where TContext : DbContext where TClass : class
    {
        private TContext _context;
        //public int PageIndex { get;private set; }
        public int PageSize { get;private set; }
        public int TotalCount { get;private set; }
        public int TotalPage { get; private set; }
        public MvcPagingList(TContext context,int pageSize)
        {
            _context = context;
            TotalCount = _context.Set<TClass>().Count();
            PageSize = pageSize;
            TotalPage = (int)Math.Ceiling(TotalCount / (double)PageSize);
        }

        public IEnumerable<TClass> GetTable()
        {
            return _context.Set<TClass>().AsEnumerable();
        }

        public IEnumerable<TClass> GetTableByAsc<TKey>(Expression<Func<TClass,TKey>> expression)
        {
            return _context.Set<TClass>().OrderBy<TClass, TKey>(expression).AsEnumerable();
        }

        public IEnumerable<TClass> GetPageTable(int pageIndex)
        {
            return  _context.Set<TClass>().Skip((pageIndex - 1) * PageSize)
                .Take(PageSize).AsEnumerable();
        }

        public IEnumerable<TClass> GetPageTableByAsc<TKey>
            (ref int pageIndex, Expression<Func<TClass, TKey>> expression)
        {
            return _context.Set<TClass>().OrderBy<TClass, TKey>(expression)
                .Skip((pageIndex - 1) * PageSize).Take(PageSize).AsEnumerable();
        }

        public IEnumerable<TClass> GetPageTableByDesc<TKey>
            (int pageIndex, Expression<Func<TClass, TKey>> expression)
        {
            return _context.Set<TClass>().OrderByDescending<TClass, TKey>(expression)
                .Skip((pageIndex - 1) * PageSize).Take(PageSize).AsEnumerable();
        }
    }
}
