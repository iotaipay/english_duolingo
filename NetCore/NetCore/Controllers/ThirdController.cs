using Microsoft.AspNetCore.Mvc;
using NetCore.Utility.Filters;

namespace NetCore.Controllers
{
    public class ThirdController : Controller
    {
        //定义内部属性
        private readonly ILogger<ThirdController> _Logger;
        private readonly ILoggerFactory _LoggerFactor;

        //定义构造函数
        public ThirdController(ILogger<ThirdController> logger, ILoggerFactory loggerFactory)
        {
            this._Logger = logger;
            this._Logger.LogInformation($"{this.GetType().Name} 被构造了----_Logger");

            this._LoggerFactor = loggerFactory;

            ILogger<ThirdController> _Logger2 = this._LoggerFactor.CreateLogger<ThirdController>();
            _Logger2.LogInformation($"{this.GetType().Name} 被构造了---_Logger2");

            Console.WriteLine($"{this.GetType().FullName} 被构造了。。。");

        }


        #region ResourceFilter
        //自定义的继承了接口 IResourceFilter的一个特性
        [CustomCacheResourceFilterAttribute]
        //接口内部 Index方法
        public IActionResult Index()
        {
            //定义一个缓存区域
            //请求来了后，根据缓存标识----作判断
            //如果有缓存，就返回缓存的值
            //如果没缓存，就作相应的计算
            //计算结果保存到缓存内部

            ViewBag.Date = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");
             
            return View();
        }

        [CustomCacheAsyncResourceFilter]
        public IActionResult Index1()
        {
            ViewBag.Date = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");
            Console.WriteLine("这里是Index1的执行");
            return View();
        }


        #endregion

        #region ActionFilter
        [CustomActionFilterAttribute]
        public IActionResult Index2()
        {

            ViewBag.Date = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");
            Console.WriteLine("这里是Index2的执行");
            return View();
        }
        /* [CustomLogActionFilterAttribute] */  //IOC容器的问题
        [TypeFilter(typeof(CustomLogActionFilterAttribute))]  //解决方式1
        /* [ServiceFilter(typeof(CustomLogActionFilterAttribute))] */  //解决方式2
        public IActionResult Index3(int id)
        {
            ViewBag.user = Newtonsoft.Json.JsonConvert.SerializeObject(new {
                Id = id,
                Name = "Richard --- ViewBag",
                Age = 34
            });
            ViewData["UserInfo"] = Newtonsoft.Json.JsonConvert.SerializeObject(new { 
                Id = id,
                Name = "Richard --- ViewData",
                Age = 34
            });

            object description = "欢迎大家来到 Richard 老师的视频课";

            return View(description);
        }
        [TypeFilter(typeof(CustomLogAsyncActionFilterAttribute))]
        public IActionResult Index4(int id)
        {
            ViewBag.user = Newtonsoft.Json.JsonConvert.SerializeObject(new { 
                Id = id,
                Name = "Payment --- ViewBag",
                Age = 33
            });

            ViewData["UserInfo"] = Newtonsoft.Json.JsonConvert.SerializeObject(new { 
                Id = id,
                Name = "Payment --- ViewData",
                Age = 33
            });

            object desription = "这是 Payment 练习的一个 Index4 方法";

            return View(desription);
        }
        #endregion

        #region ResultFilter

        [TypeFilter(typeof(CustomResultFilterAttribute))]
        public IActionResult Index5()
        {

            return View();
        }

        [TypeFilter(typeof(CustomResultFilterAttribute))]
        public IActionResult Index6()
        {
          

            return Json(new { 
                Id = 1024,
                Name = "PayMent",
                Age = 33
            });
        }

        [TypeFilter(typeof(CustomAsyncResultFilterAttribute))]
        public IActionResult Index7()
        {
            return Json(new { 
                Title = "Index7",
                Id = 1024,
                Name = "PayMent",
                Age = 33
            });
        }
        #endregion
    }
}
