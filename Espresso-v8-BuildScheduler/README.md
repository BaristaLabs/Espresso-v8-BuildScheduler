#Espresso v8 Build Scheduler

Azure Function to queue Espresso v8 Builds periodically.

Due to [Azure DevOps Organizations becoming dormant after a time period](https://docs.microsoft.com/en-us/azure/devops/pipelines/build/triggers?view=azure-devops&tabs=yaml#my-build-didnt-run-what-happened)), this Azure function
queues a build by hitting the DevOps REST services with a PAT, thus overcoming the limitation.


