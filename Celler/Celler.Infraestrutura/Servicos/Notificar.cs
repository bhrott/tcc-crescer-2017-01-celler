using Celler.Dominio.Entidades;
using Celler.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Infraestrutura.Servicos
{
    public class Notificar
    {
        public Usuario Usuario { get; set; }
        public Anuncio Anuncio { get; set; }

        public Notificar(Usuario usuario, Anuncio anuncio)
        {
            this.Usuario = usuario;
            this.Anuncio = anuncio;
        }

        public void NotificarUsuarioComentario()
        {
            EnviarNotificacao(" comentou na sua publicação: ");
        }

        public void NotificarUsuarioEvento()
        {
            EnviarNotificacao(" confirmou presença no seu evento: ");
        }

        public void NotificarUsuarioInteresse()
        {
            EnviarNotificacao(" se interessou no seu produto: ");
        }

        private void EnviarNotificacao(string conteudoNotificacao)
        {
            if (this.Usuario.NotificacaoComentarioAnuncioEmail == true)
            {
                EnviarEmail email = new EnviarEmail();
                MensagemModel modelEmail = new MensagemModel("Celler", this.Usuario.Nome + conteudoNotificacao + this.Anuncio.Titulo);
                email.enviar(this.Usuario.Email, modelEmail);
                EnviarMensagemSlack enviar = new EnviarMensagemSlack(this.Usuario.CanalSlack, this.Usuario.Nome + conteudoNotificacao + this.Anuncio.Titulo);
            }

            if (this.Usuario.NotificacaoComentarioAnuncioSlack == true)
            {
                EnviarMensagemSlack enviar = new EnviarMensagemSlack(this.Usuario.CanalSlack, this.Usuario.Nome + conteudoNotificacao + this.Anuncio.Titulo);
            }
        }
    }
}
