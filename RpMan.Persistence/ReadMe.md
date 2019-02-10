

Migrations
==========

1) Define/Change models
-----------------------


2) Create a migration.
----------------------
Do this by running command "add-migration [migration-name]" in package manager console. Remember to select Persistence project.



3) Apply Migration to DB or script
----------------------------------
Do this by running command "update-database --verbose" in package manager console. Remember to select Persistence project.



Reverse Generate Existing DB
============================
Do this by running command dotnet command below in termainal window console. Remember to select Persistence project.

dotnet ef dbcontext scaffold "Server=.;Database=RpMan;Integrated Security=true;" Microsoft.EntityFrameworkCore.SqlServer -o ReverseDbData --context-dir ReverseDbData\Context -c RpManDbContext -f


