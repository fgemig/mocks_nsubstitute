using ConsultaCredito.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using FluentAssertions;

namespace ConsultaCredito.Testes
{
    [TestFixture]
    public class AnaliseDeCreditoTestes
    {
        private IServicoConsultaCredito _servicoConsultaCredito;
        private List<PendenciaFinanceira> _listaDePendenciasFinanceiras;

        [SetUp]
        public void Setup()
        {
            _servicoConsultaCredito = Substitute.For<IServicoConsultaCredito>();
            _listaDePendenciasFinanceiras = new List<PendenciaFinanceira>();
        }

        [Test]
        public void DeveRetornarPendenciasFinanceirasParaOCpf()
        {
            var cpf = "12345678901";

            _listaDePendenciasFinanceiras.Add(
                new PendenciaFinanceira()
                {
                    CPF = cpf,
                    DataOcorrencia = new DateTime(2020, 07, 01),
                    Descricao = "Parcela não paga",
                    NomePessoa = "Pessoa",
                    NomeReclamante = "Reclamante",
                    Valor = 428.70M
                });

            var retornoAnaliseDeCredito = ConsultarAnaliseDeCredito(cpf);

            retornoAnaliseDeCredito.Should().Be(StatusConsultaCredito.Inadimplente);
        }

        [Test]
        public void NaoDeveRetornarPendenciasFinanceirasParaOCpf()
        {
            var cpf = "10987654321";

            var retornoAnaliseDeCredito = ConsultarAnaliseDeCredito(cpf);

            retornoAnaliseDeCredito.Should().Be(StatusConsultaCredito.SemPendencias);
        }

        [Test]
        public void DeveGerarErroDeComunicacaoQuandoAListaDePendenciasRetornarNula()
        {
            var cpf = "10987654321";

            _listaDePendenciasFinanceiras = null;

            var retornoAnaliseDeCredito = ConsultarAnaliseDeCredito(cpf);

            retornoAnaliseDeCredito.Should().Be(StatusConsultaCredito.ErroDeComunicacao);
        }

        private StatusConsultaCredito ConsultarAnaliseDeCredito(string cpf)
        {
            _servicoConsultaCredito
                .ConsultarPendenciasPorCPF(cpf)
                .Returns(_listaDePendenciasFinanceiras);

            var retornoAnaliseDeCredito = new AnaliseDeCredito(_servicoConsultaCredito)
                .ConsultarSituacaoCPF(cpf);

            return retornoAnaliseDeCredito;
        }
    }
}