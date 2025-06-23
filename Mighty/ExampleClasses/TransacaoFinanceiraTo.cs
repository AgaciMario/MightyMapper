namespace Mighty.ExampleClasses
{
    public class TransacaoFinanceiraTo
    {
        public string CodigoIdentificador { get; set; } // Mapeado de IdTransacao
        public string DetalheTransacao { get; set; } // Mapeado de Descricao
        public decimal Quantia { get; set; } // Mapeado de Valor
        public DateTime DataRegistro { get; set; } // Mapeado da parte da data de DataHora
        public CategoriaTransacao Categoria { get; set; } // Mapeado de Tipo
        public List<string> PalavrasChave { get; set; } // Mapeado de Tags (array para lista)
        public Contato[] Envolvidos { get; set; } // Mapeado de Participantes (lista para array)

        // Construtor (opcional, pode ser usado para inicializar)
        public TransacaoFinanceiraTo()
        {
            PalavrasChave = new List<string>();
        }

        // Enum para Categoria de Transação
        public enum CategoriaTransacao
        {
            Entrada,
            Saida,
            Movimentacao
        }
    }

    // Classe interna para Contato
    public class Contato
    {
        public string NomeCompleto { get; set; }
        public string IdentificacaoFiscal { get; set; }
    }
}
