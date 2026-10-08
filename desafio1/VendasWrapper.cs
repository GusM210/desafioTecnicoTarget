using System.Text.Json.Serialization;

public class VendasWrapper
{
    [JsonPropertyName("venda")]
    public List<Venda> Vendas { get; set; } = new();
}