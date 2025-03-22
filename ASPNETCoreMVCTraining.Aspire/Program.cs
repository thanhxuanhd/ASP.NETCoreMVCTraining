using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter(@"password", secret: true, value:"P@ssword");
var mssql = builder.AddSqlServer(name:"ApplicationDbContext", password).WithDataVolume(name:"mssql").WithLifetime(ContainerLifetime.Persistent);
var db = mssql.AddDatabase("ASPNETCoreMVCTrainingDb");

var mvcApp = builder.AddProject<ASPNETCoreMVCTraining>("ASPNETCoreMVCTrainingWeb").WithReference(mssql).WaitFor(mssql);
var apiApp = builder.AddProject<ASPNETCoreMVCTraining_Api>("ASPNETCoreMVCTrainingApi").WithReference(mssql).WaitFor(mssql);

builder.Build().Run();