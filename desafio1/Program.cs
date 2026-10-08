var reader = new VendasReader();
var calculadora = new CalculadoraComissao();

var vendas = reader.Ler("vendas.json");

var menu = new Menu(vendas, calculadora);
menu.Executar();