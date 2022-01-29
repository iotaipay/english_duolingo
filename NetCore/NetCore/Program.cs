
//builder类型 WebApplicationBuilder
//WebApplicationBuilder
//var builder = WebApplication.CreateBuilder(args);
// 两种写法 创建 web 应用程序和服务的生成器 对象



using NLog.Web;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region log4net
{

    //Nuget引入: Microsoft.Extensions.Logging.Log4Net.AspNetCore
    //如查log4net配置文件是在当前目录下，则不需要再写读取路径，否则需要
    builder.Logging.AddLog4Net("cfgfile/log4net.Config");

}
#endregion


#region NLog
{

    //Nuget 引入 NLog.Web.AspNetCore
    //builder.Logging.AddNLog("cfgfile/Nlog.config");
}
#endregion

#region
// Add services to the container.
builder.Services.AddControllersWithViews();
{
    //添加Session
    builder.Services.AddSession();
}
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

//使用Session
app.UseSession();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
