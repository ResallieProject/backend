# Resallie Backend

C# backend of the Resallie Project. For new project members, or a new group please see the Transfer_Document.pdf.

### Running the project

```shell
dotnet run
```

### Building the project

```shell
dotnet build
```

## Setting up the database

Make sure you have a MySQL server running on your machine. Then, create a database called `Resallie` and run the
following command to create the tables:

```shell
dotnet ef database update
```

### Creating a migration

```shell
dotnet ef migrations add <migration name>
```

## Authentication

Specify a secret key in the appsettings.json file. This key will be used to sign the JWT tokens. Must be greater than 32
characters.