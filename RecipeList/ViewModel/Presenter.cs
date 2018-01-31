using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Model;

namespace ViewModel
{
    public class Presenter : ObservableObject
    {
        private readonly TextConverter _textConverter = new TextConverter(x => x.ToUpper());
        private readonly ObservableCollection<Ingredient> _ingredients = new ObservableCollection<Ingredient>();
        private string _name;
        private string _quantity;
        private string _unit;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChangedEvent("Name");
            }
        }

        public string Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                RaisePropertyChangedEvent("Quantity");
            }
        }

        public string Unit
        {
            get { return _unit; }
            set
            {
                _unit = value;
                RaisePropertyChangedEvent("Unit");
            }
        }

        public IEnumerable<Ingredient> History
        {
            get { return _ingredients; }
        }

        public ICommand AddItemCommand
        {
            get { return new DelegateCommand(AddItem); }
        }

        private void AddItem()
        {
            bool AllPopulated = string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Name);
            if (AllPopulated)
            {
                return;
            }

            AddToHistory(new Ingredient(Name, Quantity, Unit));
            Name = string.Empty;
            Quantity = string.Empty;
            Unit = string.Empty;
        }

        private void AddToHistory(Ingredient item)
        {
            if (!_ingredients.Contains(item))
            {
                _ingredients.Insert(0, item);
            }
        }
    }
}
