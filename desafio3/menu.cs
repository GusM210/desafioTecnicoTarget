using System.Globalization;

public class Menu
{
    private readonly CalculadoraJuros _calculadora;
    private readonly CultureInfo _cultura = new CultureInfo("pt-BR");

    public Menu(CalculadoraJuros calculadora)
    {
        _calculadora = calculadora;
    }

    public void Executar()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("===== CÁLCULO DE JUROS =====");
            Console.WriteLine("1 - Calcular juros de um valor");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");

            switch (Console.ReadLine())
            {
                case "1":
                    CalcularJuros();
                    break;
                case "0":
                    Console.WriteLine("Encerrando...");
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    private void CalcularJuros()
    {
        decimal valor = LerValor();
        DateTime vencimento = LerData();
        DateTime hoje = DateTime.Today;

        int dias = _calculadora.DiasEmAtraso(vencimento, hoje);
        decimal juros = _calculadora.CalcularJuros(valor, vencimento, hoje);

        Console.WriteLine();
        Console.WriteLine($"Data de hoje: {hoje:dd/MM/yyyy}");
        Console.WriteLine($"Dias em atraso: {dias}");
        Console.WriteLine($"Valor original: {valor.ToString("C", _cultura)}");
        Console.WriteLine($"Juros: {juros.ToString("C", _cultura)}");
        Console.WriteLine($"Total a pagar: {(valor + juros).ToString("C", _cultura)}");
    }

    private decimal LerValor()
    {
        while (true)
        {
            Console.Write("Informe o valor (ex: 1500,50): ");

            if (decimal.TryParse(Console.ReadLine(), NumberStyles.Number, _cultura, out decimal valor) && valor > 0)
                return valor;

            Console.WriteLine("Valor inválido. Tente novamente.");
        }
    }

    private DateTime LerData()
    {
        while (true)
        {
            Console.Write("Informe a data de vencimento (dd/MM/aaaa): ");

            if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", _cultura, DateTimeStyles.None, out DateTime data))
                return data;

            Console.WriteLine("Data inválida. Use o formato dd/MM/aaaa.");
        }
    }
}