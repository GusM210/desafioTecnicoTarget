using System.Globalization;

public class Menu
{
    private readonly List<Venda> _vendas;
    private readonly List<string> _vendedores;
    private readonly CalculadoraComissao _calculadora;
    private readonly CultureInfo _cultura = new CultureInfo("pt-BR");

    public Menu(List<Venda> vendas, CalculadoraComissao calculadora)
    {
        _vendas = vendas;
        _calculadora = calculadora;

        
        _vendedores = vendas
            .Select(v => v.Vendedor)
            .Distinct()
            .ToList();
    }

    public void Executar()
    {
        while (true)
        {
            MostrarOpcoes();

            int opcao = LerOpcao();

            if (opcao == -1)
            {
                Console.WriteLine("Opção inválida. Tente novamente.");
                continue;
            }

            if (opcao == 0)
            {
                Console.WriteLine("Encerrando...");
                break;
            }

            MostrarComissoes(_vendedores[opcao - 1]);
        }
    }

    private void MostrarOpcoes()
    {
        Console.WriteLine();
        Console.WriteLine("===== MENU DE VENDEDORES =====");

        for (int i = 0; i < _vendedores.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {_vendedores[i]}");
        }

        Console.WriteLine("0 - Sair");
        Console.Write("Escolha uma opção: ");
    }

    
    private int LerOpcao()
    {
        string? entrada = Console.ReadLine();

        if (!int.TryParse(entrada, out int opcao) || opcao < 0 || opcao > _vendedores.Count)
            return -1;

        return opcao;
    }

    private void MostrarComissoes(string vendedor)
    {
        var vendasDoVendedor = _vendas.Where(v => v.Vendedor == vendedor).ToList();

        Console.WriteLine();
        Console.WriteLine($"--- Comissões de {vendedor} ---");

        decimal totalComissao = 0m;

        foreach (var venda in vendasDoVendedor)
        {
            decimal comissao = _calculadora.Calcular(venda.Valor);
            totalComissao += comissao;

            Console.WriteLine(
                $"Valor: {venda.Valor.ToString("C", _cultura)} | " +
                $"Comissão: {comissao.ToString("C", _cultura)}");
        }

        Console.WriteLine("-----------------------------");
        Console.WriteLine($"Total de vendas: {vendasDoVendedor.Count}");
        Console.WriteLine($"Total de comissão: {totalComissao.ToString("C", _cultura)}");
    }
}