using System.ComponentModel.DataAnnotations;

namespace Backend.models;

public class Veiculo
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O modelo é obrigatório.")]
    public string Modelo { get; set; }

    [Range(1900, 2100, ErrorMessage = "O ano de fabricação é inválido.")]
    public int AnoFabricacao { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "A quilometragem não pode ser negativa.")]
    public double Quilometragem { get; set; }

    public int FabricanteId { get; set; }

    public int CategoriaId { get; set; }

    public Categoria? Categoria { get; set; }

    public Fabricante? Fabricante { get; set; }

    public List<Aluguel>? Alugueis { get; set; }
}