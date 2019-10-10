namespace Evaluacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _190520192009 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departamento",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Examen",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DepartamentoID = c.Int(nullable: false),
                        UsuarioAltaID = c.Int(nullable: false),
                        FechaAlta = c.DateTime(nullable: false),
                        IsLiberado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioAltaID, cascadeDelete: true)
                .ForeignKey("dbo.Departamento", t => t.DepartamentoID, cascadeDelete: true)
                .Index(t => t.DepartamentoID)
                .Index(t => t.UsuarioAltaID);
            
            CreateTable(
                "dbo.PerfilDepartamento",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PerfilID = c.Int(nullable: false),
                        DepartamentoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Perfil", t => t.PerfilID, cascadeDelete: true)
                .ForeignKey("dbo.Departamento", t => t.DepartamentoID, cascadeDelete: true)
                .Index(t => t.PerfilID)
                .Index(t => t.DepartamentoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PerfilDepartamento", "DepartamentoID", "dbo.Departamento");
            DropForeignKey("dbo.Examen", "DepartamentoID", "dbo.Departamento");
            DropForeignKey("dbo.PerfilDepartamento", "PerfilID", "dbo.Perfil");
            DropForeignKey("dbo.Examen", "UsuarioAltaID", "dbo.Usuario");
            DropIndex("dbo.PerfilDepartamento", new[] { "DepartamentoID" });
            DropIndex("dbo.PerfilDepartamento", new[] { "PerfilID" });
            DropIndex("dbo.Examen", new[] { "UsuarioAltaID" });
            DropIndex("dbo.Examen", new[] { "DepartamentoID" });
            DropTable("dbo.PerfilDepartamento");
            DropTable("dbo.Examen");
            DropTable("dbo.Departamento");
        }
    }
}
