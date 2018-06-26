using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servico;
using App01_ConsultarCEP.Servico.Modelo;

namespace App01_ConsultarCEP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BtnBuscarCep.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object sender, EventArgs args)
        {
            string cep = EntryCEP.Text.Trim();

            if (ValidarCep(cep))
            {

                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCep(cep);

                    if (end != null)
                    {
                        LblResultado.Text = string.Format("Endereço: {2}, {3}, {0}, {1}", end.Localidade, end.Uf, end.Logradouro, end.Bairro);
                    }
                    else
                    {
                        DisplayAlert("ERRO", "Endereço não encontrado para o CEP informado: " + cep, "OK");
                    }
                }
                catch (Exception ex)
                {
                    DisplayAlert("ERRO CRÍTICO: ", ex.Message, "OK");
                }
               
            }
        }

        private bool ValidarCep(string cep)
        {
            bool valido = true;

            if (cep.Length != 8)
            {
                //ERRO
                DisplayAlert("ERRO", "CEP Inválido. O CEP deve conter 8 carácteres.", "OK");

                valido = false;
            }

            int novoCep = 0;

            if (!int.TryParse(cep, out novoCep))
            {
                //ERRO
                DisplayAlert("ERRO", "CEP Inválido. O CEP deve conter apenas números", "OK");

                valido = false;
            }

            return valido;
        }
    }
}
