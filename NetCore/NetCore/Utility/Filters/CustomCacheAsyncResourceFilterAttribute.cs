using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetCore.Utility.Filters
{
    public class CustomCacheAsyncResourceFilterAttribute : Attribute, IAsyncResourceFilter
    {
        //定义属性字典
        private static Dictionary<string, object> CacheDictionary = new Dictionary<string, object>();


        /// <summary>
        /// 当 * 资源通过异步执行的时侯
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            Console.WriteLine("CustomCacheAsyncResourceFilterAttribute.OnResourceExecutionAsync Before");

            //定义字典 key
            string key = context.HttpContext.Request.Path;

            //判断缓存标识是否存在
            if(CacheDictionary.ContainsKey(key))
            {
                context.Result = (IActionResult)CacheDictionary[key];
            }
            //缓存不存在
            else
            {
               
                //这里是执行了控制器的构造函数 + Action 方法
                ResourceExecutedContext resource = await next.Invoke();

                //将结果保存到缓存中去
                CacheDictionary[key] = resource.Result;

                Console.WriteLine("CustomCacheAsyncResourceFilterAttribute.OnResourceExecutionAsync After");
            }

            
            
            
            
        }
    }
}
