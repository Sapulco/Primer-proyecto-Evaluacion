namespace Evaluacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _150520191935 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Modulo", "ActionName", c => c.String());
            AddColumn("dbo.Modulo", "ControllerName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Modulo", "ControllerName");
            DropColumn("dbo.Modulo", "ActionName");
        }
    }
}
