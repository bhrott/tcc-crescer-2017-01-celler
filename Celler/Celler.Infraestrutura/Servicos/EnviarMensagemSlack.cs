using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Infraestrutura.Servicos
{
    public class EnviarMensagemSlack
    {
        public EnviarMensagemSlack(string usuario, string texto)
        {
            MandarMensagemNoSlack(usuario, texto);
        }

        public static void MandarMensagemNoSlack(string usuario, string texto)
        {
            byte[] data = Convert.FromBase64String("eG94cC0xNjY2ODQ0ODY5NjAtMTY3NzMwNzA0NTEyLTIwOTAyNjQxNDgyMi03OWM3ZTc3ZTYwYzRkZjY3YmIzZWMzN2M1OTY1MDNiYg==");
            string token = Encoding.UTF8.GetString(data);
            StringBuilder enviar = new StringBuilder();
            enviar.Append("https://slack.com/api/chat.postMessage?token=");
            enviar.Append(token);
            enviar.Append("&channel=");
            enviar.Append(System.Uri.EscapeDataString(usuario));
            enviar.Append("&text=");
            enviar.Append(System.Uri.EscapeDataString(texto));
            enviar.Append("&pretty=1");
            String strenviar = enviar.ToString();
            WebRequest request = WebRequest.Create(strenviar);
            WebResponse response = request.GetResponse();
            Console.WriteLine(response);
            response.Close();
        }
    }
}
