namespace FriskInventarAPI;
using Microsoft.EntityFrameworkCore;
using Oracle.EntityFrameworkCore;
class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // https://localhost:7076/swagger/index.html
        // https://whatpwacando.today/notifications

        //builder.Services.AddDbContext<FriskInventarContext>(o => o.UseOracle("Data Source=localhost:1541/xe;User Id=Fresh_Produce;Password=Fresh_Produce;"));
        builder.Services.AddDbContext<FriskInventarContext>(o => o.UseOracle("Data Source=localhost:1531/xe;User Id=system;Password=oracle"));

        //builder.Services.ConfigureMsSqlContext(builder.Configuration); 
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        

        var app = builder.Build();

        app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())  
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        // this ip can change day-to-day
        // this URL is used for testing on mobile
        //app.Urls.Add("https://192.168.1.70:7076");

        app.Run();
    }
}