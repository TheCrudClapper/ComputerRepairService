using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
