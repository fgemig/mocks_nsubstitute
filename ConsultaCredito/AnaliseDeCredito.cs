using ConsultaCredito.Interfaces;
using System;

namespace ConsultaCredito
{
    public class AnaliseDeCredito
    {
        private readonly IServicoConsultaCredito _servicoConsultaCredito;

        public AnaliseDeCredito(IServicoConsultaCredito servicoConsultaCredito)
        {
            _servicoConsultaCredito = servicoConsultaCredito;
        }

        public StatusConsultaCredito ConsultarSituacaoCPF(string cpf)
        {
            try
            {
                var pendencias = _servicoConsultaCredito.ConsultarPendenciasPorCPF(cpf);

                if (pendencias is null)
                    throw new Exception("Erro de comunicação");

                if (pendencias.Count > 0)
                    return StatusConsultaCredito.Inadimplente;

                return StatusConsultaCredito.SemPendencias;
            }
            catch(Exception ex)
            {
                return StatusConsultaCredito.ErroDeComunicacao;
            }           
        }
    }
}
