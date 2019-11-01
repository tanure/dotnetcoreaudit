# Dotnet Core Audit

This repo it's a POC about the use of the Audit.Net Library with .net Core

# Prerequisites

- .net core 3.0
- docker
- Azure Data Studio / Sql Management Studio

# How to use

Create an instance of database using docker: 

```bash

docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Pa$$w0rd" -p 1433:1433 -d mcr.microsoft.com/mssql/server:latest

```

Create database and tables

Connect in database use Sql Management Studio or Azure Data Studio and execute the following scripts:

```sql

CREATE DATABASE pocaidit;

```


```sql

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditLog](
	[AuditId] [int] IDENTITY(1,1) NOT NULL,
	[TablePk] [varchar](100) NOT NULL,
	[AuditAction] [varchar](100) NOT NULL,
	[AuditUser] [varchar](150) NOT NULL,
	[AuditDate] [datetime] NOT NULL,
	[AuditData] [varchar](max) NOT NULL,
	[EntityType] [varchar](250) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[AuditLog] ADD PRIMARY KEY CLUSTERED 
(
	[AuditId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[Email] [varchar](250) NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Person] ADD PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


```

# Configurations

In Statup.cs file we have all configurations from connecting Database and use the Audit.net Library:

```csharp

services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultDatabase"))); // Configure DbContext

/*
Configuring the Audit DataProvider with EntityFramewor and a generic method to create audit for all of the tables of the application
*/
Audit.Core.Configuration.Setup()
                    .UseEntityFramework(_ => _
                        .AuditTypeMapper(t => typeof(AuditLog))
                        .AuditEntityAction<AuditLog>((ev, entry, entity) =>
                        {
                            entity.AuditData = entry.ToJson();
                            entity.EntityType = entry.EntityType.Name;
                            entity.AuditAction = entry.Action;
                            entity.AuditDate = DateTime.Now;
                            entity.AuditUser = Environment.UserName;
                            entity.TablePk = entry.PrimaryKey.First().Value.ToString();
                        })
                    .IgnoreMatchedProperties(true));

            services.AddControllersWithViews();

```

# Execute

Execute the following command on the bash, at the POC.Audit folder, to run the application:

```bash

dotnet run

```

Access the Person menu, create and person an then access the Audit menu to see the audities.

# Reference

[Audit.Net](https://github.com/thepirat000/Audit.NET)