using Microsoft.AspNetCore.Mvc;

namespace NetCore.Controllers
{
    public class SecondController : Controller
    {
        //定义只读 ILogger 相关属性
        private readonly ILogger<SecondController> _Logger;
        private readonly ILoggerFactory _LoggerFactory;

        //构造函数注入
        public SecondController(ILogger<SecondController> logger, ILoggerFactory loggerFactory)
        {
            this._Logger = logger;
            
            this._Logger.LogInformation($"{ this.GetType().Name} 被构造了 _Logger");


            this._LoggerFactory = loggerFactory;
            ILogger<SecondController> _Logger2 =  this._LoggerFactory.CreateLogger<SecondController>();
            _Logger2.LogInformation($"{this.GetType().Name} 被构造了 _Logger2");
        }


        public IActionResult Index()
        {
            ILogger<SecondController> _Logger3 = this._LoggerFactory.CreateLogger<SecondController>();

            _Logger3.LogInformation($"{this.GetType().Name} 被执行了 _Logger3");

            this._Logger.LogInformation($"{this.GetType().Name} 被执行了 _Logger");
            return View();
        }

        public IActionResult Level()
        {
            _Logger.LogDebug("this is _Logger.LogDebug");
            _Logger.LogInformation("this is _Logger.LogInformation");
            _Logger.LogWarning("this is _Logger.LogWarning");
            _Logger.LogError("this is _LoggerLogError");
            _Logger.LogTrace("this is _LoggerTrace");
            _Logger.LogCritical("this is _LoggerCritical");
            return new JsonResult(new { Success = true});
        }
    }
}
