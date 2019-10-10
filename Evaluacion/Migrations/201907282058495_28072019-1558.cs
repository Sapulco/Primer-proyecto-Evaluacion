namespace Evaluacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _280720191558 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pregunta", "Nombre", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pregunta", "Nombre");
        }
    }
}
