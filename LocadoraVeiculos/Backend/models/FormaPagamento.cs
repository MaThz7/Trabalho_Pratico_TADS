using System.ComponentModel.DataAnnotations;

namespace Backend.models;

public class FormaPagamento
{
    public int Id { get; set; }

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    public string Descricao { get; set; }

    public List<Aluguel>? Alugueis { get; set; }
}