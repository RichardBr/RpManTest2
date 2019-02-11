

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

dotnet ef dbcontext scaffold "Server=.;Database=RpMan;Integrated Security=true;" Microsoft.EntityFrameworkCore.SqlServer -o ReverseDbData\Entities --context-dir ReverseDbData\Context -c RpManDbContext -f

(NB for convenience the above command has been put into bat file "ReverseDbData.bat" which can be run instead)

Then to get it to conform to correct namespaces to a project wide find & replace 
- Find "R pMan.Persistence.ReverseDbData.Entities" and replace with "RpMan.Domain.Entities"

Then final run "GenerateEntityConfigFiles.cs" in the RpMan.ConsoleApp project. This will create individual entity configuration files, Which should be moved to Configuration folder
