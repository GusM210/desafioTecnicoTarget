var reader = new LeitorEstoque();
var produtos = reader.Ler("estoque.json");

var service = new EstoqueService(produtos);
var menu = new Menu(service);

menu.Executar();
