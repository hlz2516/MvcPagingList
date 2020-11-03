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
    public class MvcPagingList<TContext,TClass>:IDisposable 
        where TContext : DbContext where TClass : class
    {
        private TContext _context;
        public int Count => _context.Set<TClass>().Count();
        public MvcPagingList(TContext context)
        {
            _context = context;
        }

        public IEnumerable<TClass> GetTable()
        {
            return _context.Set<TClass>().AsEnumerable();
        }

        public IEnumerable<TClass> GetTableByAsc<TKey>(Expression<Func<TClass,TKey>> expression)
        {
            return _context.Set<TClass>().OrderBy<TClass, TKey>(expression).AsEnumerable();
        }

        public IEnumerable<TClass> GetPageTable(int pageIndex,int pageSize)
        {
            return  _context.Set<TClass>().Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).AsEnumerable();
        }

        public IEnumerable<TClass> GetPageTableByAsc<TKey>
            (int pageIndex, int pageSize, Expression<Func<TClass, TKey>> expression)
        {
            return _context.Set<TClass>().OrderBy<TClass, TKey>(expression)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public IEnumerable<TClass> GetPageTableByDesc<TKey>
            (int pageIndex, int pageSize, Expression<Func<TClass, TKey>> expression)
        {
            return _context.Set<TClass>().OrderByDescending<TClass, TKey>(expression)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public IEnumerable<TClass> GetFirstPage(int pageSize)
        {
            return GetPageTable(1, pageSize);
        }

        public IEnumerable<TClass> GetFirstPageByAsc<TKey>
            (int pageSize, Expression<Func<TClass, TKey>> expression)
        {
            return GetPageTableByAsc(1, pageSize, expression);
        }

        public IEnumerable<TClass> GetFirstPageByDesc<TKey>
            (int pageSize, Expression<Func<TClass, TKey>> expression)
        {
            return GetPageTableByDesc(1, pageSize, expression);
        }

        public IEnumerable<TClass> GetLastPage(int pageSize)
        {
           int lastPageIndex = (int)Math.Ceiling(Count / (double)pageSize);
            return GetPageTable(lastPageIndex, pageSize);
        }

        public IEnumerable<TClass> GetLastPageByAsc<TKey>
            (int pageSize, Expression<Func<TClass, TKey>> expression)
        {
            int lastPageIndex = (int)Math.Ceiling(Count / (double)pageSize);
            return GetPageTableByAsc(lastPageIndex, pageSize, expression);
        }

        public IEnumerable<TClass> GetLastPageByDesc<TKey>
            (int pageSize, Expression<Func<TClass, TKey>> expression)
        {
            int lastPageIndex = (int)Math.Ceiling(Count / (double)pageSize);
            return GetPageTableByDesc(lastPageIndex, pageSize, expression);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
