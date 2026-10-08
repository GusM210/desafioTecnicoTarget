using System.Text.Json.Serialization;

public class EstoqueWrapper
{
    [JsonPropertyName("estoque")]
    public List<Produto> Produtos { get; set; } = new();
}