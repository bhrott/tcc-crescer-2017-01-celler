namespace Celler.Infraestrutura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTipoAEntidadeAnuncio : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Anuncio", "TipoAnuncio", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Anuncio", "TipoAnuncio");
        }
    }
}
