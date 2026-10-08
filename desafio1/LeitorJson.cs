using System.Text.Json;

public class VendasReader
{
    public List<Venda> Ler(string caminho)
    {
        if (!File.Exists(caminho))
            throw new FileNotFoundException($"Arquivo não encontrado: {caminho}");

        string json = File.ReadAllText(caminho);

        var wrapper = JsonSerializer.Deserialize<VendasWrapper>(json);

        return wrapper?.Vendas ?? new List<Venda>();
    }
}