using Celler.Dominio.Entidades;
using Celler.Dominio.Models;
using Celler.Infraestrutura.Repositorios;

namespace Celler.Infraestrutura.Servicos
{
    public class Notificar
    {
        public Usuario Usuario { get; set; }
        public Anuncio Anuncio { get; set; }
        public Usuario UsuarioNotificar { get; set; }

        readonly NotificacaoRepositorio _notificacaoRepositorio;

        public Notificar(Usuario usuario, Anuncio anuncio, Usuario usuarioNotificar, NotificacaoRepositorio _notificacaoRepositorio)
        {
            this.Usuario = usuario;
            this.Anuncio = anuncio;
            this.UsuarioNotificar = usuarioNotificar;
            this._notificacaoRepositorio = _notificacaoRepositorio;
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

            if (this.UsuarioNotificar.NotificacaoComentarioAnuncioBrowser == true)
            {
                Notificacao notificacao = new Notificacao(this.Usuario.Nome + " comentou no anúncio: " + this.Anuncio.Titulo, this.UsuarioNotificar, "#!/anuncio/" + this.Anuncio.Id);
                _notificacaoRepositorio.CriarNotificacao(notificacao);

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

            if (this.UsuarioNotificar.NotificacaoPresencaBrowser == true)
            {
                Notificacao notificacao = new Notificacao(this.Usuario.Nome + " confirmou presença no evento: " + this.Anuncio.Titulo, this.UsuarioNotificar, "#!/anuncio/" + this.Anuncio.Id);
                _notificacaoRepositorio.CriarNotificacao(notificacao);
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

            if (this.UsuarioNotificar.NotificacaoPresencaBrowser == true)
            {
                Notificacao notificacao = new Notificacao(this.Usuario.Nome + " desistiu do evento: " + this.Anuncio.Titulo, this.UsuarioNotificar, "#!/anuncio/" + this.Anuncio.Id);
                _notificacaoRepositorio.CriarNotificacao(notificacao);
            }
        }



        public void NotificarUsuarioInteresse()
        {
            if (this.UsuarioNotificar.NotificacaoInteresseEmail == true)
            {
                EnviarEmail email = new EnviarEmail();
                MensagemModel modelEmail = new MensagemModel("Celler", this.Usuario.Nome + " se interessou no seu produto: " + this.Anuncio.Titulo);
                email.enviar(this.UsuarioNotificar.Email, modelEmail);
            }

            if (this.UsuarioNotificar.NotificacaoInteresseSlack == true)
            {
                EnviarMensagemSlack enviar = new EnviarMensagemSlack(this.UsuarioNotificar.CanalSlack, this.Usuario.Nome + " se interessou no seu produto: " + this.Anuncio.Titulo);
            }

            if (this.UsuarioNotificar.NotificacaoInteresseBrowser == true)
            {
                 Notificacao notificacao = new Notificacao(this.Usuario.Nome + " se interessou no seu produto: " + this.Anuncio.Titulo, this.UsuarioNotificar, "#!/anuncio/" + this.Anuncio.Id);
                _notificacaoRepositorio.CriarNotificacao(notificacao);
            }
        }

        public void NotificarUsuarioDesinteresse()
        {
            if (this.UsuarioNotificar.NotificacaoInteresseEmail == true)
            {
                EnviarEmail email = new EnviarEmail();
                MensagemModel modelEmail = new MensagemModel("Celler", this.Usuario.Nome + " cancelou o interesse no seu produto: " + this.Anuncio.Titulo);
                email.enviar(this.UsuarioNotificar.Email, modelEmail);
            }

            if (this.UsuarioNotificar.NotificacaoInteresseSlack == true)
            {
                EnviarMensagemSlack enviar = new EnviarMensagemSlack(this.UsuarioNotificar.CanalSlack, this.Usuario.Nome + " cancelou o interesse no seu produto: " + this.Anuncio.Titulo);
            }

            if (this.UsuarioNotificar.NotificacaoInteresseBrowser == true)
            {
                Notificacao notificacao = new Notificacao(this.Usuario.Nome + " se interessou no seu produto: " + this.Anuncio.Titulo, this.UsuarioNotificar, "#!/anuncio/" + this.Anuncio.Id);
                _notificacaoRepositorio.CriarNotificacao(notificacao);
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

            if (this.UsuarioNotificar.NotificacaoDoacaoVaquinhaBrowser == true)
            {
                Notificacao notificacao = new Notificacao(this.Usuario.Nome + " doou para a vaquinha: " + this.Anuncio.Titulo, this.UsuarioNotificar, "#!/anuncio/" + this.Anuncio.Id);
                _notificacaoRepositorio.CriarNotificacao(notificacao);
            }

        }

    }
}
