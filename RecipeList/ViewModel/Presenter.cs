using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Model;
using System.Xml;
using System.IO;

namespace ViewModel
{
    public class Presenter : ObservableObject
    {
        private ObservableCollection<Ingredient> _ingredients = new ObservableCollection<Ingredient>();
        private string _name;
        private string _quantity;
        private string _unit;
        private readonly string RECIPE_FILE_NAME = "Recipe.xml";
        private bool _visAddIngredient = false;

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

        public bool VisAddIngredient
        {
            get { return _visAddIngredient; }
            set
            {
                _visAddIngredient = value;
                RaisePropertyChangedEvent("VisAddIngredient");
                RaisePropertyChangedEvent("NotVisAddIngredient");
            }
        }

        //Inverse of VisAddIngredient
        public bool NotVisAddIngredient
        {
            get { return !_visAddIngredient; }
        }

        //Not Sure what this does
        public IEnumerable<Ingredient> Ingredient
        {
            get { return _ingredients; }
        }

        public ICommand AddItemCommand
        {
            get { return new DelegateCommand(AddItem); }
        }

        public ICommand DontAddItemCommand
        {
            get { return new DelegateCommand(DontAddItem); }
        }

        public ICommand SaveRecipeCommand
        {
            get { return new DelegateCommand(WriteXMLString); }
        }

        public ICommand LoadRecipeCommand
        {
            get { return new DelegateCommand(LoadXMLString); }
        }

        public ICommand ToggleAddIngredientCommand
        {
            get { return new DelegateCommand(ToggleAddIngredient); }
        }

        public void ToggleAddIngredient()
        {
            VisAddIngredient = !VisAddIngredient;
        }

        private void AddItem()
        {
            bool AllPopulated = string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Quantity) || string.IsNullOrWhiteSpace(Unit);
            if (AllPopulated)
            {
                return;
            }

            _ingredients.Add(new Ingredient(Name, Quantity, Unit));
            ClearIngredientFields();
        }

        private void DontAddItem()
        {
            VisAddIngredient = false;
            ClearIngredientFields();
        }

        private void ClearIngredientFields()
        {
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

        public void WriteXMLString()
        {
            File.WriteAllText(RECIPE_FILE_NAME, ToXMLString());
        }

        public void LoadXMLString()
        {
            CreateIngredientListFromXMLString(File.ReadAllText(RECIPE_FILE_NAME));
        }

        public string ToXMLString()
        {
            XmlDocument ingredientDoc = new XmlDocument();

            XmlNode ingredients = ingredientDoc.CreateElement("Ingredients");
            ingredients = ingredientDoc.AppendChild(ingredients);

            foreach (Ingredient ingredient in _ingredients)
            {
                XmlNode ingredientNode = ingredientDoc.CreateElement("Ingredient");

                XmlAttribute nameAttribute = ingredientDoc.CreateAttribute("IngredientName");
                nameAttribute.Value = ingredient.Name;
                ingredientNode.Attributes.Append(nameAttribute);

                XmlAttribute quantityAttribute = ingredientDoc.CreateAttribute("Quantity");
                quantityAttribute.Value = ingredient.Quantity;
                ingredientNode.Attributes.Append(quantityAttribute);

                XmlAttribute unitAttribute = ingredientDoc.CreateAttribute("Unit");
                unitAttribute.Value = ingredient.Unit;
                ingredientNode.Attributes.Append(unitAttribute);

                ingredients.AppendChild(ingredientNode);
            }

            return ingredientDoc.InnerXml;
        }

        public void CreateIngredientListFromXMLString(string xmlIngredientList)
        {
            try
            {
                _ingredients.Clear();

                XmlDocument ingredientDoc = new XmlDocument();

                ingredientDoc.LoadXml(xmlIngredientList);

                XmlNodeList listOfIngredients = ingredientDoc.SelectNodes("/Ingredients/Ingredient");

                foreach (XmlNode ingredient in listOfIngredients)
                {
                    string name = ingredient.Attributes["IngredientName"].Value;
                    string quantity = ingredient.Attributes["Quantity"].Value;
                    string unit = ingredient.Attributes["Unit"].Value;

                    _ingredients.Add(new Ingredient(name, quantity, unit));
                }
            }
            catch
            {
                //Do nothing
            }
        }
    }
}
