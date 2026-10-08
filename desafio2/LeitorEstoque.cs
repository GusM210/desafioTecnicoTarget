using System.Text.Json;

public class LeitorEstoque
{
    public List<Produto> Ler(string caminho)
    {
        if (!File.Exists(caminho))
            throw new FileNotFoundException($"Arquivo não encontrado: {caminho}");

        string json = File.ReadAllText(caminho);
        var wrapper = JsonSerializer.Deserialize<EstoqueWrapper>(json);

        return wrapper?.Produtos ?? new List<Produto>();
    }
}