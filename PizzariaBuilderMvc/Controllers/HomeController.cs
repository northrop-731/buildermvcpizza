using Microsoft.AspNetCore.Mvc;
using PizzariaBuilderMvc.Models;

namespace PizzariaBuilderMvc.Controllers
{
    public class HomeController : Controller
    {

        // No padrão MVC, o Controller atua como o Cliente do padrão Builder.
        // Ele recebe a requisição e usa o Builder para montar os objetos
        // de forma limpa, legível e sem precisar conhecer a lógica interna.
        public IActionResult Index()
        {
            var construtor = new ConstrutorDePizza();
            var pizzaiolo = new PizzaioloDiretor();

            // EXEMPLO 1: Montagem passo a passo (Pizza customizada pelo cliente)
            Pizza pizzaDoCliente = construtor
                .DefinirTamanho("Família")
                .ComBordaRecheada()
                .AdicionarIngrediente("Frango")
                .AdicionarIngrediente("Catupiry")
                .AdicionarIngrediente("Milho")
                .Construir();

            // EXEMPLO 2: Montagem via Diretor (Receita fixa do cardápio)
            Pizza pizzaDoCardapio = pizzaiolo.PrepararCalabresaPadrao(construtor);

            // EXEMPLO 3: Montagem mínima (Usa os valores padrão da classe Pizza)
            Pizza pizzaBasica = construtor
                .AdicionarIngrediente("Mussarela")
                .Construir();

            // Exibição limpa na tela para facilitar a demonstração ao vivo
            var saida = $"""
                === DEMONSTRAÇÃO DO PADRÃO BUILDER (PIZZARIA MVC) ===

                1. Pizza Customizada (Builder direto):
                   {pizzaDoCliente}

                2. Pizza do Cardápio (Via Pizzaiolo Diretor):
                   {pizzaDoCardapio}

                3. Pizza Básica (Apenas 1 passo, resto padrão):
                   {pizzaBasica}
                """;

            return Content(saida, "text/plain; charset=utf-8");
        }
    }
}