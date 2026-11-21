using PizzariaBuilderMvc.Models;


// Aqui no Program.cs, inclusive, já vemos o Builder nativo da Microsoft
//  (WebApplication.CreateBuilder). Adicionamos uma verificação simples:
//  se passarmos '--terminal' ao rodar, abrimos o modo interativo no console
//  usando exatamente as mesmas classes Models do nosso projeto MVC.
if (args.Contains("--terminal"))
{
    ExecutarModoTerminalInterativo();
    return; // Encerra após o menu do terminal para não subir o servidor web
}

// Fluxo padrão do ASP.NET Core MVC (também usa o padrão Builder!)
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

static void ExecutarModoTerminalInterativo()
{
    Console.Clear();
    Console.WriteLine("====================================================");
    Console.WriteLine("   PIZZARIA BUILDER - DEMONSTRAÇÃO INTERATIVA       ");
    Console.WriteLine("====================================================\n");

    var construtor = new ConstrutorDePizza();
    var pizzaiolo = new PizzaioloDiretor();

    Console.WriteLine("Como deseja montar a pizza hoje?");
    Console.WriteLine("1 - Montar pizza personalizada (Passo a passo com o Builder)");
    Console.WriteLine("2 - Pedir Calabresa Padrão (Usando o Pizzaiolo Diretor)");
    Console.Write("\nEscolha uma opção (1 ou 2): ");
    var opcao = Console.ReadLine();

    Pizza pizzaFinal;

    if (opcao == "2")
    {
        // Demonstra o Diretor executando a receita pronta
        Console.WriteLine("\n[Executando: pizzaiolo.PrepararCalabresaPadrao(construtor)...]");
        pizzaFinal = pizzaiolo.PrepararCalabresaPadrao(construtor);
    }
    else
    {
        // Demonstra o Builder recebendo cada etapa conforme a escolha do usuário
        Console.Write("\n1º Passo - Qual o tamanho? (Pequena / Média / Grande / Família): ");
        var tamanho = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(tamanho))
        {
            construtor.DefinirTamanho(tamanho);
            Console.WriteLine($"   -> Chamado: construtor.DefinirTamanho(\"{tamanho}\")");
        }

        Console.Write("\n2º Passo - Qual o tipo de massa? (Tradicional / Fina / Pan): ");
        var massa = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(massa))
        {
            construtor.DefinirMassa(massa);
            Console.WriteLine($"   -> Chamado: construtor.DefinirMassa(\"{massa}\")");
        }

        Console.Write("\n3º Passo - Deseja borda recheada? (s/n): ");
        var borda = Console.ReadLine()?.Trim().ToLower();
        if (borda == "s")
        {
            construtor.ComBordaRecheada();
            Console.WriteLine("   -> Chamado: construtor.ComBordaRecheada()");
        }

        Console.WriteLine("\n4º Passo - Digite os ingredientes (pressione ENTER vazio para finalizar):");
        while (true)
        {
            Console.Write("   + Ingrediente: ");
            var ingrediente = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(ingrediente)) break;

            construtor.AdicionarIngrediente(ingrediente);
            Console.WriteLine($"   -> Chamado: construtor.AdicionarIngrediente(\"{ingrediente}\")");
        }

        // Passo final: entrega o produto pronto
        Console.WriteLine("\n5º Passo - Finalizando com construtor.Construir()...");
        pizzaFinal = construtor.Construir();
    }

    Console.WriteLine("\n====================================================");
    Console.WriteLine("PIZZA PRONTA ENTREGUE PELO BUILDER:");
    Console.WriteLine(pizzaFinal);
    Console.WriteLine("====================================================\n");
}