using Our.Umbraco.StorageProviders.AWSS3.DependencyInjection;
using TutorPro.Configuration;
using TutorPro.Middlewares;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

 builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers()
    .AddAWSS3MediaFileSystem()
    .Build();

builder.Services.AddServ();

WebApplication app = builder.Build();

await app.BootUmbracoAsync();

app.UseGlobalExceptionHandler();

app.UseStaticFiles();

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();

        // Enables the AWS S3 Storage middleware for Media
        u.UseAWSS3MediaFileSystem();

    })
    .WithEndpoints(u =>
    {
        u.UseInstallerEndpoints();
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

app.UseHttpsRedirection();

await app.RunAsync();
