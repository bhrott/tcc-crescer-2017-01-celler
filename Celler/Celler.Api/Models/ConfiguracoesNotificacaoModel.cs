using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Celler.Api.Models
{
    public class ConfiguracoesNotificacaoModel
    {
        public bool NotificacaoComentarioAnuncioEmail { get; set; }
        public bool NotificacaoComentarioAnuncioSlack { get; set; }
        public bool NotificacaoComentarioAnuncioBrowser { get; set; }
        public bool NotificacaoPresencaEmail { get; set; }
        public bool NotificacaoPresencaSlack { get; set; }
        public bool NotificacaoPresencaBrowser { get; set; }
        public bool NotificacaoInteresseEmail { get; set; }
        public bool NotificacaoInteresseSlack { get; set; }
        public bool NotificacaoInteresseBrowser { get; set; }
        public bool NotificacaoDoacaoVaquinhaEmail { get; set; }
        public bool NotificacaoDoacaoVaquinhaSlack { get; set; }
        public bool NotificacaoDoacaoVaquinhaBrowser { get; set; }
        public string CanalSlack { get; set; }
    }
}