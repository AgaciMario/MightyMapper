using Mighty.ExampleClasses;
using MightyMapper;

namespace Mighty
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TransacaoFinanceiraFrom from = new TransacaoFinanceiraFrom(
                idTransacao: Guid.NewGuid(), // Um novo GUID para o ID da transação
                descricao: "Compra de suprimentos de escritório para o mês de Junho",
                valor: 1523.75m, // Usando 'm' para indicar que é um decimal
                dataHora: new DateTime(2025, 06, 17, 10, 30, 0), // Data e hora da transação
                tipo: TransacaoFinanceiraFrom.TipoTransacao.Despesa, // Um valor do enum
                tags: new string[] { "escritório", "material", "infraestrutura", "mensal" }, // Array de strings
                participantes: new List<Participante> // Lista de Participantes
                {
                    new Participante("Papelaria XYZ", "12.345.678/0001-90"),
                    new Participante("João Silva", "123.456.789-01"),
                    new Participante("Maria Oliveira", "987.654.321-02")
                }
            );
            TransacaoFinanceiraTo to = null;

            MightyMapper<TransacaoFinanceiraFrom, TransacaoFinanceiraTo> mapper = new()
            {
                MightyMapperRuleList = [
                    new MightyMapperRule<TransacaoFinanceiraFrom, TransacaoFinanceiraTo>(
                        from => from.IdTransacao.ToString(),
                        To => To.CodigoIdentificador
                    ),

                    new MightyMapperRule<TransacaoFinanceiraFrom, TransacaoFinanceiraTo>(
                        from => from.Descricao,
                        To => To.DetalheTransacao
                    ),

                    new MightyMapperRule<TransacaoFinanceiraFrom, TransacaoFinanceiraTo>(
                        from => from.Valor,
                        To => To.Quantia
                    ),

                    new MightyMapperRule<TransacaoFinanceiraFrom, TransacaoFinanceiraTo>(
                        from => from.DataHora,
                        To => To.DataRegistro
                    ),

                    new MightyMapperRule<TransacaoFinanceiraFrom, TransacaoFinanceiraTo>(
                        from => from.Tipo,
                        To => To.Categoria
                    ),

                    new MightyMapperRule<TransacaoFinanceiraFrom, TransacaoFinanceiraTo>(
                        from => from.Tags.ToList(),
                        To => To.PalavrasChave
                    ),

                    new MightyMapperRule<TransacaoFinanceiraFrom, TransacaoFinanceiraTo>(
                        from => from.Participantes,
                        To => To.Envolvidos,
                        DoNothing: true
                    ),
                ]
            };

            to = mapper.Map(from);

            MightyMapper<Participante, Contato> subMapper = new()
            {
                MightyMapperRuleList = [
                    new MightyMapperRule<Participante, Contato>(
                        from => from.Nome,
                        To => To.NomeCompleto
                    ),
                    new MightyMapperRule<Participante, Contato>(
                        from => from.Documento,
                        To => To.IdentificacaoFiscal
                    )
                ]
            };

            Contato[] envolvidos = [];
            foreach (Participante participante in from.Participantes)
            {
                Contato contato = subMapper.Map(participante);
                envolvidos = envolvidos.Append(contato).ToArray();
            }

            to.Envolvidos = envolvidos;
        }
    }
}
