using HorrorOnline.Core.ServiceContracts.Stories;
using HorrorOnline.Core.Services.Stories;
using HorrorOnline.UI.StartupExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.ConfigureServices(builder.Configuration, builder.Environment);

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    
}

app.UseHsts();// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapControllers();

app.Run();
//Server=tcp:horroronline-dbdbserver.database.windows.net;Authentication=Active Directory Default;Database=HorrorOnline.db;
//    "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HorrorOnlineDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"

public partial class Program { }