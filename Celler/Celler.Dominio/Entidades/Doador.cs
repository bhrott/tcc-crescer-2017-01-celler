namespace Celler.Dominio.Entidades
{
    public class Doador : EntidadeBasica
    {
        public int Id { get; private set; }
        public Usuario Usuario { get; set; }
        public double ValorDoado { get; private set; }
        //Status: 'p' - pago; 'n' - não pago
        public string Status { get; private set; }

        protected Doador(){}

        public static readonly string Erro_Valor_Doado_Zero = "O valor doado não pode ser 0.";
        public static readonly string Erro_Valor_Doado_Negativo = "O valor doado não pode ser negativo.";

        public Doador(Usuario usuario, double valorDoado)
        {
            this.Usuario = usuario;
            this.ValorDoado = valorDoado;
            this.Status = "n";

            if (this.ValorDoado == 0)
                Mensagens.Add(Erro_Valor_Doado_Zero);

            if (this.ValorDoado < 0)
                Mensagens.Add(Erro_Valor_Doado_Negativo);
        }

        public double AlterarStatusDoacao(Doador doador)
        {
            this.Status = "p";
            return this.ValorDoado;
        }
    }
}
