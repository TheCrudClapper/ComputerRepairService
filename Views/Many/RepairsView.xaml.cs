using System.Windows.Controls;
using System.Windows.Input;

namespace ComputerRepairService.Views.Many
{
    /// <summary>
    /// Logika interakcji dla klasy RepairsView.xaml
    /// </summary>
    public partial class RepairsView : UserControl
    {
        public RepairsView()
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
