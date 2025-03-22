using ASPNETCoreMVCTraining.Application.Interfaces;
using ASPNETCoreMVCTraining.Application.Services;
using ASPNETCoreMVCTraining.Application.ViewModels;
using ASPNETCoreMVCTraining.Persistent;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.AddServiceDefaults();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext"));
    }
);
builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddScoped<IDeptService, DeptService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IPageService, PageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseReDoc(options =>
    {
        options.SpecUrl = "/openapi/v1.json";
    });
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapControllers();
app.MapDefaultEndpoints();

var deptGroup = app.MapGroup("/api/Dept").WithTags("Dept");

deptGroup.MapGet("/", ([FromServices] IDeptService deptService) =>
{
    List<DeptViewModel> depts = [];
    try
    {
        depts = deptService.GetDept();
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex.Message);
    }

    return depts;
}).WithName("GetDept");

deptGroup.MapPost("/", ([FromServices] IDeptService deptService, [FromBody] DeptCreateEditViewModel model) =>
{
    var isSuccess = false;
    try
    {
        if (model is null || string.IsNullOrEmpty(model?.Name))
        {
            return Results.BadRequest("Model invalid.");
        }

        isSuccess = deptService.CreateDept(new DeptViewModel() { Name = model.Name });
    }
    catch (Exception e)
    {
        app.Logger.LogError(e.Message);
        Results.BadRequest("Failed to add dept.");
    }

    return isSuccess ? Results.Ok() : Results.BadRequest("Failed to add dept.");
}).WithName("AddDept");

deptGroup.MapPut("/{id}", ([FromServices] IDeptService deptService, int? id, [FromBody] DeptCreateEditViewModel model) =>
{
    var isSuccess = false;
    try
    {
        if (id == null)
        {
            return Results.BadRequest("Id invalid.");
        }

        if (string.IsNullOrEmpty(model.Name))
        {
            return Results.BadRequest("Name invalid.");
        }

        isSuccess = deptService.UpdateDept(new DeptViewModel() { Id = (int)id, Name = model.Name });
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex.Message);
        Results.BadRequest("Failed to update dept.");
    }

    return isSuccess ? Results.NoContent() : Results.BadRequest("Failed to update dept.");
}).WithName("UpdateDept");

deptGroup.MapDelete("/{id}", ([FromServices] IDeptService deptService, int? id) =>
{
    var isSuccess = false;
    try
    {
        if (id is null)
        {
            app.Logger.LogError("Id invalid.");
            return Results.BadRequest("Id invalid.");
        }

        isSuccess = deptService.RemoveDept(id.Value);
    }
    catch (Exception e)
    {
        app.Logger.LogError(e.Message);
        Results.BadRequest("Failed to delete dept.");
    }

    return isSuccess ? Results.NoContent() : Results.BadRequest("Failed to remove dept.");
}).WithName("DeleteDept");

app.Run();