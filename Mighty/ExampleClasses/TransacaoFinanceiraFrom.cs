namespace Mighty.ExampleClasses
{
    internal class TransacaoFinanceiraFrom
    {
        public Guid IdTransacao { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataHora { get; set; }
        public TipoTransacao Tipo { get; set; }
        public string[] Tags { get; set; } // Array de string
        public List<Participante> Participantes { get; set; } // Lista de objetos

        // Construtor
        public TransacaoFinanceiraFrom(Guid idTransacao, string descricao, decimal valor,
                                       DateTime dataHora, TipoTransacao tipo,
                                       string[] tags, List<Participante> participantes)
        {
            IdTransacao = idTransacao;
            Descricao = descricao;
            Valor = valor;
            DataHora = dataHora;
            Tipo = tipo;
            Tags = tags;
            Participantes = participantes;
        }

        // Enum para Tipo de Transação
        public enum TipoTransacao
        {
            Receita,
            Despesa,
            Transferencia
        }
    }

    // Classe interna para Participante
    public class Participante
    {
        public string Nome { get; set; }
        public string Documento { get; set; } // CPF/CNPJ

        public Participante(string nome, string documento)
        {
            Nome = nome;
            Documento = documento;
        }
    }
}
