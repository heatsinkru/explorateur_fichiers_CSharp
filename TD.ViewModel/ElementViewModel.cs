using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TD
{
    public abstract class ElementViewModel : ViewModelBase
    {
        public abstract long Taille { get; }

        public abstract DateTime Date { get; }
    }

    public class FichierViewModel : ElementViewModel
    {
        private FileInfo _f;

        public FichierViewModel(FileInfo f)
        {
            _f = f;
        }

        // La liaison de données XAML ne reconnait que les propriétés.

        public string Nom 
        { 
            get { return _f.Name; } 
        }

        // Implémentation de la propriété Taille.
        public override long Taille
        {
            get { return _f.Length; }
        }

        // Implémentation de la propriété Date.
        public override DateTime Date
        {
            get { return _f.LastWriteTime; }
        }
    }

    public class DossierViewModel : ElementViewModel
    {
        private DirectoryInfo _d;

        public DossierViewModel(DirectoryInfo d)
        {
            _d = d;
        }

        public DirectoryInfo Dossier
        {
            get { return _d; }
        }

        // La liaison de données XAML ne reconnait que les propriétés.

        public string Nom
        {
            get { return _d.Name; }
        }

        // Implémentation de la propriété Taille.
        public override long Taille
        {
            get { return 0L; /* On ramène la taille d'un dossier à 0 octet. */ }
        }

        // Implémentation de la propriété Date.
        public override DateTime Date
        {
            get { return _d.LastWriteTime; }
        }
    }
}
