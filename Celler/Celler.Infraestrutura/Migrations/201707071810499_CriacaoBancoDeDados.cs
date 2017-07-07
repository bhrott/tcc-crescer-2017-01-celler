namespace Celler.Infraestrutura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaoBancoDeDados : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Anuncio",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(),
                        Descricao = c.String(),
                        Foto1 = c.String(),
                        Foto2 = c.String(),
                        Foto3 = c.String(),
                        DataAnuncio = c.DateTime(nullable: false),
                        IdCriador = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.IdCriador, cascadeDelete: true)
                .Index(t => t.IdCriador);
            
            CreateTable(
                "dbo.Comentario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Texto = c.String(),
                        DataComentario = c.DateTime(nullable: false),
                        IdUsuario = c.Int(nullable: false),
                        IdAnuncio = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.IdUsuario, cascadeDelete: true)
                .ForeignKey("dbo.Anuncio", t => t.IdAnuncio)
                .Index(t => t.IdUsuario)
                .Index(t => t.IdAnuncio);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Email = c.String(),
                        Senha = c.String(),
                        NotificacaoComentarioAnuncioEmail = c.Boolean(nullable: false),
                        NotificacaoComentarioAnuncioSlack = c.Boolean(nullable: false),
                        NotificacaoComentarioAnuncioBrowser = c.Boolean(nullable: false),
                        NotificacaoPresencaEmail = c.Boolean(nullable: false),
                        NotificacaoPresencaSlack = c.Boolean(nullable: false),
                        NotificacaoPresencaBrowser = c.Boolean(nullable: false),
                        NotificacaoInteresseEmail = c.Boolean(nullable: false),
                        NotificacaoInteresseSlack = c.Boolean(nullable: false),
                        NotificacaoInteresseBrowser = c.Boolean(nullable: false),
                        NotificacaoDoacaoVaquinhaEmail = c.Boolean(nullable: false),
                        NotificacaoDoacaoVaquinhaSlack = c.Boolean(nullable: false),
                        NotificacaoDoacaoVaquinhaBrowser = c.Boolean(nullable: false),
                        CanalSlack = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Permissaos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Usuario_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.Usuario_Id)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.Doador",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ValorDoado = c.Double(nullable: false),
                        IdUsuario = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.IdUsuario, cascadeDelete: true)
                .Index(t => t.IdUsuario);
            
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
            
            CreateTable(
                "dbo.ConfirmadoEvento",
                c => new
                    {
                        IdEvento = c.Int(nullable: false),
                        IdUsuario = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdEvento, t.IdUsuario })
                .ForeignKey("dbo.Evento", t => t.IdEvento, cascadeDelete: true)
                .ForeignKey("dbo.Usuario", t => t.IdUsuario, cascadeDelete: true)
                .Index(t => t.IdEvento)
                .Index(t => t.IdUsuario);
            
            CreateTable(
                "dbo.InteressadoProduto",
                c => new
                    {
                        IdProduto = c.Int(nullable: false),
                        IdUsuario = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdProduto, t.IdUsuario })
                .ForeignKey("dbo.Produto", t => t.IdProduto, cascadeDelete: true)
                .ForeignKey("dbo.Usuario", t => t.IdUsuario, cascadeDelete: true)
                .Index(t => t.IdProduto)
                .Index(t => t.IdUsuario);
            
            CreateTable(
                "dbo.DoadorVaquinha",
                c => new
                    {
                        IdVaquinha = c.Int(nullable: false),
                        IdDoador = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdVaquinha, t.IdDoador })
                .ForeignKey("dbo.Vaquinha", t => t.IdVaquinha, cascadeDelete: true)
                .ForeignKey("dbo.Doador", t => t.IdDoador, cascadeDelete: true)
                .Index(t => t.IdVaquinha)
                .Index(t => t.IdDoador);
            
            CreateTable(
                "dbo.Evento",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        DataRealizacao = c.DateTime(nullable: false),
                        Local = c.String(),
                        DataMaximaConfirmacao = c.DateTime(nullable: false),
                        ValorPorPessoa = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Anuncio", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Produto",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        IdComprador = c.Int(),
                        Valor = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Anuncio", t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.IdComprador)
                .Index(t => t.Id)
                .Index(t => t.IdComprador);
            
            CreateTable(
                "dbo.Vaquinha",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ArrecadamentoPrevisto = c.Double(nullable: false),
                        TotalArrecadado = c.Double(nullable: false),
                        DateTermino = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Anuncio", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vaquinha", "Id", "dbo.Anuncio");
            DropForeignKey("dbo.Produto", "IdComprador", "dbo.Usuario");
            DropForeignKey("dbo.Produto", "Id", "dbo.Anuncio");
            DropForeignKey("dbo.Evento", "Id", "dbo.Anuncio");
            DropForeignKey("dbo.Notificacao", "IdUsuario", "dbo.Usuario");
            DropForeignKey("dbo.DoadorVaquinha", "IdDoador", "dbo.Doador");
            DropForeignKey("dbo.DoadorVaquinha", "IdVaquinha", "dbo.Vaquinha");
            DropForeignKey("dbo.Doador", "IdUsuario", "dbo.Usuario");
            DropForeignKey("dbo.InteressadoProduto", "IdUsuario", "dbo.Usuario");
            DropForeignKey("dbo.InteressadoProduto", "IdProduto", "dbo.Produto");
            DropForeignKey("dbo.ConfirmadoEvento", "IdUsuario", "dbo.Usuario");
            DropForeignKey("dbo.ConfirmadoEvento", "IdEvento", "dbo.Evento");
            DropForeignKey("dbo.Anuncio", "IdCriador", "dbo.Usuario");
            DropForeignKey("dbo.Comentario", "IdAnuncio", "dbo.Anuncio");
            DropForeignKey("dbo.Comentario", "IdUsuario", "dbo.Usuario");
            DropForeignKey("dbo.Permissaos", "Usuario_Id", "dbo.Usuario");
            DropIndex("dbo.Vaquinha", new[] { "Id" });
            DropIndex("dbo.Produto", new[] { "IdComprador" });
            DropIndex("dbo.Produto", new[] { "Id" });
            DropIndex("dbo.Evento", new[] { "Id" });
            DropIndex("dbo.DoadorVaquinha", new[] { "IdDoador" });
            DropIndex("dbo.DoadorVaquinha", new[] { "IdVaquinha" });
            DropIndex("dbo.InteressadoProduto", new[] { "IdUsuario" });
            DropIndex("dbo.InteressadoProduto", new[] { "IdProduto" });
            DropIndex("dbo.ConfirmadoEvento", new[] { "IdUsuario" });
            DropIndex("dbo.ConfirmadoEvento", new[] { "IdEvento" });
            DropIndex("dbo.Notificacao", new[] { "IdUsuario" });
            DropIndex("dbo.Doador", new[] { "IdUsuario" });
            DropIndex("dbo.Permissaos", new[] { "Usuario_Id" });
            DropIndex("dbo.Comentario", new[] { "IdAnuncio" });
            DropIndex("dbo.Comentario", new[] { "IdUsuario" });
            DropIndex("dbo.Anuncio", new[] { "IdCriador" });
            DropTable("dbo.Vaquinha");
            DropTable("dbo.Produto");
            DropTable("dbo.Evento");
            DropTable("dbo.DoadorVaquinha");
            DropTable("dbo.InteressadoProduto");
            DropTable("dbo.ConfirmadoEvento");
            DropTable("dbo.Notificacao");
            DropTable("dbo.Doador");
            DropTable("dbo.Permissaos");
            DropTable("dbo.Usuario");
            DropTable("dbo.Comentario");
            DropTable("dbo.Anuncio");
        }
    }
}
