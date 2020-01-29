This project offers a way to expose configuring from Spring Cloud Config Server to your apps as environmental variables. Normally this requires a client library to be used in code such as those available for .NET via SteelToe or Java via Spring. This allows config server to be used with other stacks such as NodeJS, PHP, etc. Note that since environmental variables are read only once on app startup, if values in config server changes you need to restart the app.

## How to Use

- Create Config server instance and bind it your your app

- Apply as supply buildpack in your manifest. Edit the `SPRING__APPLICATION__NAME` environmental variable to map it to the name of the app in config server

  ```yaml
  ---
  applications:
  - name: myapp
    buildpacks: 
    - https://github.com/macsux/config-server-buildpack/releases/download/v0.1.0/ConfigServerBuildpack-linux-x64-0.1.0.zip
    - nodejs_buildpack
    env:
      SPRING__APPLICATION__NAME: myapp
  ```

Environmental variables will be available to your app as both individual values mapped to the path AND a single JSON block available in `CONFIG_SERVER_APP_JSON` environmental variable. During app startup the buildpack will print out all variables it creates (without values).