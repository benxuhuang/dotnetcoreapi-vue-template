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

    // �P�_�O�_�O�n�s�������A�Ӥ��O�o�e API �ݨD
    if (context.Response.StatusCode == 404 &&                       // �Ӹ귽���s�b
        !System.IO.Path.HasExtension(context.Request.Path.Value) && // ���}�̫�S���a���ɦW
        !context.Request.Path.Value.StartsWith("/api"))             // ���}���O /api �}�Y
    {
        context.Request.Path = "/index.html";                       // �N���}�令 /index.html
        context.Response.StatusCode = 200;                          // �ñN HTTP ���A�X�קאּ 200 ���\
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
