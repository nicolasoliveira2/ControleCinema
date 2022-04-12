using ControleCinema.ConsoleApp.Compartilhado;
using ControleCinema.ConsoleApp.ModuloFuncionario;
using ControleCinema.ConsoleApp.ModuloSala;
using System;
using System.Collections.Generic;

namespace ControleCinema.ConsoleApp.ModuloSala

{
    public class TelaCadastroSala : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Sala> _repositorioSala;
        private readonly Notificador _notificador;
        private IRepositorio<Funcionario> repositorioSala;
        private Notificador notificador;

        public TelaCadastroSala(IRepositorio<Sala> repositorioSala, Notificador notificador)
            : base("Cadastro de Salas")
        {
            _repositorioSala = repositorioSala;
            _notificador = notificador;
        }

        public TelaCadastroSala(IRepositorio<Funcionario> repositorioSala, Notificador notificador) : base("Cadastro de Salas")
        {
            this.repositorioSala = repositorioSala;
            this.notificador = notificador;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Salas");

            Sala novoGenero = ObterSala();

            _repositorioSala.Inserir(novoGenero);

            _notificador.ApresentarMensagem("Sala cadastrada com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Sala");

            bool temRegistrosCadastrados = VisualizarRegistros("Pesquisando");

            if (temRegistrosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhuma sala cadastrada para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroGenero = ObterNumeroRegistro();

            Sala generoAtualizado = ObterSala();

            bool conseguiuEditar = _repositorioSala.Editar(numeroGenero, generoAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Sala editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Sala");

            bool temFuncionariosRegistrados = VisualizarRegistros("Pesquisando");

            if (temFuncionariosRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhuma sala cadastrada para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroGenero = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioSala.Excluir(numeroGenero);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Sala excluída com sucesso1", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Salas");

            List<Sala> generos = _repositorioSala.SelecionarTodos();

            if (generos.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhuma sala disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Sala genero in generos)
                Console.WriteLine(genero.ToString());

            Console.ReadLine();

            return true;
        }

        private Sala ObterSala()
        {
            Console.Write("Digite a descrição do sala: ");
            string descricao = Console.ReadLine();

            return new Sala(descricao);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID da sala que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioSala.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID da sala não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
