using Celler.Dominio.Entidades;
using Celler.Dominio.Models;

namespace Celler.Infraestrutura.Servicos
{
    public class Notificar
    {
        public Usuario Usuario { get; set; }
        public Anuncio Anuncio { get; set; }
        public Usuario UsuarioNotificar { get; set; }

        public Notificar(Usuario usuario, Anuncio anuncio, Usuario usuarioNotificar)
        {
            this.Usuario = usuario;
            this.Anuncio = anuncio;
            this.UsuarioNotificar = usuarioNotificar;
        }

        public void NotificarUsuarioComentario()
        {
            if (this.UsuarioNotificar.NotificacaoComentarioAnuncioEmail == true)
            {
                EnviarEmail email = new EnviarEmail();
                MensagemModel modelEmail = new MensagemModel("Celler", this.Usuario.Nome + " comentou no anúncio: " + this.Anuncio.Titulo);
                email.enviar(this.UsuarioNotificar.Email, modelEmail);
            }

            if (this.UsuarioNotificar.NotificacaoComentarioAnuncioSlack == true)
            {
                EnviarMensagemSlack enviar = new EnviarMensagemSlack(this.UsuarioNotificar.CanalSlack, this.Usuario.Nome + " comentou no seu anúncio: " + this.Anuncio.Titulo);
            }
        }

        public void NotificarUsuarioEvento()
        {
            if (this.UsuarioNotificar.NotificacaoPresencaEmail == true)
            {
                EnviarEmail email = new EnviarEmail();
                MensagemModel modelEmail = new MensagemModel("Celler", this.Usuario.Nome + " confirmou presença no evento: " + this.Anuncio.Titulo);
                email.enviar(this.UsuarioNotificar.Email, modelEmail);
            }

            if (this.UsuarioNotificar.NotificacaoPresencaSlack == true)
            {
                EnviarMensagemSlack enviar = new EnviarMensagemSlack(this.UsuarioNotificar.CanalSlack, this.Usuario.Nome + " confirmou presença no evento: " + this.Anuncio.Titulo);
            }
        }

        public void NotificarUsuarioDesistirEvento()
        {
            if (this.UsuarioNotificar.NotificacaoPresencaEmail == true)
            {
                EnviarEmail email = new EnviarEmail();
                MensagemModel modelEmail = new MensagemModel("Celler", this.Usuario.Nome + " desistiu do evento: " + this.Anuncio.Titulo);
                email.enviar(this.UsuarioNotificar.Email, modelEmail);
            }

            if (this.UsuarioNotificar.NotificacaoPresencaSlack == true)
            {
                EnviarMensagemSlack enviar = new EnviarMensagemSlack(this.UsuarioNotificar.CanalSlack, this.Usuario.Nome + " desistiu do evento: " + this.Anuncio.Titulo);
            }
        }



        public void NotificarUsuarioInteresse()
        {
            if (this.UsuarioNotificar.NotificacaoInteresseEmail == true)
            {
                EnviarEmail email = new EnviarEmail();
                MensagemModel modelEmail = new MensagemModel("Celler", this.Usuario.Nome + " se interessou no seu evento: " + this.Anuncio.Titulo);
                email.enviar(this.UsuarioNotificar.Email, modelEmail);
            }

            if (this.UsuarioNotificar.NotificacaoInteresseSlack == true)
            {
                EnviarMensagemSlack enviar = new EnviarMensagemSlack(this.UsuarioNotificar.CanalSlack, this.Usuario.Nome + " se interessou no seu evento: " + this.Anuncio.Titulo);
            }
        }

        public void NotificarUsuarioDesinteresse()
        {
            if (this.UsuarioNotificar.NotificacaoInteresseEmail == true)
            {
                EnviarEmail email = new EnviarEmail();
                MensagemModel modelEmail = new MensagemModel("Celler", this.Usuario.Nome + " cancelou o interesse no seu: " + this.Anuncio.Titulo);
                email.enviar(this.UsuarioNotificar.Email, modelEmail);
            }

            if (this.UsuarioNotificar.NotificacaoInteresseSlack == true)
            {
                EnviarMensagemSlack enviar = new EnviarMensagemSlack(this.UsuarioNotificar.CanalSlack, this.Usuario.Nome + " cancelou o interesse no seu: " + this.Anuncio.Titulo);
            }
        }

        public void NotificarUsuarioDoacaoVaquinha()
        {
            if (this.UsuarioNotificar.NotificacaoDoacaoVaquinhaEmail == true)
            {
                EnviarEmail email = new EnviarEmail();
                MensagemModel modelEmail = new MensagemModel("Celler", this.Usuario.Nome + " doou para a vaquinha: " + this.Anuncio.Titulo);
                email.enviar(this.UsuarioNotificar.Email, modelEmail);
            }

            if (this.UsuarioNotificar.NotificacaoDoacaoVaquinhaSlack == true)
            {
                EnviarMensagemSlack enviar = new EnviarMensagemSlack(this.UsuarioNotificar.CanalSlack, this.Usuario.Nome + " Doou para a vaquinha: " + this.Anuncio.Titulo);
            }

        }
    }
}
