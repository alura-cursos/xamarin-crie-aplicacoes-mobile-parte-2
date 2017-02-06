using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDrive.Models;
using TestDrive.Services;
using TestDrive.ViewModels;
using Xamarin.Forms;

namespace TestDrive.Views
{
    public partial class AgendamentosUsuarioView : ContentPage
    {
        readonly AgendamentosUsuarioViewModel _viewModel;
        public AgendamentosUsuarioView()
        {
            InitializeComponent();
            this._viewModel = new AgendamentosUsuarioViewModel();
            this.BindingContext = this._viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<Agendamento>(this, "AgendamentoSelecionado",
                async (agendamento) =>
                {
                    if (!agendamento.Confirmado)
                    {
                        var reenviar = await DisplayAlert("Reenviar", "Deseja reenviar o agendamento?", "sim", "não");
                        if (reenviar)
                        {
                            AgendamentoService agendamentoService = new AgendamentoService();
                            await agendamentoService.EnviarAgendamento(agendamento);
                            this._viewModel.AtualizarLista();
                        }
                    }
                });

            MessagingCenter.Subscribe<Agendamento>(this, "SucessoAgendamento",
                async (agendamento) =>
                {
                    await DisplayAlert("Reenviar", "Reenvio com sucesso!", "ok");
                });

            MessagingCenter.Subscribe<Agendamento>(this, "FalhaAgendamento",
                async (agendamento) =>
                {
                    await DisplayAlert("Reenviar", "Falha ao reenviar!", "ok");
                });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<Agendamento>(this, "AgendamentoSelecionado");
            MessagingCenter.Unsubscribe<Agendamento>(this, "SucessoAgendamento");
            MessagingCenter.Unsubscribe<Agendamento>(this, "FalhaAgendamento");
        }
    }
}
