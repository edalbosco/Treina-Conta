using System;
using Xamarin.Forms;

//http://csharphelper.com/blog/2016/04/convert-to-and-from-roman-numerals-in-c/
namespace Treina_Contas.ViewModels
{
    public class RomanoViewModel : BaseViewModel
    {
        private int _maximo;
        private int _numero;
        private string _romano;
        private string _resposta;
        private int _certas;
        private int _erradas;
        private Random random = new Random();


        // Map digits to letters.
        private string[] ThouLetters = { "", "M", "MM", "MMM" };
        private string[] HundLetters =
            { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" };
        private string[] TensLetters =
            { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" };
        private string[] OnesLetters =
            { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" };

        public int Maximo { get => _maximo; set => SetProperty(ref _maximo, value); }
        public int Numero { get => _numero; set => SetProperty(ref _numero, value); }
        public string Romano { get => _romano; set => SetProperty(ref _romano, value); }
        public string Resposta { get => _resposta; set => SetProperty(ref _resposta, value); }
        public int Certas { get => _certas; set => SetProperty(ref _certas, value); }
        public int Erradas { get => _erradas; set => SetProperty(ref _erradas, value); }

        public Command AnswerParaArabicoCommand => new Command(() => RespondeParaArabico());
        public Command AnswerParaRomanoCommand => new Command(() => RespondeParaRomano());

        public event EventHandler SetFocus;
        public event EventHandler<string> ShowAlert;

        public RomanoViewModel()
        {
            Maximo = 1999;
            Certas = 0;
            Erradas = 0;
            Romano = "";
            Resposta = "";

            CriaConta();
        }

        private void CriaConta()
        {
            try
            {
                Numero = random.Next(1, Maximo);
                Romano = ArabicToRoman(Numero);

                Resposta = "";

                SetFocus?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                ShowAlert?.Invoke(this, $"Deu Erro: {ex.ToString()}");
            }
        }

        // Convert Roman numerals to an integer.
        private string ArabicToRoman(int arabic)
        {
            // See if it's >= 4000.
            if (arabic >= 4000)
            {
                // Use parentheses.
                int thou = arabic / 1000;
                arabic %= 1000;
                return "(" + ArabicToRoman(thou) + ")" +
                    ArabicToRoman(arabic);
            }

            // Otherwise process the letters.
            string result = "";

            // Pull out thousands.
            int num;
            num = arabic / 1000;
            result += ThouLetters[num];
            arabic %= 1000;

            // Handle hundreds.
            num = arabic / 100;
            result += HundLetters[num];
            arabic %= 100;

            // Handle tens.
            num = arabic / 10;
            result += TensLetters[num];
            arabic %= 10;

            // Handle ones.
            result += OnesLetters[arabic];

            return result;
        }

        private void RespondeParaRomano()
        {
            try
            {
                if (Resposta.ToUpper() == Romano)
                    Certas++;
                else
                    Erradas++;
            }
            catch (Exception ex)
            {
                ShowAlert?.Invoke(this, "Não faz besteira, guri!");
            }

            CriaConta();
        }

        private void RespondeParaArabico()
        {
            try
            {
                if (int.Parse("0" + (Resposta ?? "")) == Numero)
                    Certas++;
                else
                    Erradas++;
            }
            catch (Exception ex)
            {
                ShowAlert?.Invoke(this, "Não faz besteira, guri!");
            }

            CriaConta();
        }
    }
}