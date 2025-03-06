using System.Windows.Controls;
using System.Windows.Input;

namespace ComputerRepairService.Views.Single
{
    /// <summary>
    /// Logika interakcji dla klasy AddInvoiceView.xaml
    /// </summary>
    public partial class AddInvoiceView : UserControl
    {
        public AddInvoiceView()
        {
            InitializeComponent();
        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)

        {

            if (e.Key == Key.Enter)

            {

                ((Control)sender)?.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));

            }

        }
    }

}
