using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Treina_Contas.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MultiplicaDezenasPage : ContentPage
    {
        public int Numerador { get; set; }
        public int Denominador { get; set; }
        public string Resultado { get; set; }
        public int Certas { get; set; }
        public int Erradas { get; set; }

        private Random random = new Random();

        public Command AnswerCommand => new Command(() => Responde());

        public MultiplicaDezenasPage()
        {
            InitializeComponent();

            Certas = 0;
            Erradas = 0;
            Resultado = "";

            CriaConta();

            BindingContext = this;
        }

        private void CriaConta()
        {
            Numerador = random.Next(10, 99); OnPropertyChanged(nameof(Numerador));
            Denominador = random.Next(10, 19); OnPropertyChanged(nameof(Denominador));
            answer.Focus();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Responde();
        }

        private void Responde()
        {

            if (int.Parse("0" + (Resultado ?? "")) == Numerador * Denominador)
            {
                Certas++; OnPropertyChanged(nameof(Certas));
            }
            else
            {
                Erradas++; OnPropertyChanged(nameof(Erradas));
            }

            Resultado = ""; OnPropertyChanged(nameof(Resultado));
            CriaConta();
        }
    }
}