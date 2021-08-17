using Microsoft.EntityFrameworkCore.Migrations;

namespace DDD.DataAccess.Migrations
{
    public partial class AddMandatoryPasswordForNonSocialCheckConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE Users ADD CONSTRAINT CK_Users_MandatoryPasswordForNonSocial CHECK ((IsSocialLogin = 1 AND Password IS NULL) OR (IsSocialLogin = 0 AND Password IS NOT NULL))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE Users DROP CONSTRAINT CK_Users_MandatoryPasswordForNonSocial;");
        }
    }
}
