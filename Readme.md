### .Net Core WebAPI Custom Template Creation Guide
---

**Step 1:** Create .Net core web API Project using .Net CLI
```sh
$ dotnet new webapi -n PROJECT_NAME 
```

Example: 
```sh
$ dontnet new webapi -n basic-webapi-template

```

**Step 2:** Modify the features of your template

**Step 3:** Creating the Template Configuration File
1. Create a folder **.template.config** in the root of your .NetCore WebAPI Project
2. With in the **.template.config** directory, we need to create **template.json** file
3. Paste the below configuration and modify the file depends on your requirement

```json
  "author": "AUTHOR_NAME",
  "classifications": ["WebApi"],
  "name": "Basic WebAPI Template",
  "identity": "BasicWebApi.Template",
  "shortName": "Basic WebApi",
  "tags": {
      "language": "C#"
  },
  "sourceName": "BasicWebApiTemplate",
  "preferNameDirectory": "true"
```
**Step 4:** Installing locally created template

```sh
$ dotnet new --install /path/to/parent/of/.template.config
```
Example: 
```sh
$ dotnet new --install ~/Templates/BasicWebApiTemplaye/
```
**Step 5:** Using the Template
To list the installed template
```sh
$ dotnet new --list
```

```sh
$ dotnet new templateName --name MyProject
```
