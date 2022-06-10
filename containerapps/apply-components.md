### You should use a proper path in the --yaml parameter.

```ps
az containerapp env dapr-component set --yaml .\wisdom-pubsub.yaml --dapr-component-name pubsub --name YOUR_ENVIRONMENT --resource-group YOUR_RESOURCE_GROUP

az containerapp env dapr-component set --yaml .\wisdom-statestore.yaml --dapr-component-name statestore --name YOUR_ENVIRONMENT --resource-group YOUR_RESOURCE_GROUP
