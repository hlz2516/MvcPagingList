using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcPagingListDesign.Models;
using MvcPagingListDesign.PagingList;

namespace MvcPagingListDesign.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StuContext _context;

        public HomeController(ILogger<HomeController> logger,StuContext context)
        {
            _logger = logger;
            _context = context;
            if (!context.Students.Any())
            {
                var stus = new List<Student>()
                {
                    new Student {Number="001",Name="张三"},
                    new Student {Number="002",Name="李四"},
                    new Student {Number="003",Name="王五"},
                    new Student {Number="004",Name="胡六"},
                    new Student {Number="005",Name="林七"},
                    new Student {Number="006",Name="潘八"},
                    new Student {Number="007",Name="张九"},
                    new Student {Number="008",Name="李十"},
                    new Student {Number="009",Name="王十一"},
                    new Student {Number="010",Name="胡十二"}
                };
                _context.AddRange(stus);
                _context.SaveChanges();
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TurnToPage(int pageIndex)
        {
            //初始化，由于是泛型类，所以传入你需要的领域类以及该领域类所在的context
            var pagelist = new MvcPagingList<StuContext, Student>(_context, 4); //参数二表示每页多少个
            //调用分页方法,参数一表示页索引即第几页，参数2表示你要根据领域类的哪个字段进行升序排序
            var stus = pagelist.GetPageTableByAsc(pageIndex, stu => stu.Number);
            //封装成ViewModel
            var model = new PageListViewModel
            {
                Students = stus,
                PageIndex = pageIndex,
                TotalPage = pagelist.TotalPage
            };
            //将model返回给页面
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
