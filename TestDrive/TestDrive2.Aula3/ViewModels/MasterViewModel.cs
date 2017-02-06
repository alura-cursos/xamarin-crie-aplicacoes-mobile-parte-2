using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class MasterViewModel : BaseViewModel
    {
        public string Nome
        {
            get
            {
                return this.usuario.nome;
            }
            set
            {
                this.usuario.nome = value;
            }
        }

        public string Email
        {
            get { return this.usuario.email; }
            set
            {
                this.usuario.email = value;
            }
        }

        public string DataNascimento
        {
            get { return usuario.dataNascimento; }
            set { usuario.dataNascimento = value; }
        }

        public string Telefone
        {
            get { return usuario.telefone; }
            set { usuario.telefone = value; }
        }

        private bool editando;
        public bool Editando
        {
            get { return editando; }
            private set
            {
                editando = value;
                OnPropertyChanged(nameof(Editando));
            }
        }

        private readonly Usuario usuario;

        public ICommand EditarPerfilCommand { get; private set; }
        public ICommand EditarCommand { get; private set; }
        public ICommand SalvarCommand { get; private set; }

        public MasterViewModel(Usuario usuario)
        {
            this.usuario = usuario;

            EditarPerfilCommand = new Command(() =>
            {
                MessagingCenter.Send<Usuario>(usuario, "EditarPerfil");
            });

            EditarCommand = new Command(() =>
            {
                Editando = true;
            });

            SalvarCommand = new Command(() =>
            {
                Editando = false;
                MessagingCenter.Send<Usuario>(usuario, "SucessoSalvarUsuario");
            });
        }
    }
}
