dotnet ef dbcontext scaffold "Server=.;Database=RpMan;Integrated Security=true;" Microsoft.EntityFrameworkCore.SqlServer -o ReverseDbData\Entities --context-dir ReverseDbData\Context -c RpManDbContext -f