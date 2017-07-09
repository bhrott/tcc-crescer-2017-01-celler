namespace Celler.Infraestrutura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterartipodecharparastring : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produto", "Status", c => c.String());
            AddColumn("dbo.Doador", "Status", c => c.String());
            AddColumn("dbo.Notificacao", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notificacao", "Status");
            DropColumn("dbo.Doador", "Status");
            DropColumn("dbo.Produto", "Status");
        }
    }
}
