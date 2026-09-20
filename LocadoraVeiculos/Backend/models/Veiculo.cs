public class Veiculo
{
    public int Id { get; set; }

    public string Modelo { get; set; }

    public int AnoFabricacao { get; set; }

    public double Quilometragem { get; set; }

    public int FabricanteId { get; set; }

    public int CategoriaId { get; set; }

    public Fabricante Fabricante { get; set; }

    public Categoria Categoria { get; set; }

    public ICollection<Aluguel> Alugueis { get; set; }
}