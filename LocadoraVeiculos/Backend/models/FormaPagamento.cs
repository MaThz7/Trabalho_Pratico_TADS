public class FormaPagamento
{
    public int Id { get; set; }

    public string Descricao { get; set; }

    public ICollection<Aluguel> Alugueis { get; set; }
}