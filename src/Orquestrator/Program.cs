var builder = DistributedApplication.CreateBuilder(args);
var search = builder.AddConnectionString("Search");

#if DEBUG

var mongo = builder
    .AddMongoDB("Mongo")
    .WithDataVolume();

var sqlServerAuth = builder
    .AddSqlServer("SqlServer")
    .WithDataVolume();

var sqlServerPm = builder
    .AddSqlServer("SqlServerProfessionalManagement")
    .WithDataVolume();

var storage = builder
    .AddAzureStorage("Storage")
    .RunAsEmulator(c => c
        .WithDataVolume()
        .WithImageTag("latest")
    );

var socialService = builder
    .AddProject<Projects.SocialService>("social-service")
    .WaitFor(storage)
    .WithReference(storage.AddBlobs("BlobStorage"))
    .WithReference(search);

var notificationService = builder
    .AddProject<Projects.NotificationService>("notification-service")
    .WaitFor(mongo)
    .WithReference(mongo);

var chatService = builder
    .AddProject<Projects.ChatService>("chat-service")
    .WaitFor(mongo)
    .WithReference(mongo);

var nutritionService = builder
        .AddProject<Projects.NutritionService>("nutrition-service")
        .WaitFor(mongo)
        .WithReference(mongo);

var authService = builder
    .AddProject<Projects.AuthService>("auth-service")
    .WaitFor(sqlServerAuth)
    .WithReference(sqlServerAuth);

var trainingService = builder
    .AddProject<Projects.TrainingService>("training-service")
    .WaitFor(sqlServerAuth)
    .WaitFor(mongo)
    .WithReference(sqlServerAuth)
    .WithReference(mongo);

var professionalManagementService = builder
    .AddProject<Projects.ProfessionalManagementService>("professional-management-service")
    .WaitFor(sqlServerPm)
    .WithReference(sqlServerPm);

#else

var mongo = builder.AddConnectionString("Mongo");

var sqlServerAuth = builder.AddConnectionString("SqlServer");

var sqlServerPm = builder.AddConnectionString("SqlServerProfessionalManagement");

var storage = builder
    .AddAzureStorage("Storage");  

var socialService = builder
        .AddProject<Projects.SocialService>("social-service")
        .WithReference(storage.AddBlobs("BlobStorage"))
        .WithReference(search);

var notificationService = builder
        .AddProject<Projects.NotificationService>("notification-service")
        .WithReference(mongo);

var chatService = builder
        .AddProject<Projects.ChatService>("chat-service")
        .WithReference(mongo);

var nutritionService = builder
        .AddProject<Projects.NutritionService>("nutrition-service")
        .WaitFor(mongo)
        .WithReference(mongo);

var authService = builder
        .AddProject<Projects.AuthService>("auth-service")
        .WithReference(sqlServerAuth);

var trainingService = builder
        .AddProject<Projects.TrainingService>("training-service")
        .WithReference(sqlServerAuth)
        .WithReference(mongo);

var professionalManagementService = builder
        .AddProject<Projects.ProfessionalManagementService>("professional-management-service")
        .WithReference(sqlServerPm);
#endif

builder.Build().Run();