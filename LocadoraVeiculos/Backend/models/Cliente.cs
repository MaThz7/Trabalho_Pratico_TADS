using System.ComponentModel.DataAnnotations;

namespace Backend.models;

public class Cliente
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O CPF é obrigatório.")]
    public string CPF { get; set; }

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
    public string Email { get; set; }

    public List<Aluguel>? Alugueis { get; set; }
}