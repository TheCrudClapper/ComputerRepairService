using System.Windows.Controls;
using System.Windows.Input;

namespace ComputerRepairService.Views.Single
{
    /// <summary>
    /// Logika interakcji dla klasy AddNewRepairView.xaml
    /// </summary>
    public partial class AddNewRepairView : UserControl
    {
        public AddNewRepairView()
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

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
