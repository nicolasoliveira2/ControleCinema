using ControleCinema.ConsoleApp.Compartilhado;
using ControleCinema.ConsoleApp.ModuloGenero;
using ControleCinema.ConsoleApp.ModuloSessao;
using System;
using System.Collections.Generic;

namespace ControleCinema.ConsoleApp.ModuloSessao

{
    public class TelaCadastroSessao : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Sessao> _repositorioSessao;
        private readonly Notificador _notificador;

        public TelaCadastroSessao(IRepositorio<Sessao> repositorioSessao, Notificador notificador)
            : base("Cadastro de Sessao")
        {
            _repositorioSessao = repositorioSessao;
            _notificador = notificador;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Sessao");

            Sessao novaSessao = ObterSessao();

            _repositorioSessao.Inserir(novaSessao);

            _notificador.ApresentarMensagem("Sala cadastrada com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Sessao");

            bool temRegistrosCadastrados = VisualizarRegistros("Pesquisando");

            if (temRegistrosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhuma sessao cadastrada para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroGenero = ObterNumeroRegistro();

            Sessao generoAtualizado = ObterSessao();

            bool conseguiuEditar = _repositorioSessao.Editar(numeroGenero, generoAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Sessao editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Sessao");

            bool temFuncionariosRegistrados = VisualizarRegistros("Pesquisando");

            if (temFuncionariosRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhuma sessao cadastrada para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroGenero = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioSessao.Excluir(numeroGenero);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Sessao excluída com sucesso1", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Sessao");

            List<Sessao> generos = _repositorioSessao.SelecionarTodos();

            if (generos.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhuma Sessao disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Sessao genero in generos)
                Console.WriteLine(genero.ToString());

            Console.ReadLine();

            return true;
        }

        private Sessao ObterSessao()
        {
            
            Console.Write("Digite o filme desejado: ");
            Console.WriteLine("Digite o genero: ");
            string descricao = Console.ReadLine();
            Console.WriteLine("Digite o horario da sessão");
            int horario = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Digite a duração da sessão");
            int duracao = Convert.ToInt32(Console.ReadLine());

            return new Sessao(descricao);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID da sessao que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioSessao.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID da sessao não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
