using Microsoft.AspNetCore.Mvc.Filters;

namespace NetCore.Utility.Filters
{
    

    public class CustomLogActionFilterAttribute : Attribute, IActionFilter
    {
        //定义只读属性
        private readonly ILogger<CustomLogActionFilterAttribute> _ILogger;

        //构造函数
        public CustomLogActionFilterAttribute(ILogger<CustomLogActionFilterAttribute> iLogger)
        {
            this._ILogger = iLogger;
        }

        //控制器Action执行前
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //获得请求参数
            var para = context.HttpContext.Request.QueryString.Value;

            //获得控制器名称
            var controllerName = context.HttpContext.GetRouteValue("controller");

            //获得控制器方法
            var actionName = context.HttpContext.GetRouteValue("action");

            _ILogger.LogInformation($"执行{controllerName}控制器--{actionName}方法；参数为：{para}");

            Console.WriteLine("CustomLogActionFilterAttribute.OnActionExecuting");
        }

        //控制器Action执行后，只执行一次
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //获得请求结果数据 并且要合的 Json 格式化的数据
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(context.Result);
            //获得控制器名称
            var controllerName = context.HttpContext.GetRouteValue("controller");
            //获得控制器方法
            var actionName = context.HttpContext.GetRouteValue("action");

            _ILogger.LogInformation($"执行{controllerName}控制器--{actionName}方法；结果为：{result}");

            Console.WriteLine("CustomLogActionFilterAttribute.OnActionExecuted");
        }

    }
}
