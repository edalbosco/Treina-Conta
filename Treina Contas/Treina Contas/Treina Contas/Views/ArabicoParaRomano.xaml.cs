using System;
using System.ComponentModel;
using Treina_Contas.ViewModels;
using Xamarin.Forms;

namespace Treina_Contas.Views
{
    [DesignTimeVisible(false)]
    public partial class ArabicoParaRomano : ContentPage
    {
        public ArabicoParaRomano()
        {
            InitializeComponent();

            var vm = new RomanoViewModel();

            vm.SetFocus += Vm_SetFocus;

            BindingContext = vm;
        }

        private void Vm_SetFocus(object sender, EventArgs e)
        {
            answer.Focus();
        }
    }
}