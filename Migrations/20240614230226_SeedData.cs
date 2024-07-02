using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Text;

#nullable disable

namespace FieldMIRServices.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />

            const string ADMIN_ROLE_GUID = "02114ac2-38de-42fe-9397-dd8761fffc40";
            const string USER_ROLE_GUID = "55eafc070-f066-4d45-ab43-19dcb38290e2";

            const string ADMIN_USER_GUID = "c757b302-2e31-4308-bdc1-a2359fc6cc46";
            const string USER_USER_GUID = "1ba31155-408b-4974-b6a8-f93f4861a85e";

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var hasher = new PasswordHasher<IdentityUser>();

            var passwordHash = hasher.HashPassword(null, "Pa$$w0rd1!");
            var passworHash1 = hasher.HashPassword(null, "Pa$$w0rd2!");

            AddUser(migrationBuilder, "admin@FieldMRIServices.com", passwordHash, ADMIN_USER_GUID);

            AddUser(migrationBuilder, "user@FieldMRIServices.com", passworHash1, USER_USER_GUID);


            AddRole(migrationBuilder, "Admin", ADMIN_ROLE_GUID);
            AddRole(migrationBuilder, "USER", USER_ROLE_GUID);

            AddUserToRole(migrationBuilder, ADMIN_USER_GUID, ADMIN_ROLE_GUID);
            AddUserToRole(migrationBuilder, USER_USER_GUID, USER_ROLE_GUID);
        }
        private void AddUser(MigrationBuilder migrationBuilder, string email, string passwordHash, string userGuid)
        {
            StringBuilder sb = new StringBuilder();

            string emailCaps = email.ToUpper();

            sb.AppendLine("INSERT INTO AspNetUsers(Id, UserName, NormalizedUserName,Email,EmailConfirmed,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnabled,AccessFailedCount,NormalizedEmail,PasswordHash,SecurityStamp)");
            sb.AppendLine("VALUES(");
            sb.AppendLine($"'{userGuid}'");
            sb.AppendLine($",'{email}'");
            sb.AppendLine($",'{emailCaps}'");
            sb.AppendLine($",'{email}'");
            sb.AppendLine(", 0");
            sb.AppendLine(", 0");
            sb.AppendLine(", 0");
            sb.AppendLine(", 0");
            sb.AppendLine(", 0");
            sb.AppendLine($",'{emailCaps}'");
            sb.AppendLine($", '{passwordHash}'");
            sb.AppendLine(", ''");
            sb.AppendLine(")");

            migrationBuilder.Sql(sb.ToString());
        }

        private void AddRole(MigrationBuilder migrationBuilder, string roleName, string roleGuid)
        {
            string roleNameCaps = roleName.ToUpper();

            migrationBuilder.Sql($"INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('{roleGuid}','{roleName}','{roleNameCaps}')");


        }

        private void AddUserToRole(MigrationBuilder migrationBuilder, string userGuid, string roleGuid)
        {
            migrationBuilder.Sql($"INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES ('{userGuid}','{roleGuid}')");


        }
    }

    /// <inheritdoc />
   
}
