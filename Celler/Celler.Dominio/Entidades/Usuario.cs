using System;
using System.Collections.Generic;
using Celler.Dominio.Models;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Dominio.Entidades
{
    public class Usuario : EntidadeBasica
    {
        static readonly char[] _caracteresNovaSenha = "abcdefghijklmnopqrstuvzwyz1234567890*-_".ToCharArray();
        static readonly int _numeroCaracteresNovaSenha = 10;

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public List<Permissao> Permissoes { get; private set; }
        public bool NotificacaoComentarioAnuncioEmail { get; private set; }
        public bool NotificacaoComentarioAnuncioSlack { get; private set; }
        public bool NotificacaoComentarioAnuncioBrowser { get; private set; }
        public bool NotificacaoPresencaEmail { get; private set; }
        public bool NotificacaoPresencaSlack { get; private set; }
        public bool NotificacaoPresencaBrowser { get; private set; }
        public bool NotificacaoInteresseEmail { get; private set; }
        public bool NotificacaoInteresseSlack { get; private set; }
        public bool NotificacaoInteresseBrowser { get; private set; }
        public bool NotificacaoDoacaoVaquinhaEmail { get; private set; }
        public bool NotificacaoDoacaoVaquinhaSlack { get; private set; }
        public bool NotificacaoDoacaoVaquinhaBrowser { get; private set; }
        public string CanalSlack { get; private set; }

        protected Usuario()
        {
        }

        public Usuario(string nome, string email, string senha)
        {
            Nome = nome;
            Email = email;
            Id = 0;
            if (!string.IsNullOrWhiteSpace(senha))
                Senha = CriptografarSenha(senha);
            Permissoes = new List<Permissao>();
            AtribuirPermissoes("Colaborador");

            if (string.IsNullOrWhiteSpace(Nome))
                AdicionarMensagem("Nome é inválido.");

            if (string.IsNullOrWhiteSpace(Email))
                AdicionarMensagem("Email é inválido.");

            if (string.IsNullOrWhiteSpace(Senha))
                AdicionarMensagem("Senha é inválido.");
        }

        public void SetarConfiguracoes(bool notificacaoComentarioAnuncioEmail,
                                       bool notificacaoComentarioAnuncioSlack,
                                       bool notificacaoComentarioAnuncioBrowser,
                                       bool notificacaoPresencaEmail,
                                       bool notificacaoPresencaSlack,
                                       bool notificacaoPresencaBrowser,
                                       bool notificacaoInteresseEmail,
                                       bool notificacaoInteresseSlack,
                                       bool notificacaoInteresseBrowser,
                                       bool notificacaoDoacaoVaquinhaEmail,
                                       bool notificacaoDoacaoVaquinhaSlack,
                                       bool notificacaoDoacaoVaquinhaBrowser,
                                       string canalSlack)
        {
            NotificacaoComentarioAnuncioEmail = notificacaoComentarioAnuncioEmail;
            NotificacaoComentarioAnuncioSlack = notificacaoComentarioAnuncioSlack;
            NotificacaoComentarioAnuncioBrowser = notificacaoComentarioAnuncioBrowser;
            NotificacaoPresencaEmail = notificacaoPresencaEmail;
            NotificacaoPresencaSlack = notificacaoPresencaSlack;
            NotificacaoPresencaBrowser = notificacaoPresencaBrowser;
            NotificacaoInteresseEmail = notificacaoInteresseEmail;
            NotificacaoInteresseSlack = notificacaoInteresseSlack;
            NotificacaoInteresseBrowser = notificacaoInteresseBrowser;
            NotificacaoDoacaoVaquinhaEmail = notificacaoDoacaoVaquinhaEmail;
            NotificacaoDoacaoVaquinhaSlack = notificacaoDoacaoVaquinhaSlack;
            NotificacaoDoacaoVaquinhaBrowser = notificacaoDoacaoVaquinhaBrowser;

            if (string.IsNullOrEmpty(canalSlack) && (notificacaoComentarioAnuncioSlack ||
                                                     notificacaoPresencaSlack ||
                                                     notificacaoInteresseSlack ||
                                                     notificacaoDoacaoVaquinhaSlack))
                AdicionarMensagem("O canal do slack é nulo!");

            
            CanalSlack = canalSlack;
        }

        public string ResetarSenha()
        {
            var senha = string.Empty;
            for (int i = 0; i < _numeroCaracteresNovaSenha; i++)
            {
                senha += new Random().Next(0, _caracteresNovaSenha.Length);
            }

            Senha = CriptografarSenha(senha);

            return senha;
        }

        private string CriptografarSenha(string senha)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.Default.GetBytes(Email + senha);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("x2"));

            return sb.ToString();
        }

        public bool ValidarSenha(string senha)
        {
            return CriptografarSenha(senha) == Senha;
        }

        public void AtribuirPermissoes(params string[] nomes)
        {
            foreach (var nome in nomes)
                Permissoes.Add(new Permissao(nome));
        }

        public static string ObterEmail(string basicAuth)
        {
            if (basicAuth == null && !basicAuth.StartsWith("Basic"))
            { 
                throw new Exception("A string de autorização é inválida.");
            }
            Encoding encoding = Encoding.GetEncoding("iso-8859-1");

            string encodedUsernamePassword = basicAuth.Substring("Basic ".Length).Trim();
            string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));
            int seperatorIndex = usernamePassword.IndexOf(':');
            string username = usernamePassword.Substring(0, seperatorIndex);

            return username;
        }

        public override bool Equals(object obj)
        {
            Usuario outro = (Usuario)obj;
            return outro.Id == this.Id && outro.Email == this.Email;
        }
    }
}
