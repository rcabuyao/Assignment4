using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsRCabuyao
{
	public class Client
	{
		private string _tag;
		private string _name;
		private double _age;
		private double _weight;

		public Client()
		{
			Tag="XXXX";
			Name="YYYY";
			Age = 0;
			Weight = 0;
		}

		public Client(string tag, string name, double age, double weight)
		{
			Tag=tag;
			Name=name;
			Age = age;
			Weight = weight;
		}

		public string Tag
		{
			get { return _tag; }
			set
			{
				if (string.IsNullOrWhiteSpace(value))
					throw new ArgumentNullException("Tag is required. Must not be empty or blank.");
				_tag = value;
			}
		}

		public string Name
		{
			get { return _name; }
			set
			{
				if (string.IsNullOrWhiteSpace(value))
					throw new ArgumentNullException("Name is required. Must not be empty or blank.");
				_name = value;
			}
		}

		public double Age
		{
			get { return _age; }
			set
			{
				if (value < 0.0)
					throw new ArgumentException("Age must be a positive value (0 or greater)");
				_age = value;
			}
		}

		public double Weight
		{
			get { return _weight; }
			set
			{
				if (value < 0.0)
					throw new ArgumentException("Weight must be a positive value (0 or greater)");
				_weight = value;
			}
		}

		public override string ToString()
		{
			return $"{Tag},{Name},{Age},{Weight}";
		}
	}
}
