public class EstoqueService
{
    private readonly List<Produto> _produtos;
    private readonly List<Movimentacao> _movimentacoes = new();
    private int _proximoId = 1;

    public EstoqueService(List<Produto> produtos)
    {
        _produtos = produtos;
    }

    public IReadOnlyList<Produto> Produtos => _produtos;
    public IReadOnlyList<Movimentacao> Movimentacoes => _movimentacoes;

    public Produto? BuscarProduto(int codigo) =>
        _produtos.FirstOrDefault(p => p.Codigo == codigo);

    public Movimentacao Movimentar(int codigo, TipoMovimentacao tipo, int quantidade)
    {
        if (quantidade <= 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.");

        var produto = BuscarProduto(codigo)
            ?? throw new ArgumentException($"Produto {codigo} não encontrado.");

        if (tipo == TipoMovimentacao.Saida && quantidade > produto.Estoque)
            throw new InvalidOperationException(
                $"Estoque insuficiente. Disponível: {produto.Estoque}.");

        produto.Estoque += tipo == TipoMovimentacao.Entrada ? quantidade : -quantidade;

        var movimentacao = new Movimentacao
        {
            Id = _proximoId++,
            Tipo = tipo,
            Descricao = tipo == TipoMovimentacao.Entrada
                ? "Entrada de mercadoria no depósito"
                : "Saída de mercadoria do depósito",
            CodigoProduto = codigo,
            Quantidade = quantidade,
            DataHora = DateTime.Now,
            EstoqueFinal = produto.Estoque
        };

        _movimentacoes.Add(movimentacao);
        return movimentacao;
    }
}