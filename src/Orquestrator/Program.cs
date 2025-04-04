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
         .WaitFor(search)
         .WithReference(storage.AddBlobs("BlobStorage"))
         .WithReference(redis)
         .WithReference(search);

 var notificationService = builder
         .AddProject<Projects.NotificationService>("NotificationService")
         .WaitFor(redis)
         .WithReference(redis)
         .WithExternalHttpEndpoints();

 var chatService = builder
         .AddProject<Projects.ChatService>("ChatService")
         .WaitFor(redis)
         .WaitFor(mongo)
         .WithReference(redis)
         .WithReference(mongo)
         .WithExternalHttpEndpoints();

var nutritionService = builder
        .AddProject<Projects.NutritionService>("NutritionService")
        .WaitFor(mongo)
        .WithReference(mongo)
        .WithExternalHttpEndpoints();

 var authService = builder
         .AddProject<Projects.AuthService>("AuthService")
         .WaitFor(sqlServer)
         .WithReference(sqlServer)
         .WithExternalHttpEndpoints();

builder.Build().Run();