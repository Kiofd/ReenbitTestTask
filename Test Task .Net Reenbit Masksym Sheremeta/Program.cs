using Test_Task_.Net_Reenbit_Masksym_Sheremeta.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IFileService, FileService>(provider => new FileService(
    builder.Configuration.GetValue<string>("AzureBlobStorage:ConnectionString") ?? string.Empty,
    builder.Configuration.GetValue<string>("AzureBlobStorage:ContainerName") ?? string.Empty));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();