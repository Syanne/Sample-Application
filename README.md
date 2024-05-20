# Db Setup

1. Please create an empty database with user, that has an db_owner role.
2. Then replace a ReadWriteDbConnection in Sample.Auth.Api/appsettings.json 
3. Select Sample.Auth.Api (Identity server) as Startup
4. Execure following instructions:
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
simply writes a log to Logs table



