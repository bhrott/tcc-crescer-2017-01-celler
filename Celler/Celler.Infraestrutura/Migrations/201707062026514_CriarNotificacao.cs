namespace Celler.Infraestrutura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriarNotificacao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notificacao",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Texto = c.String(),
                        IdUsuario = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.IdUsuario, cascadeDelete: true)
                .Index(t => t.IdUsuario);
            
            AddColumn("dbo.Usuario", "NotificacaoComentarioAnuncioEmail", c => c.Boolean(nullable: false));
            AddColumn("dbo.Usuario", "NotificacaoComentarioAnuncioSlack", c => c.Boolean(nullable: false));
            AddColumn("dbo.Usuario", "NotificacaoComentarioAnuncioBrowser", c => c.Boolean(nullable: false));
            AddColumn("dbo.Usuario", "NotificacaoPresencaEmail", c => c.Boolean(nullable: false));
            AddColumn("dbo.Usuario", "NotificacaoPresencaSlack", c => c.Boolean(nullable: false));
            AddColumn("dbo.Usuario", "NotificacaoPresencaBrowser", c => c.Boolean(nullable: false));
            AddColumn("dbo.Usuario", "NotificacaoInteresseEmail", c => c.Boolean(nullable: false));
            AddColumn("dbo.Usuario", "NotificacaoInteresseSlack", c => c.Boolean(nullable: false));
            AddColumn("dbo.Usuario", "NotificacaoInteresseBrowser", c => c.Boolean(nullable: false));
            AddColumn("dbo.Usuario", "NotificacaoDoacaoVaquinhaEmail", c => c.Boolean(nullable: false));
            AddColumn("dbo.Usuario", "NotificacaoDoacaoVaquinhaSlack", c => c.Boolean(nullable: false));
            AddColumn("dbo.Usuario", "NotificacaoDoacaoVaquinhaBrowser", c => c.Boolean(nullable: false));
            AddColumn("dbo.Usuario", "CanalSlack", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notificacao", "IdUsuario", "dbo.Usuario");
            DropIndex("dbo.Notificacao", new[] { "IdUsuario" });
            DropColumn("dbo.Usuario", "CanalSlack");
            DropColumn("dbo.Usuario", "NotificacaoDoacaoVaquinhaBrowser");
            DropColumn("dbo.Usuario", "NotificacaoDoacaoVaquinhaSlack");
            DropColumn("dbo.Usuario", "NotificacaoDoacaoVaquinhaEmail");
            DropColumn("dbo.Usuario", "NotificacaoInteresseBrowser");
            DropColumn("dbo.Usuario", "NotificacaoInteresseSlack");
            DropColumn("dbo.Usuario", "NotificacaoInteresseEmail");
            DropColumn("dbo.Usuario", "NotificacaoPresencaBrowser");
            DropColumn("dbo.Usuario", "NotificacaoPresencaSlack");
            DropColumn("dbo.Usuario", "NotificacaoPresencaEmail");
            DropColumn("dbo.Usuario", "NotificacaoComentarioAnuncioBrowser");
            DropColumn("dbo.Usuario", "NotificacaoComentarioAnuncioSlack");
            DropColumn("dbo.Usuario", "NotificacaoComentarioAnuncioEmail");
            DropTable("dbo.Notificacao");
        }
    }
}
