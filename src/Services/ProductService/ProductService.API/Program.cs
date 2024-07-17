// <copyright file="Program.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector.Services.ProductService. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Diagnostics;
using SneakerCollector.Services.ProductService.API;
using SneakerCollector.Services.ProductService.Application;
using SneakerCollector.Services.ProductService.Infrastructure;
using SneakerCollector.SharedDefinitions.Application;
using SneakerCollector.SharedDefinitions.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder
        .AddSharedDefinitionsInfrastructure("ProductService")
        .AddProductServicePersistance()
        .AddSharedDefinitionsApplication(typeof(SneakerCollector.Services.ProductService.Application.AssemblyAnchor).Assembly)
        .AddPresentation();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        app.UseDefaultFiles();
        app.UseStaticFiles();
    }

    app.UseExceptionHandler("/error");

    app.Map("/error", (HttpContext context) =>
    {
        Exception? exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        return Results.Problem(exception?.Message);
    });

    app.UseHttpsRedirection();

    app.UseCors();

    app.MapControllers();

    app.UseAuthorization();

    app.Run();
}