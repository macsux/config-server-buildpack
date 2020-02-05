This is a sample NodeJS app that will print out all environmental variables. How to deploy:

- Run `cf create-service p-config-server standard config-server -c gitconfig.json`
- Wait for service to finish provisioning
- Push the app with included manifest