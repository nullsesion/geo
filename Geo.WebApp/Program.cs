using System.Reflection;
using Geo.Application.Common.Mappings;
using Geo.Application.Interfaces;
using Geo.Application.IpLocations.Queries.Get;
using Geo.Persistence;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IGeoApiDbContext, GeoApiDbContext>();

builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(config =>
{
	config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
	config.AddProfile(new AssemblyMappingProfile(typeof(IGeoApiDbContext).Assembly));
});
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetIpLocation).Assembly));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapGet("/api/v1/getgeo/{ip}", async (IMediator _mediator, string ip) =>
	await _mediator.Send(new GetIpLocation { Address = ip }) is { } t 
		? Results.Ok(t) 
		: Results.NotFound("not found")
)
.Produces<IpLocationVm>(StatusCodes.Status200OK)
.WithName("GetGeoIP")
.WithTags("Getters");

app.Run();