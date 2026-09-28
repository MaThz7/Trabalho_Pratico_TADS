using System.ComponentModel.DataAnnotations;

namespace Backend.models;

public class Categoria
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Nome { get; set; }

    public List<Veiculo>? Veiculos { get; set; }
}