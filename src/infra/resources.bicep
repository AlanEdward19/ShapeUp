@description('The location used for all deployed resources')
param location string = resourceGroup().location
@description('Id of the user or app to assign application roles')
param principalId string = ''


@description('Tags that will be applied to all resources')
param tags object = {}

var resourceToken = uniqueString(resourceGroup().id)

resource managedIdentity 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-01-31' = {
  name: 'mi-${resourceToken}'
  location: location
  tags: tags
}

resource containerRegistry 'Microsoft.ContainerRegistry/registries@2023-07-01' = {
  name: replace('acr-${resourceToken}', '-', '')
  location: location
  sku: {
    name: 'Basic'
  }
  tags: tags
}

resource caeMiRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(containerRegistry.id, managedIdentity.id, subscriptionResourceId('Microsoft.Authorization/roleDefinitions', '7f951dda-4ed3-4680-a7ca-43fe172d538d'))
  scope: containerRegistry
  properties: {
    principalId: managedIdentity.properties.principalId
    principalType: 'ServicePrincipal'
    roleDefinitionId:  subscriptionResourceId('Microsoft.Authorization/roleDefinitions', '7f951dda-4ed3-4680-a7ca-43fe172d538d')
  }
}

resource logAnalyticsWorkspace 'Microsoft.OperationalInsights/workspaces@2022-10-01' = {
  name: 'law-${resourceToken}'
  location: location
  properties: {
    sku: {
      name: 'PerGB2018'
    }
  }
  tags: tags
}

resource storageVolume 'Microsoft.Storage/storageAccounts@2022-05-01' = {
  name: 'vol${resourceToken}'
  location: location
  kind: 'StorageV2'
  sku: {
    name: 'Standard_LRS'
  }
  properties: {
    largeFileSharesState: 'Enabled'
  }
}

resource storageVolumeFileService 'Microsoft.Storage/storageAccounts/fileServices@2022-05-01' = {
  parent: storageVolume
  name: 'default'
}

resource mongoOrquestrator63f34af23fMongoDataFileShare 'Microsoft.Storage/storageAccounts/fileServices/shares@2022-05-01' = {
  parent: storageVolumeFileService
  name: take('${toLower('Mongo')}-${toLower('orquestrator63f34af23fMongodata')}', 60)
  properties: {
    shareQuota: 1024
    enabledProtocols: 'SMB'
  }
}
resource redisOrquestrator63f34af23fRedisDataFileShare 'Microsoft.Storage/storageAccounts/fileServices/shares@2022-05-01' = {
  parent: storageVolumeFileService
  name: take('${toLower('Redis')}-${toLower('orquestrator63f34af23fRedisdata')}', 60)
  properties: {
    shareQuota: 1024
    enabledProtocols: 'SMB'
  }
}
resource sqlServerOrquestrator63f34af23fSqlServerDataFileShare 'Microsoft.Storage/storageAccounts/fileServices/shares@2022-05-01' = {
  parent: storageVolumeFileService
  name: take('${toLower('SqlServer')}-${toLower('orquestrator63f34af23fSqlServerdata')}', 60)
  properties: {
    shareQuota: 1024
    enabledProtocols: 'SMB'
  }
}
resource sqlServerProfessionalManagementOrquestrator63f34af23fSqlServerProfessionalManagementDataFileShare 'Microsoft.Storage/storageAccounts/fileServices/shares@2022-05-01' = {
  parent: storageVolumeFileService
  name: take('${toLower('SqlServerProfessionalManagement')}-${toLower('orquestrator63f34af23fSqlServerProfessionalManagementdata')}', 60)
  properties: {
    shareQuota: 1024
    enabledProtocols: 'SMB'
  }
}

resource containerAppEnvironment 'Microsoft.App/managedEnvironments@2024-02-02-preview' = {
  name: 'cae-${resourceToken}'
  location: location
  properties: {
    workloadProfiles: [{
      workloadProfileType: 'Consumption'
      name: 'consumption'
    }]
    appLogsConfiguration: {
      destination: 'log-analytics'
      logAnalyticsConfiguration: {
        customerId: logAnalyticsWorkspace.properties.customerId
        sharedKey: logAnalyticsWorkspace.listKeys().primarySharedKey
      }
    }
  }
  tags: tags

  resource aspireDashboard 'dotNetComponents' = {
    name: 'aspire-dashboard'
    properties: {
      componentType: 'AspireDashboard'
    }
  }

}

