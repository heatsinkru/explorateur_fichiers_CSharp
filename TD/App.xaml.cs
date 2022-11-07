using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TD
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Démarrage de l'application
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Se substitue à 
            // StartupUri="MainWindow.xaml" dans le fichier XAML.

            // Affiche la fenêtre principale, avec sa vue-modèle.
            MainWindow w = new MainWindow();
            w.DataContext = new MainViewModel();
            w.Show();
        }
    }
}
