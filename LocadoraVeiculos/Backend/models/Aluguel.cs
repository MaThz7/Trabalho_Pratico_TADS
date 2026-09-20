public class Aluguel
{
    public int Id { get; set; }

    public int ClienteId { get; set; }

    public int VeiculoId { get; set; }

    public int FormaPagamentoId { get; set; }

    public DateTime DataInicio { get; set; }

    public DateTime DataFim { get; set; }

    public DateTime? DataDevolucao { get; set; }

    public double QuilometragemInicial { get; set; }

    public double? QuilometragemFinal { get; set; }

    public decimal ValorDiaria { get; set; }

    public decimal ValorTotal { get; set; }

    public Cliente Cliente { get; set; }

    public Veiculo Veiculo { get; set; }

    public FormaPagamento FormaPagamento { get; set; }
}