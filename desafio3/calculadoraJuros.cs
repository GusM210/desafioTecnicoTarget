public class CalculadoraJuros
{
    private const decimal TaxaDiaria = 0.025m; // 2.5% per day

    public int DiasEmAtraso(DateTime vencimento, DateTime hoje)
    {
        int dias = (hoje.Date - vencimento.Date).Days;
        return Math.Max(0, dias); // not overdue = 0 days
    }

    public decimal CalcularJuros(decimal valor, DateTime vencimento, DateTime hoje)
    {
        int dias = DiasEmAtraso(vencimento, hoje);
        return valor * TaxaDiaria * dias;
    }
}