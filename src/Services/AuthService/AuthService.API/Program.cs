// <copyright file="Program.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using AuthService.API;
using AuthService.Application;
using AuthService.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using SharedDefinitions.Application;
using SharedDefinitions.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder
        .AddSharedDefinitionsInfrastructure("AuthService")
        .AddSharedDefinitionsApplication(typeof(AssemblyAnchor).Assembly)
        .AddAuthenticationInfrastructure()
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

    app.UseSerilogRequestLogging();

    await app.RunAsync();
}