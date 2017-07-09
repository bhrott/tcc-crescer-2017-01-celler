namespace Celler.Infraestrutura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AbstrairStatusDeAnuncio : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Anuncio", "Status", c => c.String());
            DropColumn("dbo.Evento", "Status");
            DropColumn("dbo.Produto", "Status");
            DropColumn("dbo.Vaquinha", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vaquinha", "Status", c => c.String());
            AddColumn("dbo.Produto", "Status", c => c.String());
            AddColumn("dbo.Evento", "Status", c => c.String());
            DropColumn("dbo.Anuncio", "Status");
        }
    }
}
