# Projekt4-Pear

Projekt 4 Uppgift - Ehandel sida

If you are running this project for the first time please refer to [First launch](#first-launch) for the necessary steps!

# First launch

All the steps are written with "Projekt4-Pear" as the starting directory. If you are already within the correct directory you can skip the "cd \*" step.

## Frontend

The project is using yarn as the package manager and Vue as the frontend library with Vite as the build tool

In order to run the frontend the following steps are required:

```bash
cd PearFrontend
yarn install
yarn dev
```

Make sure that you are within the PearFrontend directory to not create unnecessary files in wrong directiories

## Backend

The backend is written in C# with .NET (Dotnet) & Postgress Entity Framework

In order to run the backend the following steps are required:

```bash
cd PearApi
dotnet build
dotnet run
```

The project supports Hot Reload which can be achieved by running:

```bash
dotnet run watch
```

The project contains a seed file which can be used by running:

```bash
cd PearApi
dotnet run seed
```

## Docker/Database

Get the docker image from docker hub:
```bash
docker pull postgres
```

Start the docker container:
```bash
docker run --rm   --name pg-docker -e POSTGRES_PASSWORD=docker -d -p 5432:5432 -v $HOME/docker/volumes/postgres:/var/lib/postgresql/data  postgres
```


# DB Context switch

Go to the base folder of the repo

```bash
chmod +x ./dockerDbFix.sh
./dockerDbFix.sh
```

If the script fails run the following commands

```bash
cd PearApi
dotnet ef database update
```

# Linter Info

This project uses a linter to enforce a common common stadard. The common rules are as follows.

- We use double quotes ("") in strings, not single quotes ('').
- As indentation we use tripple space.
- Snake_case is forbidden so no underscores in functions, classes or variable names although they are allowed at the begining.
- Semi colons are enforced in all typescript.

In the PearAPI the specific rules include.

- PascalCase for all.
- Space around column in inheritance clause.
- No space in empty parenthesies.
- Always new line before braces.
- No unused rows at the end of files.

In the PearFrontend the rules follow.

- The prettier code standard.
- The vue recomended code standard.
- The typescript code standard.

The linters we use are eslint for the frontend in th PearFrontend folder and dotnet format for the backend in the PearAPI folder.
To run the linters locally make sure to have all dependencies installed in the frontend with "yarn install" and run the linter on the
src folder with "yarn lint" or if you want to lint a specific file run "yarn eslint <(file path)> --fix". The suffix --fix automatically fixes
autofixable ruletransgressions. If you want the linter to follow the file tree remember to put the path in double coutes as such
'yarn eslint "./src/\*_/_" --fix'.

The linteres are at the moment run with github actions on pull requests and pushes to main and you can also run them before each commit by 
running "./changeHookDir.sh" in the terminal, changing the it hooks directory manualy with "git config core.hooksPath .githooks" or by following
the instructions in the .githooks/pre-commit file. If you want to skip the linting on a commit you may also append the suffix --no-verify 
at the end of the commit command.

The linters are configured in Projekt4-Pear/PearApi/.editorconfig for dotnet format, Projekt4-Pear/PearFrontend/.eslintrc.json for eslint,
Projekt4-Pear/.github/workflows/linter.yml for the github action and Projekt4-Pear/PearFrontend/prettierrc.json for the prettier configuration.
There you can also find further documentation on their inner working except for the prettierrc.json that doesn't allow it.

**The linters are configured in:**

Projekt4-Pear/PearApi/.editorconfig for **dotnet format**

Projekt4-Pear/PearFrontend/.eslintrc.json for **eslint**

Projekt4-Pear/.github/workflows/linter.yml for **GitHub Actions**

Projekt4-Pear/PearFrontend/prettierrc.json for **Prettier**

## Rules

This project uses a linter to enforce a common code stadard. The common rules are as follows:

- We use double quotes ("") in strings, not single quotes ('').
- As indentation we use tripple space.
- Snake_case is forbidden so no underscores in functions, classes or variable names although they are allowed at the begining.
- Semi colons are enforced in all typescript.

Backend specific rules include:

- PascalCase for all.
- Space around column in inheritance clause.
- No space in empty parenthesies.
- Always new line before braces.
- No unused rows at the end of files.

Frontend specific rules include:

- The Prettier code standard.
- The Vue recomended code standard.
- The TypeScript code standard.

## Run linter

The linters we use are eslint for the frontend in the PearFrontend folder and dotnet format for the backend in the PearAPI folder.

### Frontend

To run the linter locally make sure you have all dependencies installed in the frontend. Refer to [First Launch](#first-launch) for guidance.

To run the frontend linter on the
src folder with

```bash
yarn lint
```

To lint a specific file run

```bash
yarn eslint <FILE_PATH> --fix
```

The suffix --fix automatically fixes
autofixable ruletransgressions.

The linteres are at the moment run with github actions on pull requests and pushes to main and you can also run them before each commit by 
running "./changeHookDir.sh" in the terminal, changing the it hooks directory manualy with "git config core.hooksPath .githooks" or by following
the instructions in the .githooks/pre-commit file. If you want to skip the linting on a commit you may also append the suffix --no-verify 
at the end of the commit command.

The linters are configured in Projekt4-Pear/PearApi/.editorconfig for dotnet format, Projekt4-Pear/PearFrontend/.eslintrc.json for eslint,
Projekt4-Pear/.github/workflows/linter.yml for the github action and Projekt4-Pear/PearFrontend/prettierrc.json for the prettier configuration.
There you can also find further documentation on their inner working except for the prettierrc.json that doesn't allow it.

```bash
yarn eslint "./src/\*_/_" --fix
```

# Working Tree

```
|-- PearApi
|   |-- Context
|   |-- Controllers
|   |-- Interfaces
|   |-- Migrations
|   |   |-- Order
|   |   └- Pear
|   |-- Models
|   |-- Properties
|   |-- Seeder
|   |-- Services
|   |--
|   └-- obj
|
└-- PearFrontend
    |-- public
    └-- src
        |-- assets
        |-- components
        |   |-- TestMeLinter
        |   |-- adminComponents
        |   |-- buisnessComponents
        |   |-- generalComponents
        |   └-- shopComponents
        |-- router
        |-- stores
        └-- views
            |-- admin
            |-- buisness
            └-- shop
```

# Custom import routes:

@generalComponents = ./pearFrontend/src/components/generalComponents
This hosts all the general components that could be used by everyone and **IS NOT HARDCODED**

@adminComponents = ./pearFrontend/src/components/adminComponents
This hosts all the specialized components for the admin pages

@buisnessComponents = ./pearFrontend/src/components/buisnessComponents
This hosts all the specialized components for the buisness pages

@shopComponents = ./pearFrontend/src/components/shopComponents
This hosts all the specialized components for the shop pages

@views = ./pearFrontend/src/views
This hosts all views and at this level should only hold the homepage vue file

@adminViews = ./pearFrontend/src/views/adminViews
This hosts all views for admin

@buisnessViews = ./pearFrontend/src/views/buisnessViews
This hosts all views for buisness

@shopViews = ./pearFrontend/src/views/shopViews
This hosts all views for the shop

@router = ./pearFrontend/src/router
This hosts the router index file

@store = ./pearFrontend/src/store
This hosts the stores

# Testing
The Backend tests are done with Xunit and can be run locally with the command "dotnet test". To make a test, go to the "tests" folder,
create a c# file with an apropriate name and follow the pattern layed out here: https://xunit.net/docs/getting-started/netcore/cmdline.

If you want to test an API you need to mimic one of the other API testing files wich are characterised by the model name + XUT. I have as
a standard to end testingfiles with XUT wich stands for X Unit Test. If you want you may also use dredd for testing tha api wich uses an
.apib file as testing source. To learn the syntax of apib check out https://apiblueprint.org/. This has the potential to be generated 
automatically although at the moment the providers of this service have not kept up to date with dotnet. Another drawback of using dredd is
that it at the moment queries the main database "pg-docker" directly wich as several sideaffects and tests also affect each other. You mabie
could solce this by using dredd hooks but due to timeconstraints I have not yet looked into it. For more information about dredd click   
https://dredd.org/en/latest/how-to-guides.html# and to run dredd run "yarn dredd" in the PearApi.