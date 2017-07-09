namespace Celler.Infraestrutura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adicionarcampolinknanotificacao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notificacao", "Link", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notificacao", "Link");
        }
    }
}
