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

namespace TD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Définition de la vue-modèle par la fenêtre.
            // Par convention, nous allons plutôt la définir
            // au démarrage de l'application (classe App).
            //
            // DataContext = new MainViewModel();
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is MainViewModel m && _lb.SelectedItem is ElementViewModel elt)
            {
                m.Action(elt);
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is MainViewModel m)
            {
                m.Selectionne(_lb.SelectedItems.OfType<ElementViewModel>().ToArray());
            }
        }
    }
}
