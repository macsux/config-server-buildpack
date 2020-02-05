This is a sample .NET Framework (windows stack) app that will print out all environmental variables. How to deploy:

- Build the app locally (in Visual Studio or using Developer Command Prompt with `msbuild` command)
- Run `cf create-service p-config-server standard config-server -c gitconfig.json`
- Wait for service to finish provisioning
- Push the app with included manifest