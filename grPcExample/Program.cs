using grPcExample.Context;
using grPcExample.Services.OrderServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using static grPcExample.Services.OrderGrpcServices.OrderGrpcServices;

var builder = WebApplication.CreateBuilder(args);


var conn = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddDbContext<AppDbContext>(db => db.UseSqlServer(conn));
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Register Grpc 
builder.Services.AddGrpc().AddJsonTranscoding();
builder.Services.AddGrpcSwagger();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "gRPC Transcoding",
        Version = "v1"
    });

    var filePath = Path.Combine(System.AppContext.BaseDirectory, "grpcService.xml");
    options.IncludeXmlComments(filePath);
    options.IncludeGrpcXmlComments(filePath, includeControllerXmlComments: true);
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapGrpcService<OrderGrpcService>();
app.UseSwagger();
app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
