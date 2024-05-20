# Db Setup

1. Please create an empty database with user, that has an db_owner role.
2. Then replace a ReadWriteDbConnection in Sample.Auth.Api/appsettings.json 
3. Select Sample.Auth.Api (Identity server) as Startup
4. Execute following instructions:
   ```
    Update-Database -Project Sample.Auth.DataAccess.MsSql -Context ConfigurationDbContext
    Update-Database -Project Sample.Auth.DataAccess.MsSql -Context PersistedGrantDbContext
    Update-Database -Project Sample.Auth.DataAccess.MsSql -Context ReadWriteDbContext
   ```

# Run application
Please run both Sample.Auth.Api and Sample.ConsoleApp and following instructions in Sample.ConsoleApp

You have a pre-set up user. Credentials:
userName: sampleUser@test.com
password: sampleUser@test.com

# Post-login event
Implementedonly in Identity Server. When any post-login event is raised, sink simply writes a log to Logs table

# Further improvements
1. Sample.ConsoleApp should be moved to other solution
2. Sample.Auth.Models should be used as a nuget, not project reference
3. Generate and return a JWT token after successfull authentification
