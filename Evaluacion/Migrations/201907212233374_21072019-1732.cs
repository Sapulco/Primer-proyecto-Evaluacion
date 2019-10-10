namespace Evaluacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _210720191732 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Resultado",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UsuarioID = c.Int(nullable: false),
                        FechaRespuesta = c.DateTime(nullable: false),
                        PreguntaID = c.Int(nullable: false),
                        Respuesta = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pregunta", t => t.PreguntaID, cascadeDelete: true)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID, cascadeDelete: true)
                .Index(t => t.UsuarioID)
                .Index(t => t.PreguntaID);
            
            CreateTable(
                "dbo.Pregunta",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ExamenID = c.Int(nullable: false),
                        TipoPreguntaID = c.Int(nullable: false),
                        Orden = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Examen", t => t.ExamenID, cascadeDelete: false)
                .ForeignKey("dbo.TipoPregunta", t => t.TipoPreguntaID, cascadeDelete: true)
                .Index(t => t.ExamenID)
                .Index(t => t.TipoPreguntaID);
            
            CreateTable(
                "dbo.Opcion",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Valor = c.String(),
                        PreguntaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pregunta", t => t.PreguntaID, cascadeDelete: true)
                .Index(t => t.PreguntaID);
            
            CreateTable(
                "dbo.TipoPregunta",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        IsListado = c.Boolean(nullable: false),
                        IsMultiple = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Resultado", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.Pregunta", "TipoPreguntaID", "dbo.TipoPregunta");
            DropForeignKey("dbo.Resultado", "PreguntaID", "dbo.Pregunta");
            DropForeignKey("dbo.Opcion", "PreguntaID", "dbo.Pregunta");
            DropForeignKey("dbo.Pregunta", "ExamenID", "dbo.Examen");
            DropIndex("dbo.Opcion", new[] { "PreguntaID" });
            DropIndex("dbo.Pregunta", new[] { "TipoPreguntaID" });
            DropIndex("dbo.Pregunta", new[] { "ExamenID" });
            DropIndex("dbo.Resultado", new[] { "PreguntaID" });
            DropIndex("dbo.Resultado", new[] { "UsuarioID" });
            DropTable("dbo.TipoPregunta");
            DropTable("dbo.Opcion");
            DropTable("dbo.Pregunta");
            DropTable("dbo.Resultado");
        }
    }
}
