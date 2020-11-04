using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MvcPagingListDesign.PagingList
{
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
        /// <summary>
        /// 不排序获取对应页索引的分页数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public IEnumerable<TClass> GetPageTable(int pageIndex)
        {
            return  _context.Set<TClass>().Skip((pageIndex - 1) * PageSize)
                .Take(PageSize).AsEnumerable();
        }
        /// <summary>
        /// 根据TKey字段进行升序，再获取对应页索引的分页数据
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IEnumerable<TClass> GetPageTableByAsc<TKey>
            (int pageIndex, Expression<Func<TClass, TKey>> expression)
        {
            return _context.Set<TClass>().OrderBy<TClass, TKey>(expression)
                .Skip((pageIndex - 1) * PageSize).Take(PageSize).AsEnumerable();
        }
        /// <summary>
        /// 根据TKey字段进行降序，再获取对应页索引的分页数据
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IEnumerable<TClass> GetPageTableByDesc<TKey>
            (int pageIndex, Expression<Func<TClass, TKey>> expression)
        {
            return _context.Set<TClass>().OrderByDescending<TClass, TKey>(expression)
                .Skip((pageIndex - 1) * PageSize).Take(PageSize).AsEnumerable();
        }
    }
}
