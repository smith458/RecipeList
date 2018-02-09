using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ViewModel
{
    public class Ingredient
    {
        private string _name;
        private string _quantity;
        private string _unit;

        public Ingredient(string name, string quantity, string unit)
        {
            _name = name.Trim();
            _quantity = quantity.Trim();
            _unit = unit.Trim();
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

        public override string ToString()
        {
            return String.Format("{0,5} {1,8} {2,15}", Quantity, Unit, Name);
        }
    }
}