resource mongoOrquestrator63f34af23fMongoDataStore 'Microsoft.App/managedEnvironments/storages@2023-05-01' = {
  parent: containerAppEnvironment
  name: take('${toLower('Mongo')}-${toLower('orquestrator63f34af23fMongodata')}', 32)
  properties: {
    azureFile: {
      shareName: mongoOrquestrator63f34af23fMongoDataFileShare.name
      accountName: storageVolume.name
      accountKey: storageVolume.listKeys().keys[0].value
      accessMode: 'ReadWrite'
    }
  }
}

resource redisOrquestrator63f34af23fRedisDataStore 'Microsoft.App/managedEnvironments/storages@2023-05-01' = {
  parent: containerAppEnvironment
  name: take('${toLower('Redis')}-${toLower('orquestrator63f34af23fRedisdata')}', 32)
  properties: {
    azureFile: {
      shareName: redisOrquestrator63f34af23fRedisDataFileShare.name
      accountName: storageVolume.name
      accountKey: storageVolume.listKeys().keys[0].value
      accessMode: 'ReadWrite'
    }
  }
}

resource sqlServerOrquestrator63f34af23fSqlServerDataStore 'Microsoft.App/managedEnvironments/storages@2023-05-01' = {
  parent: containerAppEnvironment
  name: take('${toLower('SqlServer')}-${toLower('orquestrator63f34af23fSqlServerdata')}', 32)
  properties: {
    azureFile: {
      shareName: sqlServerOrquestrator63f34af23fSqlServerDataFileShare.name
      accountName: storageVolume.name
      accountKey: storageVolume.listKeys().keys[0].value
      accessMode: 'ReadWrite'
    }
  }
}

resource sqlServerProfessionalManagementOrquestrator63f34af23fSqlServerProfessionalManagementDataStore 'Microsoft.App/managedEnvironments/storages@2023-05-01' = {
  parent: containerAppEnvironment
  name: take('${toLower('SqlServerProfessionalManagement')}-${toLower('orquestrator63f34af23fSqlServerProfessionalManagementdata')}', 32)
  properties: {
    azureFile: {
      shareName: sqlServerProfessionalManagementOrquestrator63f34af23fSqlServerProfessionalManagementDataFileShare.name
      accountName: storageVolume.name
      accountKey: storageVolume.listKeys().keys[0].value
      accessMode: 'ReadWrite'
    }
  }
}

output MANAGED_IDENTITY_CLIENT_ID string = managedIdentity.properties.clientId
output MANAGED_IDENTITY_NAME string = managedIdentity.name
output MANAGED_IDENTITY_PRINCIPAL_ID string = managedIdentity.properties.principalId
output AZURE_LOG_ANALYTICS_WORKSPACE_NAME string = logAnalyticsWorkspace.name
output AZURE_LOG_ANALYTICS_WORKSPACE_ID string = logAnalyticsWorkspace.id
output AZURE_CONTAINER_REGISTRY_ENDPOINT string = containerRegistry.properties.loginServer
output AZURE_CONTAINER_REGISTRY_MANAGED_IDENTITY_ID string = managedIdentity.id
output AZURE_CONTAINER_REGISTRY_NAME string = containerRegistry.name
output AZURE_CONTAINER_APPS_ENVIRONMENT_NAME string = containerAppEnvironment.name
output AZURE_CONTAINER_APPS_ENVIRONMENT_ID string = containerAppEnvironment.id
output AZURE_CONTAINER_APPS_ENVIRONMENT_DEFAULT_DOMAIN string = containerAppEnvironment.properties.defaultDomain
output SERVICE_MONGO_VOLUME_ORQUESTRATOR63F34AF23FMONGODATA_NAME string = mongoOrquestrator63f34af23fMongoDataStore.name
output SERVICE_REDIS_VOLUME_ORQUESTRATOR63F34AF23FREDISDATA_NAME string = redisOrquestrator63f34af23fRedisDataStore.name
output SERVICE_SQLSERVER_VOLUME_ORQUESTRATOR63F34AF23FSQLSERVERDATA_NAME string = sqlServerOrquestrator63f34af23fSqlServerDataStore.name
output SERVICE_SQLSERVERPROFESSIONALMANAGEMENT_VOLUME_ORQUESTRATOR63F34AF23FSQLSERVERPROFESSIONALMANAGEMENTDATA_NAME string = sqlServerProfessionalManagementOrquestrator63f34af23fSqlServerProfessionalManagementDataStore.name
output AZURE_VOLUMES_STORAGE_ACCOUNT string = storageVolume.name
