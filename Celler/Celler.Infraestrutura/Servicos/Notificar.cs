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
            if (this.Usuario.NotificacaoComentarioAnuncioEmail == true)
            {
                EnviarEmail email = new EnviarEmail();
                MensagemModel modelEmail = new MensagemModel("Celler", this.Usuario.Nome + " comentou no anúncio: " + this.Anuncio.Titulo);
                email.enviar(this.Usuario.Email, modelEmail);
                EnviarMensagemSlack enviar = new EnviarMensagemSlack(this.Usuario.CanalSlack, this.Usuario.Nome + " comentou no seu anúncio: " + this.Anuncio.Titulo);
            }

            if(this.Usuario.NotificacaoComentarioAnuncioSlack == true)
            {
                EnviarMensagemSlack enviar = new EnviarMensagemSlack(this.Usuario.CanalSlack, this.Usuario.Nome + " comentou no seu anúncio: " + this.Anuncio.Titulo);
            }

        }

        public void NotificarUsuarioEvento()
        {
            if (this.Usuario.NotificacaoComentarioAnuncioEmail == true)
            {
                EnviarEmail email = new EnviarEmail();
                MensagemModel modelEmail = new MensagemModel("Celler", this.Usuario.Nome + " confirmou presença no evento: " + this.Anuncio.Titulo);
                email.enviar(this.Usuario.Email, modelEmail);
                EnviarMensagemSlack enviar = new EnviarMensagemSlack(this.Usuario.CanalSlack, this.Usuario.Nome + " confirmou presença no evento: " + this.Anuncio.Titulo);
            }

            if (this.Usuario.NotificacaoComentarioAnuncioSlack == true)
            {
                EnviarMensagemSlack enviar = new EnviarMensagemSlack(this.Usuario.CanalSlack, this.Usuario.Nome + " confirmou presença no evento: " + this.Anuncio.Titulo);
            }

        }

        public void NotificarUsuarioInteresse()
        {
            if (this.Usuario.NotificacaoComentarioAnuncioEmail == true)
            {
                EnviarEmail email = new EnviarEmail();
                MensagemModel modelEmail = new MensagemModel("Celler", this.Usuario.Nome + " se interessou no seu: " + this.Anuncio.Titulo);
                email.enviar(this.Usuario.Email, modelEmail);
                EnviarMensagemSlack enviar = new EnviarMensagemSlack(this.Usuario.CanalSlack, this.Usuario.Nome + " se interessou no seu: " + this.Anuncio.Titulo);
            }

            if (this.Usuario.NotificacaoComentarioAnuncioSlack == true)
            {
                EnviarMensagemSlack enviar = new EnviarMensagemSlack(this.Usuario.CanalSlack, this.Usuario.Nome + " se interessou no seu: " + this.Anuncio.Titulo);
            }

        }
    }
}
