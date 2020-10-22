using System.Collections.Generic;

namespace ConsultaCredito.Interfaces
{
    public interface IServicoConsultaCredito
    {
        IList<PendenciaFinanceira> ConsultarPendenciasPorCPF(string cpf);
    }
}
