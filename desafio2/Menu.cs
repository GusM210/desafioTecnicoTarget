public class Menu
{
    private readonly EstoqueService _service;

    public Menu(EstoqueService service)
    {
        _service = service;
    }

    public void Executar()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("===== CONTROLE DE ESTOQUE =====");
            Console.WriteLine("1 - Entrada de mercadoria");
            Console.WriteLine("2 - Saída de mercadoria");
            Console.WriteLine("3 - Ver estoque atual");
            Console.WriteLine("4 - Histórico de movimentações");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");

            switch (Console.ReadLine())
            {
                case "1": LancarMovimentacao(TipoMovimentacao.Entrada); break;
                case "2": LancarMovimentacao(TipoMovimentacao.Saida); break;
                case "3": MostrarEstoque(); break;
                case "4": MostrarHistorico(); break;
                case "0":
                    Console.WriteLine("Encerrando...");
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    private void LancarMovimentacao(TipoMovimentacao tipo)
    {
        MostrarEstoque();

        Console.Write("Código do produto: ");
        if (!int.TryParse(Console.ReadLine(), out int codigo))
        {
            Console.WriteLine("Código inválido.");
            return;
        }

        Console.Write("Quantidade: ");
        if (!int.TryParse(Console.ReadLine(), out int quantidade))
        {
            Console.WriteLine("Quantidade inválida.");
            return;
        }

        try
        {
            var mov = _service.Movimentar(codigo, tipo, quantidade);
            var produto = _service.BuscarProduto(codigo)!;

            Console.WriteLine();
            Console.WriteLine($"Movimentação #{mov.Id} registrada: {mov.Descricao}");
            Console.WriteLine($"Produto: {produto.Descricao}");
            Console.WriteLine($"Quantidade movimentada: {mov.Quantidade}");
            Console.WriteLine($"Estoque final: {mov.EstoqueFinal}");
        }
        catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }

    private void MostrarEstoque()
    {
        Console.WriteLine();
        Console.WriteLine("--- Estoque atual ---");
        foreach (var p in _service.Produtos)
        {
            Console.WriteLine($"{p.Codigo} - {p.Descricao}: {p.Estoque}");
        }
        Console.WriteLine();
    }

    private void MostrarHistorico()
    {
        Console.WriteLine();
        Console.WriteLine("--- Histórico de movimentações ---");

        if (_service.Movimentacoes.Count == 0)
        {
            Console.WriteLine("Nenhuma movimentação lançada ainda.");
            return;
        }

        foreach (var m in _service.Movimentacoes)
        {
            Console.WriteLine(
                $"#{m.Id} | {m.DataHora:dd/MM/yyyy HH:mm} | {m.Descricao} | " +
                $"Produto {m.CodigoProduto} | Qtde: {m.Quantidade} | Estoque final: {m.EstoqueFinal}");
        }
    }
}