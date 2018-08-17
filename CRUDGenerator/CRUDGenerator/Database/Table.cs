using System;
using System.Collections.Generic;

namespace CRUDGenerator.Database
{
	public class Table
	{
        public string Type { get; set; }
		public string Name { get; set; }
		public List<Column> Columns { get; set; }
		public List<Index> Indexes { get; set; }
		public List<string> PrimaryKey { get; set; }
		public List<UniqueKey> UniqueKeys { get; set; }
		public List<ForeignKey> ForeignKeys { get; set; }

        public Table()
        {
            Type = "Normal";
            Columns = new List<Column>();
            Indexes = new List<Index>();
            PrimaryKey = new List<string>();
            UniqueKeys = new List<UniqueKey>();
            ForeignKeys = new List<ForeignKey>();
        }

		public void addUniqueKey(UniqueKey uk){
			bool isOK = false;
			int indexNumber = -1;
			while (isOK == false) {
				indexNumber++;
				string indexName = "uk_" + this.Name + "_" + indexNumber;
				isOK = true;
				foreach (UniqueKey tmp in UniqueKeys) {
					if (tmp.Name == indexName)
						isOK = false;
				}
			}
			uk.Name = "uk_" + this.Name + "_" + indexNumber;
			this.UniqueKeys.Add (uk);
		}

		public void removeUniqueKey(string ukName){
			int indexNumber = -1;
			for (int i = 0; i < UniqueKeys.Count; i++) {
				if (UniqueKeys [i].Name == ukName) {
					indexNumber = i;
					break;
				}
			}
			if(indexNumber != -1)
				this.UniqueKeys.RemoveAt(indexNumber);
		}

		public void updateUniqueKey(UniqueKey uk){
			this.removeUniqueKey (uk.Name);
			this.UniqueKeys.Add (uk);
		}

		public void addForeignKey(ForeignKey fk){
			bool isOK = false;
			int indexNumber = -1;
			while (isOK == false) {
				indexNumber++;
				string indexName = "fk_" + this.Name + "_" + indexNumber;
				isOK = true;
				foreach (ForeignKey tmp in ForeignKeys) {
					if (tmp.Name == indexName)
						isOK = false;
				}
			}
			fk.Name = "fk_" + this.Name + "_" + fk.ExternTableName + "_" + indexNumber;
			this.ForeignKeys.Add (fk);
		}

		public void removeForeignKey(string fkName){
			int indexNumber = -1;
			for (int i = 0; i < ForeignKeys.Count; i++) {
				if (ForeignKeys [i].Name == fkName) {
					indexNumber = i;
					break;
				}
			}
			if(indexNumber != -1)
				this.ForeignKeys.RemoveAt(indexNumber);
		}

		public void updateForeignKey(ForeignKey fk){
			this.removeForeignKey (fk.Name);
			this.ForeignKeys.Add (fk);
		}

		public void addIndex(Index index){
			bool isOK = false;
			int indexNumber = -1;
			while (isOK == false) {
				indexNumber++;
				string indexName = "index_" + this.Name + "_" + indexNumber;
				isOK = true;
				foreach (Index tmp in Indexes) {
					if (tmp.Name == indexName)
						isOK = false;
				}
			}
			index.Name = "index_" + this.Name + "_" + indexNumber;
			this.Indexes.Add (index);
		}

		public void removeIndex(string indexName){
			int indexNumber = -1;
			for (int i = 0; i < Indexes.Count; i++) {
				if (Indexes [i].Name == indexName) {
					indexNumber = i;
					break;
				}
			}
			if(indexNumber != -1)
				this.Indexes.RemoveAt(indexNumber);
		}

		public void updateIndex(Index index){
			this.removeIndex (index.Name);
			this.Indexes.Add (index);
		}

		public ForeignKeyHelper getFKHFromColumn(Column column){
			foreach (ForeignKey fk in ForeignKeys) {
				ForeignKeyHelper tmp = fk.getHelper (column);
				if ( tmp != null)
					return tmp;
			}
			return null;
		}

		public Column getColumnByName(string name)
		{
			foreach (Column column in Columns)
			{
				if (column.Name == name)
					return column;
			}
			throw new Exception("No Column with the name \"" + name + "\" in the table \"" + Name + "\"");
		}

