using DotSyncServer.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.AddTransient<IUploadService, UploadService>();
builder.Services.AddTransient<IDownloadService, DownloadService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

var internalIPs = (new IPService()).GetInternalIPs();
foreach (var ip in internalIPs)
{
    app.Urls.Add(ip);
}

app.Run();

