namespace PizzariaBuilderMvc.Models
{
    // Aqui temos a classe 'Pizza', que é o nosso Produto final.
    //  Reparem que ela tem várias propriedades opcionais. Sem o padrão Builder,
    //  teríamos que criar vários construtores gigantes ou passar vários 'null'
    //  e 'false' toda vez que fôssemos instanciar uma pizza simples.
    public class Pizza
    {
        public string Tamanho { get; set; } = "Média";
        public string TipoMassa { get; set; } = "Tradicional";
        public bool BordaRecheada { get; set; }
        public List<string> Ingredientes { get; } = new();

        public override string ToString()
        {
            var recheio = Ingredientes.Count > 0 
                ? string.Join(", ", Ingredientes) 
                : "Apenas queijo e molho";

            return $"Pizza {Tamanho} | Massa: {TipoMassa} | " +
                   $"Borda Recheada: {(BordaRecheada ? "Sim" : "Não")} | " +
                   $"Ingredientes: [{recheio}]";
        }
    }

    // Esta é a classe principal do padrão: o ConstrutorDePizza.
    // O segredo do Builder está em dois pontos:
    // 1. Ele guarda uma instância privada da Pizza enquanto ela é montada.
    // 2. Todo método de configuração retorna 'this' (o próprio construtor),
    // o que nos permite encadear os métodos um atrás do outro (Interface Fluente).

    public class ConstrutorDePizza
    {
        private Pizza _pizza = new();

        public ConstrutorDePizza DefinirTamanho(string tamanho)
        {
            _pizza.Tamanho = tamanho;
            return this; // <-- Aponte aqui na apresentação: "É isso que permite o encadeamento!"
        }

        public ConstrutorDePizza DefinirMassa(string massa)
        {
            _pizza.TipoMassa = massa;
            return this;
        }

        public ConstrutorDePizza ComBordaRecheada()
        {
            _pizza.BordaRecheada = true;
            return this;
        }

        public ConstrutorDePizza AdicionarIngrediente(string ingrediente)
        {
            _pizza.Ingredientes.Add(ingrediente);
            return this;
        }


        // Quando terminamos de configurar, chamamos o Construir(). Ele entrega
        //  a pizza pronta e já reseta o objeto interno para a próxima montagem.
        public Pizza Construir()
        {
            Pizza pizzaPronta = _pizza;
            _pizza = new Pizza(); // Limpa o estado para não misturar com o próximo pedido
            return pizzaPronta;
        }
    }


    // "Por fim, temos o PizzaioloDiretor. Ele não é obrigatório no padrão,
    //  mas serve para guardar receitas prontas do cardápio. Assim, o Controller
    //  não precisa repetir os mesmos passos toda vez que alguém pedir uma Calabresa."

    public class PizzaioloDiretor
    {
        public Pizza PrepararCalabresaPadrao(ConstrutorDePizza construtor)
        {
            return construtor
                .DefinirTamanho("Grande")
                .DefinirMassa("Fina")
                .ComBordaRecheada()
                .AdicionarIngrediente("Calabresa")
                .AdicionarIngrediente("Cebola")
                .AdicionarIngrediente("Orégano")
                .Construir();
        }
    }
}