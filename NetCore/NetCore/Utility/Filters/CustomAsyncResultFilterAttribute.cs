using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NetCore.Models;

namespace NetCore.Utility.Filters
{
    public class CustomAsyncResultFilterAttribute : Attribute, IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if(context.Result is JsonResult)
            {
                JsonResult jsonResult =  (JsonResult)context.Result;


                context.Result = new JsonResult(new AjaxResult() { 
                    Success = true,
                    Message = "ok",
                    Data = jsonResult.Value
                });
            }

            //渲染 View 视图结果之前


            //这里渲染 View 视图结果
            ResultExecutedContext executedContext = await next.Invoke();  //执行委托

            //渲染 View 视图结果之后

            Console.WriteLine("CustomAsyncResultFilterAttribute.OnResultExecutionAsync");
            
        }
    }
}