		public List<Column> getPrimaryKeys()
		{
			List<Column> res = new List<Column>();
			foreach (string columnName in PrimaryKey)
			{
				res.Add(getColumnByName(columnName));
			}
			return res;
		}

		public List<List<Column>> getIndexes()
		{
			List<List<Column>> res = new List<List<Column>>();
			foreach (Index index in Indexes)
			{
				List<Column> tmp = new List<Column>();
				foreach (string columnName in index.List)
				{
					tmp.Add(getColumnByName(columnName));
				}
				res.Add(tmp);
			}
			return res;
		}

		public List<List<Column>> getUniqueKeys()
		{
			List<List<Column>> res = new List<List<Column>>();
			foreach (UniqueKey uk in UniqueKeys)
			{
				List<Column> tmp = new List<Column>();
				foreach (string columnName in uk.List)
				{
					tmp.Add(getColumnByName(columnName));
				}
				res.Add(tmp);
			}
			return res;
		}

		public List<Column> getNormalColumns()
		{
			List<Column> res = new List<Column>();
			foreach (Column column in this.Columns)
			{
				if (!this.PrimaryKey.Contains(column.Name))
					res.Add(column);
			}
			return res;
		}

		public void removeColumnByName(string columnName)
		{
			foreach (string columnNameTmp in this.PrimaryKey)
			{
				if (columnName == columnNameTmp)
				{
					throw new Exception("Vous ne pouvez pas supprimer une colone présente dans la clé primaire");
				}
			}
			List<Index> listTmp1 = new List<Index>();
			for (int i = 0; i < this.Indexes.Count; i++)
			{
				Index tmp = new Index{
					Name = this.Indexes[i].Name
				};
				for (int j = 0; j < this.Indexes[i].List.Count; j++)
				{
					if (this.Indexes [i].List [j] != columnName)
						tmp.List.Add (this.Indexes [i].List [j]);
					else {
						if (this.Indexes [i].List.Count > 1) {
							throw new Exception ("Vous ne pouvez pas supprimer une colone présente dans un index à plusieurs colones");
						} else {
							tmp.List.Add (this.Indexes [i].List [j]);
						}
					}
				}
				if (tmp.List.Count > 0)
				{
					listTmp1.Add(tmp);
				}
			}

			List<UniqueKey> listTmp2 = new List<UniqueKey>();
			for (int i = 0; i < this.UniqueKeys.Count; i++)
			{
				UniqueKey tmp = new UniqueKey();
				for (int j = 0; j < this.UniqueKeys[i].List.Count; j++)
				{
					if (this.UniqueKeys [i].List [j] != columnName)
						tmp.List.Add (this.UniqueKeys [i].List [j]);
					else {
						if (this.UniqueKeys [i].List.Count > 1) {
							throw new Exception ("Vous ne pouvez pas supprimer une colone présente dans une clé unique à plusieurs colones");
						} else {
							tmp.List.Add (this.UniqueKeys [i].List [j]);
						}
					}
				}
				if (tmp.List.Count > 0)
				{
					listTmp2.Add(tmp);
				}
			}

			List<ForeignKey> tmpFK = new List<ForeignKey>();
			foreach (ForeignKey fk in this.ForeignKeys)
			{
				bool b = true;
				foreach (ForeignKeyInside fki in fk.Info)
				{
					if (fki.ColumnName == columnName) {
						if(fk.Info.Count > 1){
							throw new Exception ("Vous ne pouvez pas supprimer une colone présente dans une clé étrangère à plusieurs colones");
						}
						b = false;
					}
				}
				if (b == true)
					tmpFK.Add(fk);
			}
			this.Indexes = listTmp1;
			this.UniqueKeys = listTmp2;
			this.ForeignKeys = tmpFK;

			List<Column> columnTmp = new List<Column>();
			foreach (Column column in this.Columns)
			{
				if (column.Name != columnName)
					columnTmp.Add(column);
			}
			this.Columns = columnTmp;
		}

