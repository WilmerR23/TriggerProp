namespace Payroll.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial23 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Asientoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Numero = c.Int(nullable: false),
                        Descripcion = c.String(),
                        FechaGenerado = c.DateTime(nullable: false),
                        CuentaContable = c.Long(nullable: false),
                        Monto = c.Int(nullable: false),
                        TipoMovimiento = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Asientoes");
        }
    }
}
