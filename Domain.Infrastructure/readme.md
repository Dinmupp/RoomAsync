Add Migration:

dotnet ef migrations add "NAME" --context ApplicationDbContext --startup-project ../RoomAsync/RoomAsync.Web\RoomAsync.Web.Web\RoomAsync.Web.Web.csproj --project ../RoomAsync/Domain.Infrastructure/Domain.Infrastructure.csproj


Update Database:

dotnet ef database update --context ApplicationDbContext --startup-project ../RoomAsync/RoomAsync.Web\RoomAsync.Web.Web\RoomAsync.Web.Web.csproj --project ../RoomAsync/Domain.Infrastructure/Domain.Infrastructure.csproj