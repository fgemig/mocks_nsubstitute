using System;

namespace ConsultaCredito
{
    public class PendenciaFinanceira
    {
        public string CPF { get; set; }

        public string NomePessoa { get; set; }

        public string NomeReclamante { get; set; }

        public string Descricao { get; set; }

        public DateTime DataOcorrencia { get; set; }

        public decimal Valor { get; set; }
    }
}
