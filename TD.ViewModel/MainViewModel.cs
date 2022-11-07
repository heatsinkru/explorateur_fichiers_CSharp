using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TD
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ElementViewModel> _elements;
        private                              ElementViewModel[] _selection;
        private                                   DirectoryInfo _courant;

        public MainViewModel()
        {
            _courant   = new DirectoryInfo(Directory.GetCurrentDirectory());
            _selection = Array.Empty<ElementViewModel>();
            _elements  = new ObservableCollection<ElementViewModel>();

            Recharge();
        }

        private void Recharge()
        {
            _elements.Clear();

            try
            {
                foreach (DirectoryInfo d in _courant.EnumerateDirectories())
                {
                    _elements.Add(new DossierViewModel(d));
                }

                foreach (FileInfo f in _courant.EnumerateFiles())
                {
                    _elements.Add(new FichierViewModel(f));
                }
            }
            catch (UnauthorizedAccessException) // Le répertoire courant est interdit d'accès.
            {
                // La collection est laissée vide.
            }
        }

        // La liaison de données XAML ne reconnait que les propriétés.

        public string Courant 
        { 
            get { return _courant.FullName; } 
        }

        // public ObservableCollection<ElementViewModel> Elements
        // {
        //     get { return _elements; }
        // }
        //
        // Rend la collection non-modifiable par la vue :

        public ReadOnlyObservableCollection<ElementViewModel> Elements
        {
            get { return new ReadOnlyObservableCollection<ElementViewModel>(_elements); }
        }

        public ICommand RetourCommand
        {
            get { return new RelayCommand(Retour); }
        }

        public void Retour()
        {
            if (_courant.Parent != null)
            {
                _courant = _courant.Parent;
                OnPropertyChanged(nameof(Courant));

                Recharge();
            }
        }

        // Interaction sur tout élément de la liste.
        public void Action(ElementViewModel e)
        {
            if (e is DossierViewModel d) // Si l'élément est un dossier...
            {
                _courant = d.Dossier;
                OnPropertyChanged(nameof(Courant));

                Recharge();
            }
        }

        public void Selectionne(ElementViewModel[] elements)
        {
            _selection = elements;
            OnPropertyChanged(nameof(SelectionNombre));
            OnPropertyChanged(nameof(SelectionTaille));
            OnPropertyChanged(nameof(SelectionDate));
        }

        public int SelectionNombre
        {
            get { return _selection.Length; }
        }

        public long SelectionTaille
        {
            get 
            {
                if (_selection.Length == 0) return 0L; // Valeur par défaut (sélection vide).
                return _selection.Sum(e => e.Taille); 
            }
        }

        public DateTime SelectionDate
        {
            get 
            {
                if (_selection.Length == 0) return DateTime.MinValue; // Valeur par défaut (sélection vide).
                return _selection.Max(e => e.Date); 
            }
        }
    }
}
