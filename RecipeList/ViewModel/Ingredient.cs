using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class Ingredient
    {
        private string _name;
        private string _quantity;
        private string _unit;

        public Ingredient(string name, string quantity = "1", string unit = "cup")
        {
            _name = name;
            _quantity = quantity;
            _unit = unit;
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string Quantity
        {
            get
            {
                return _quantity;
            }
        }

        public string Unit
        {
            get
            {
                return _unit;
            }
        }
    }
}
