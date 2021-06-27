using System;
using DIO.Series.Repository;
using DIO.Series.Enums;
using DIO.Series.Entity;

namespace DIO.Series
{
    class Program
    {
        static SerieRepository repository = new SerieRepository();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

			while (opcaoUsuario.ToUpper() != "X")
			{
				switch (opcaoUsuario)
				{
					case "1":
						ListarSeries();
						break;
					case "2":
						InserirSerie();
						break;
					case "3":
						AtualizarSerie();
						break;
					case "4":
						ExcluirSerie();
						break;
					case "5":
						VisualizarSerie();
						break;
					case "C":
						Console.Clear();
						break;

					default:
						throw new ArgumentOutOfRangeException();
				}

				opcaoUsuario = ObterOpcaoUsuario();
			}

			Console.WriteLine("Obrigado por utilizar nossos serviços.");
			Console.ReadLine();
        }

        private static void VisualizarSerie()
        {
            Console.Write("Digite o id da série: ");
			int id = int.Parse(Console.ReadLine());

			var serie = repository.RetornaPorId(id);

			Console.WriteLine(serie);
        }

        private static void ExcluirSerie()
        {
            Console.Write("Digite o id da série: ");
			int id = int.Parse(Console.ReadLine());

            repository.Excluir(id);
        }

        private static void AtualizarSerie()
        {
            Console.Write("Digite o id da série: ");
			int id = int.Parse(Console.ReadLine());

            int entradaGenero, entradaAno;
            string entradaTitulo, entradaDescricao;
            CapturarDadosSerie(out entradaGenero, out entradaTitulo, out entradaAno, out entradaDescricao);

            Serie atualizaSerie = new Serie(id: id,
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

            if (!SerieValida(atualizaSerie))
                return;

			repository.Atualizar(id, atualizaSerie);

        }

        private static void InserirSerie()
        {
            Console.WriteLine("Inserir nova série");

            int entradaGenero, entradaAno;
            string entradaTitulo, entradaDescricao;
            CapturarDadosSerie(out entradaGenero, out entradaTitulo, out entradaAno, out entradaDescricao);

            Serie novaSerie = new Serie(id: repository.ProximoId(),
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            if (!SerieValida(novaSerie))
                return;

            repository.Inserir(novaSerie);
        }

        private static bool SerieValida(Serie serie)
        {
            if (!serie.IsValid())
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Clear(); 
                Console.WriteLine("******** FALHA NA OPERACAO ********");
                Console.WriteLine("********      Motivos      ********");
                Console.WriteLine("***********************************");
                foreach (var erro in serie.ObterErros())
                {
                    Console.WriteLine(erro);
                }
            }
            Console.ResetColor(); 
            return serie.IsValid();                
        }
        private static void CapturarDadosSerie(out int entradaGenero, out string entradaTitulo, out int entradaAno, out string entradaDescricao)
        {
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
            }

            Console.Write("Digite o gênero entre as opções acima: ");

            entradaGenero = int.Parse(Console.ReadLine());
            Console.Write("Digite o Título da Série: ");
            entradaTitulo = Console.ReadLine();
            Console.Write("Digite o Ano de Início da Série: ");
            entradaAno = int.Parse(Console.ReadLine());
            Console.Write("Digite a Descrição da Série: ");
            entradaDescricao = Console.ReadLine();
        }

        private static void ListarSeries()
        {
            Console.WriteLine("Listar séries");

			var lista = repository.Listar();

			if (lista.Count == 0)
			{
				Console.WriteLine("Nenhuma série cadastrada.");
				return;
			}

			foreach (var serie in lista)
			{
				Console.WriteLine("#ID {0}: - {1}", serie.Id, serie.Titulo);
			}
        }

        private static string ObterOpcaoUsuario()
		{
			Console.WriteLine();
			Console.WriteLine("DIO Séries a seu dispor!!!");
			Console.WriteLine("Informe a opção desejada:");

			Console.WriteLine("1- Listar séries");
			Console.WriteLine("2- Inserir nova série");
			Console.WriteLine("3- Atualizar série");
			Console.WriteLine("4- Excluir série");
			Console.WriteLine("5- Visualizar série");
			Console.WriteLine("C- Limpar Tela");
			Console.WriteLine("X- Sair");
			Console.WriteLine();

			string opcaoUsuario = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return opcaoUsuario;
		}
    }
}
