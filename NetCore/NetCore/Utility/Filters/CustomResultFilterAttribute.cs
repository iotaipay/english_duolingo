using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NetCore.Models;

namespace NetCore.Utility.Filters
{
    /// <summary>
    /// 自定义结果过滤器属性
    /// </summary>
    public class CustomResultFilterAttribute : Attribute, IResultFilter  //同步版本
    {
        //定义内部只读属性
        private readonly ILogger<CustomResultFilterAttribute> _ILogger;

        //定义构造函数
        public CustomResultFilterAttribute(ILogger<CustomResultFilterAttribute> iLogger)
        {
            this._ILogger = iLogger;
        }

        /// <summary>
        /// * 结果之前
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            //判断 context.Result 是不是 JsonResult
            if (context.Result is JsonResult)
            {
                //取得 JsonResult 的数据
                JsonResult result = (JsonResult)context.Result;

                //将 JsonResult 格式化的数据 写入Model 类 重新赋给 context.Result
                context.Result = new JsonResult(new AjaxResult()
                {

                    Success = true,
                    Message = "ok",
                    Data = result.Value

                });

            }


            Console.WriteLine("CustomResultFilterAttribute.OnResultExecuting");
        }


        //OnResultExecuting 与 OnResultExecuted 之间
        //有一个渲染视图的动作


        /// <summary>
        /// * 结果之后
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuted(ResultExecutedContext context)
        {

            Console.WriteLine("CustomResultFilterAttribute.OnResultExecuted");
        }


    }
}
