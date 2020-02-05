This project offers a way to expose configuring from Spring Cloud Config Server to your apps as environmental variables. Normally this requires a client library to be used in code such as those available for .NET via SteelToe or Java via Spring. This allows config server to be used with other stacks such as NodeJS, PHP, etc. Note that since environmental variables are read only once on app startup, if values in config server changes you need to restart the app.

## How to Use

- Create Config server instance and bind it your your app

- Apply as supply buildpack in your manifest:

  ```yaml
  ---
  applications:
  - name: myapp
    buildpacks: 
    - https://github.com/macsux/config-server-buildpack/releases/download/v0.1.0/ConfigServerBuildpack-linux-x64-0.1.0.zip
    - nodejs_buildpack
    env:
      SPRING__APPLICATION__NAME: myapp
      SPRING__CLOUD__CONFIG__ENV: profile-name
      SPRING__CLOUD__CONFIG__LABEL: label-name
  ```

(Note if targeting windows, make sure to substitute above buildpack URL with appropriate windows based release. Get latest compiled buildpack binary URL on GitHub releases page)

Edit the env vars above:

* `SPRING__APPLICATION__NAME` - name of the app in config server
* `SPRING__CLOUD__CONFIG__ENV` - (optional) profile name
* `SPRING__CLOUD__CONFIG__LABEL` - (optional) label name

**Attention: Note the double underscore in environmental variables**

Environmental variables will be available to your app as both individual values mapped to the path AND a single JSON block available in `CONFIG_SERVER_APP_JSON` environmental variable. During app startup the buildpack will print out all variables it creates (without values).