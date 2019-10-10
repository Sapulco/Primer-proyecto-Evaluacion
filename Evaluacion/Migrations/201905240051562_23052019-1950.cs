namespace Evaluacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _230520191950 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Examen", "NombreExamen", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Examen", "NombreExamen");
        }
    }
}
