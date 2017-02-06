using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDrive.Models;
using TestDrive.ViewModels;
using Xamarin.Forms;

namespace TestDrive.Views
{
    public partial class AgendamentoView : ContentPage
    {
        public AgendamentoViewModel ViewModel { get; set; }

        public AgendamentoView(Veiculo veiculo, Usuario usuario)
        {
            InitializeComponent();
            this.ViewModel = new AgendamentoViewModel(veiculo, usuario);
            this.BindingContext = this.ViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AssinarMensagens();
        }

        private void AssinarMensagens()
        {
            MessagingCenter.Subscribe<Agendamento>(this, "Agendamento",
                async (msg) =>
                {
                    var confirma = await DisplayAlert("Salvar Agendamento",
                    "Deseja mesmo enviar o agendamento?",
                    "sim", "não");

                    if (confirma)
                    {
                        this.ViewModel.SalvarAgendamento();
                    }
                });

            MessagingCenter.Subscribe<Agendamento>(this, "SucessoAgendamento",
                async (msg) =>
                {
                    await DisplayAlert("Agendamento", "Agendamento salvo com sucesso!", "ok");
                    await Navigation.PopToRootAsync();
                });

            MessagingCenter.Subscribe<ArgumentException>(this, "FalhaAgendamento",
                async (msg) =>
                {
                    await DisplayAlert("Agendamento", "Falha ao agendar o test drive! Verifique os dados e tente novamente mais tarde!", "ok");
                    await Navigation.PopToRootAsync();
                });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Agendamento>(this, "Agendamento");

            MessagingCenter.Unsubscribe<Agendamento>(this, "SucessoAgendamento");
            MessagingCenter.Unsubscribe<ArgumentException>(this, "FalhaAgendamento");
        }
    }
}
