namespace Celler.Infraestrutura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adicionarstatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Evento", "Status", c => c.String());
            AddColumn("dbo.Vaquinha", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vaquinha", "Status");
            DropColumn("dbo.Evento", "Status");
        }
    }
}
