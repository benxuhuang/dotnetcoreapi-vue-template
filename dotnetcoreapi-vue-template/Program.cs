var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    await next();

    // 判斷是否是要存取網頁，而不是發送 API 需求
    if (context.Response.StatusCode == 404 &&                       // 該資源不存在
        !System.IO.Path.HasExtension(context.Request.Path.Value) && // 網址最後沒有帶副檔名
        !context.Request.Path.Value.StartsWith("/api"))             // 網址不是 /api 開頭
    {
        context.Request.Path = "/index.html";                       // 將網址改成 /index.html
        context.Response.StatusCode = 200;                          // 並將 HTTP 狀態碼修改為 200 成功
        await next();
    }
});


app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();
