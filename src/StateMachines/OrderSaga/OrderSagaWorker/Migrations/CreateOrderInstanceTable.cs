//using Microsoft.EntityFrameworkCore.Migrations;

//namespace OrderSaga.Worker.Migrations
//{
//    public partial class CreateOrderInstanceTable : Migration
//    {
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.CreateTable(
//                "OrderStateInstance",
//                table => new
//                {
//                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
//                    CurrentState = table.Column<string>(type: "text", nullable: true),
//                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
//                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_OrderStateInstance", x => x.CorrelationId);
//                });
//        }

//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropTable(
//                name: "OrderStateInstance");
//        }
//    }
//}