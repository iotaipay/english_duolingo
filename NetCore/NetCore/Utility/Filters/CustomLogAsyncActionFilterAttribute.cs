using Microsoft.AspNetCore.Mvc.Filters;

namespace NetCore.Utility.Filters
{
    public class CustomLogAsyncActionFilterAttribute : Attribute, IAsyncActionFilter
    {
        //定义只读属性
        private readonly ILogger<CustomLogAsyncActionFilterAttribute> _ILogger;

        //定义构造函数
        public CustomLogAsyncActionFilterAttribute(ILogger<CustomLogAsyncActionFilterAttribute> iLogger)
        {
            this._ILogger = iLogger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //获得控制器名称
            var controllerName = context.HttpContext.GetRouteValue("controller");
            //获得执行方法名称
            var actionName = context.HttpContext.GetRouteValue("action");

            //获得请求参数值
            var para = context.HttpContext.Request.QueryString.Value;  

            //开始写入日志
            _ILogger.LogInformation($"执行了{controllerName}控制器的{actionName}方法，参数为{para}");

            //这句话就是去执行 Action 方法
            ActionExecutedContext executedContext = await next.Invoke();  

            //获得控制器执行结果
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(executedContext.Result);

            //开始写入日志
            _ILogger.LogInformation($"执行了{controllerName}控制器的{actionName}方法，结果内容为{result}");
          

        }
    }
}
