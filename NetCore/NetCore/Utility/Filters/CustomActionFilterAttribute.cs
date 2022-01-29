using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetCore.Utility.Filters
{
    public class CustomActionFilterAttribute : Attribute, IActionFilter
    {
        //定义字典
        private static Dictionary<string, object> CacheDictionary = new Dictionary<string, object>();

        

        /// <summary>
        /// 在 * Action执行之前
        /// </summary>
        /// <param name="context"></param>

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //定义key
            string key = context.HttpContext.Request.Path;
            //如果key存在
            if(CacheDictionary.ContainsKey(key))
            {
                context.Result = (IActionResult)CacheDictionary[key];
            }

            Console.WriteLine("CustomActionFilterAttribute.OnActionExecuting");
        }

        /// <summary>
        /// 在 * Action执行之后
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //定义请求路径
            string key = context.HttpContext.Request.Path;
            //将请求数据存入缓存
            CacheDictionary[key] = context.Result;


            Console.WriteLine("CustomActionFilterAttribute.OnActionExecuted");
        }
    }
}
