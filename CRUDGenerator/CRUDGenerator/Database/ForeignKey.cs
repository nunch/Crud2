using System;
using System.Collections.Generic;

namespace CRUDGenerator.Database
{
	public class ForeignKeyHelper{
		public string ExternTableName { get; set;}
		public string ExternColumnName { get; set; }
        public ForeignKeyHelper()
        {

        }

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			ForeignKeyHelper fkh = (ForeignKeyHelper)obj;
			if (this.ExternColumnName != fkh.ExternColumnName || this.ExternTableName != fkh.ExternTableName)
				return false;
			return true;
		}

		// override object.GetHashCode
		public override int GetHashCode()
		{
			int hash = 17;
			hash = 23 * hash + this.ExternColumnName.GetHashCode();
			hash = 23 * hash + this.ExternTableName.GetHashCode();
			return hash;
		}
	}

	public class ForeignKeyInside
	{
		public string ColumnName { get; set; }
		public string ExternColumnName { get; set; }
        public ForeignKeyInside()
        {

        }
	}

	public class ForeignKey
	{
		public string Name { get; set; }
		public List<ForeignKeyInside> Info { get; set; }
		public string ExternTableName { get; set; }

		public ForeignKey ()
		{
			Info = new List<ForeignKeyInside> ();
		}

		public ForeignKeyHelper getHelper(Column column){
			foreach (ForeignKeyInside fki in Info) {
				if (fki.ColumnName == column.Name)
					return new ForeignKeyHelper {
						ExternTableName = this.ExternTableName,
						ExternColumnName = fki.ExternColumnName
					};
			}
			return null; 
		}

		public override bool Equals(object obj)
		{

			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			ForeignKey fk = (ForeignKey)obj;
			if (this.Info != fk.Info || this.ExternTableName != fk.ExternTableName)
				return false;
			return true;
		}

		// override object.GetHashCode
		public override int GetHashCode()
		{
			int hash = 17;
			hash = 23 * hash + this.Info.GetHashCode();
			hash = 23 * hash + this.ExternTableName.GetHashCode();
			return hash;
		}
	}
}

