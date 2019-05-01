# Espresso v8 Functions

Azure Functions that support Espresso v8

Mono-Repo that includes:

### Espresso v8 Build Scheduler

Azure Function to queue Espresso v8 Builds on a set schedule.
Due to [Azure DevOps Organizations becoming dormant after a time period](https://docs.microsoft.com/en-us/azure/devops/pipelines/build/triggers?view=azure-devops&tabs=yaml#my-build-didnt-run-what-happened)), scheduled builds will
only execute one time after a user logs in. This seems like an arbitrary restriction on Azure DevOps part, so this is a quick-and-dirty Azure Timer-triggered Function that queues a build by hitting the DevOps REST services with a PAT,
thus overcoming the limitation.

Refer to https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-timer#usage to mutate the TimerTrigger attribute