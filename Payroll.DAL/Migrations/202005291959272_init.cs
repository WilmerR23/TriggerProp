namespace Payroll.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DocumentNumber = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CrudeSalary = c.Double(),
                        AccountNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PayrollSheets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PayrollDate = c.DateTime(nullable: false),
                        ISRDiscount = c.Double(),
                        AFPDiscount = c.Double(),
                        OthersDiscounts = c.Double(),
                        TotalDiscounts = c.Double(),
                        NetSalary = c.Double(),
                        EmployeeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PayrollSheets", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.PayrollSheets", new[] { "EmployeeId" });
            DropTable("dbo.PayrollSheets");
            DropTable("dbo.Employees");
        }
    }
}