		public void updateColumn(string previousName, Column newColumn, DatabaseXML db)
		{
			Column c2 = new Column {
				Name = previousName,
				isNull = newColumn.isNull,
				Length = newColumn.Length,
				Type = newColumn.Type
			};
			List<Index> tmp1 = new List<Index>();
			foreach (Index index in this.Indexes)
			{
				Index tmp_1 = new Index{
					Name = index.Name
				};
				foreach (string name in index.List)
				{
					if (name == previousName)
						tmp_1.List.Add(newColumn.Name);
					else
						tmp_1.List.Add(name);
				}
				tmp1.Add(tmp_1);
			}
			this.Indexes = tmp1;

			List<UniqueKey> tmp5 = new List<UniqueKey>();
			foreach (UniqueKey uk in this.UniqueKeys)
			{
				UniqueKey tmp_1 = new UniqueKey{
					Name = uk.Name
				};
				foreach (string name in uk.List)
				{
					if (name == previousName)
						tmp_1.List.Add(newColumn.Name);
					else
						tmp_1.List.Add(name);
				}
				tmp5.Add(tmp_1);
			}
			this.UniqueKeys = tmp5;

			List<Column> tmp2 = new List<Column>();
			foreach (Column column in this.Columns)
			{
				if (column.Name == previousName)
					tmp2.Add(newColumn);
				else
					tmp2.Add(column);
			}
			this.Columns = tmp2;

			List<string> tmp3 = new List<string>();
			foreach (string name in this.PrimaryKey)
			{
				if (name == previousName)
					tmp3.Add(newColumn.Name);
				else
					tmp3.Add(name);
			}
			this.PrimaryKey = tmp3;

			List<ForeignKey> tmp4 = new List<ForeignKey>();
			foreach (ForeignKey fk in this.ForeignKeys)
			{
				ForeignKey fk2 = new ForeignKey
				{
					ExternTableName = fk.ExternTableName,
					Info = new List<ForeignKeyInside>()
				};
				foreach (ForeignKeyInside fki in fk.Info)
				{
					if (fki.ColumnName == previousName)
					{
						fk2.Info.Add(new ForeignKeyInside
							{
								ColumnName = newColumn.Name,
								ExternColumnName = fki.ExternColumnName
							});
					}
					else
					{
						fk2.Info.Add(new ForeignKeyInside
							{
								ColumnName = fki.ColumnName,
								ExternColumnName = fki.ExternColumnName
							});
					}
				}
				tmp4.Add(fk2);
			}
			this.ForeignKeys = tmp4;

			foreach (Table table in db.Content)
			{
				if (table.Name != this.Name)
				{
					bool keep = true;
					foreach (ForeignKey fk in table.ForeignKeys)
					{
						if (keep == true) {
							if (fk.ExternTableName == this.Name)
							{
								foreach (ForeignKeyInside fki in fk.Info)
								{
									if (keep == true) {
										if (fki.ExternColumnName == previousName)
										{
											c2.Name = fki.ColumnName;
											table.updateColumn (c2.Name, c2, db);
											keep = false;
										}
									}

								}
							}
						}
					}
				}
			}
		}

        public ForeignKey getForeignKeyByName(string Name)
        {
            foreach (ForeignKey fk in this.ForeignKeys)
            {
                if (fk.Name == Name)
                    return fk;
            }
            return null;
        }

        public Index getIndexByName(string Name)
        {
            foreach (Index index in this.Indexes)
            {
                if (index.Name == Name)
                    return index;
            }
            return null;
        }

        public UniqueKey getUniqueKeyByName(string Name)
        {
            foreach (UniqueKey uk in this.UniqueKeys)
            {
                if (uk.Name == Name)
                    return uk;
            }
            return null;
        }

		public bool accept(ValidationVisitor visitor, DatabaseXML db){
			return visitor.visiteTable (this, db);
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			Table table = (Table)obj;
			if (this.Name != table.Name || this.Columns != table.Columns || this.Indexes != table.Indexes || this.PrimaryKey != table.PrimaryKey || this.UniqueKeys != table.UniqueKeys || this.ForeignKeys != table.ForeignKeys)
				return false;

			return true;
		}

		public override int GetHashCode()
		{
			int hash = 17;
			hash = hash * 23 + this.Name.GetHashCode();
			hash = hash * 23 + this.Columns.GetHashCode();
			hash = hash * 23 + this.Indexes.GetHashCode();
			hash = hash * 23 + this.PrimaryKey.GetHashCode();
			hash = hash * 23 + this.UniqueKeys.GetHashCode();
			hash = hash * 23 + this.ForeignKeys.GetHashCode();
			return hash;
		}
	}
}

