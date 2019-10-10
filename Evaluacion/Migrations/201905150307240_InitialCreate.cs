namespace Evaluacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Modulo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        IsActivo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PerfilModulo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PerfilID = c.Int(nullable: false),
                        ModuloID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Perfil", t => t.PerfilID, cascadeDelete: true)
                .ForeignKey("dbo.Modulo", t => t.ModuloID, cascadeDelete: true)
                .Index(t => t.PerfilID)
                .Index(t => t.ModuloID);
            
            CreateTable(
                "dbo.Perfil",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        IsAdministrador = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CuentaUsuario = c.String(),
                        Contrasena = c.String(),
                        Nombre = c.String(),
                        Paterno = c.String(),
                        Materno = c.String(),
                        FechaNacimiento = c.DateTime(nullable: false),
                        PerfilID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Perfil", t => t.PerfilID, cascadeDelete: true)
                .Index(t => t.PerfilID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PerfilModulo", "ModuloID", "dbo.Modulo");
            DropForeignKey("dbo.Usuario", "PerfilID", "dbo.Perfil");
            DropForeignKey("dbo.PerfilModulo", "PerfilID", "dbo.Perfil");
            DropIndex("dbo.Usuario", new[] { "PerfilID" });
            DropIndex("dbo.PerfilModulo", new[] { "ModuloID" });
            DropIndex("dbo.PerfilModulo", new[] { "PerfilID" });
            DropTable("dbo.Usuario");
            DropTable("dbo.Perfil");
            DropTable("dbo.PerfilModulo");
            DropTable("dbo.Modulo");
        }
    }
}
