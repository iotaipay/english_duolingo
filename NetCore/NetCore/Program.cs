
//builder���� WebApplicationBuilder
//WebApplicationBuilder
//var builder = WebApplication.CreateBuilder(args);
// ����д�� ���� web Ӧ�ó���ͷ���������� ����



using NLog.Web;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region log4net
{

    //Nuget����: Microsoft.Extensions.Logging.Log4Net.AspNetCore
    //���log4net�����ļ����ڵ�ǰĿ¼�£�����Ҫ��д��ȡ·����������Ҫ
    builder.Logging.AddLog4Net("cfgfile/log4net.Config");

}
#endregion


#region NLog
{

    //Nuget ���� NLog.Web.AspNetCore
    //builder.Logging.AddNLog("cfgfile/Nlog.config");
}
#endregion

#region
// Add services to the container.
builder.Services.AddControllersWithViews();
{
    //���Session
    builder.Services.AddSession();
}
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

//ʹ��Session
app.UseSession();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
