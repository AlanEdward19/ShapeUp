var builder = DistributedApplication.CreateBuilder(args);

var mongo = builder
        .AddMongoDB("Mongo")
        .WithDataVolume();

 var redis = builder
         .AddRedis("Redis")
         .WithDataVolume();

var sqlServer = builder
        .AddSqlServer("SqlServer")
        .WithDataVolume();

var sqlServerProfessionalManagement = builder
        .AddSqlServer("SqlServerProfessionalManagement")
        .WithDataVolume();

var storage = builder
        .AddAzureStorage("Storage")
        .RunAsEmulator(c => c
                .WithDataVolume()
                .WithImageTag("latest")
        );

var search = builder.AddConnectionString("Search");

var socialService = builder
        .AddProject<Projects.SocialService>("SocialService")
        .WaitFor(redis)
        .WaitFor(storage)
        .WithReference(storage.AddBlobs("BlobStorage"))
        .WithReference(redis)
        .WithReference(search);

var notificationService = builder
        .AddProject<Projects.NotificationService>("NotificationService")
        .WaitFor(mongo)
        .WithReference(mongo);

var chatService = builder
        .AddProject<Projects.ChatService>("ChatService")
        .WaitFor(redis)
        .WaitFor(mongo)
        .WithReference(redis)
        .WithReference(mongo);

var nutritionService = builder
        .AddProject<Projects.NutritionService>("NutritionService")
        .WaitFor(mongo)
        .WithReference(mongo);

var authService = builder
        .AddProject<Projects.AuthService>("AuthService")
        .WaitFor(sqlServer)
        .WithReference(sqlServer);

var trainingService = builder
        .AddProject<Projects.TrainingService>("TrainingService")
        .WaitFor(sqlServer)
        .WaitFor(mongo)
        .WithReference(sqlServer)
        .WithReference(mongo);

var professionalManagementService = builder
        .AddProject<Projects.ProfessionalManagementService>("ProfessionalManagementService")
        .WaitFor(sqlServerProfessionalManagement)
        .WithReference(sqlServerProfessionalManagement);

builder.Build().Run();