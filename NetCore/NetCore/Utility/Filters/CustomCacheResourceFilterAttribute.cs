using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetCore.Utility.Filters
{

    //继承 Attribute 和 IResourceFilter 2个父类
    public class CustomCacheResourceFilterAttribute : Attribute, IResourceFilter
    {
        //定义一个缓存区域
        //请求来了后，根据缓存标识----作判断
        //如果有缓存，就返回缓存的值
        //如果没缓存，就作相应的计算
        //计算结果保存到缓存内部

        private static Dictionary<string, object> CacheDictionary = new Dictionary<string, object>();  //定义字典


        /// <summary>
        /// 在 * 资源之前
        /// </summary>
        /// <param name="context"></param>
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            string key = context.HttpContext.Request.Path;  //定义 http 请求路径
            if (CacheDictionary.ContainsKey(key))  //如果请求路径存在
            {
                //注意 只要 Result 被赋值，就会中断往后执行，直接返回给调用方
                context.Result = (IActionResult)CacheDictionary[key];  //Result 是 IActionResult类型，所以值需要做转换 并且需要引入 Microsoft.AspNetCore.Mvc
            }

            Console.WriteLine("CustomResourceFilterAttribute, OnResourceExecuted");
        }


        /// <summary>
        /// 在 * 资源之后
        /// </summary>
        /// <param name="context"></param>
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            string key = context.HttpContext.Request.Path;  //定义 http 请求路径
            CacheDictionary[key] = context.Result;

            Console.WriteLine("CustomResourceFilterAttribute, OnResourceExecuted");
        }
    }
}
