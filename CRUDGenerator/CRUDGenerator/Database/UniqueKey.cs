using System;
using System.Collections.Generic;

namespace CRUDGenerator.Database
{
	public class UniqueKey{
		public string Name { get; set; }
		public List<string> List { get; set; }

		public UniqueKey ()
		{
			List = new List<string> ();
		}

		public override bool Equals(object obj)
		{

			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			UniqueKey fk = (UniqueKey)obj;
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

