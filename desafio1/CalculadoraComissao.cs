public class CalculadoraComissao
{
    public decimal Calcular(decimal valor)
    {
        if (valor < 100m)
            return 0;              // no commission

        if (valor < 500m)
            return valor * 0.01m;   // 1%

        return valor * 0.05m;       // 5%
    }
}