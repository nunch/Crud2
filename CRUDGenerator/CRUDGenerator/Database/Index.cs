using System;
using System.Collections.Generic;

namespace CRUDGenerator.Database
{
	public class Index{
		public string Name { get; set; }
		public List<string> List { get; set; }
		public Index ()
		{
			List = new List<string> ();
		}

		public override bool Equals(object obj)
		{

			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			Index fk = (Index)obj;
			if (this.Name != fk.Name || this.List != fk.List)
				return false;
			return true;
		}

		// override object.GetHashCode
		public override int GetHashCode()
		{
			int hash = 17;
			hash = 23 * hash + this.Name.GetHashCode();
			hash = 23 * hash + this.List.GetHashCode();
			return hash;
		}
	}
}

