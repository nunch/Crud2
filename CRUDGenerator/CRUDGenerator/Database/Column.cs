using System;

namespace CRUDGenerator.Database
{
    public class Column
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public int Length { get; set; }
        public byte isNull { get; set; }
        public int DecimalLength { get; set; }

        public Column()
        {
            isNull = 0;
            DecimalLength = 0;
        }

        public bool accept(ValidationVisitor visitor, Table table, DatabaseXML db)
        {
            return visitor.visiteColumn(this, table, db);
        }

        public dynamic getLength()
        {
            switch (Type)
            {
                case Type.Int:
                    return Length;
                    break;
                case Type.Int_AI:
                    return Length;
                    break;
                case Type.Boolean:
                    return "/";
                    break;
                case Type.String:
                    return Length;
                    break;
                case Type.Text:
                    return "/";
                    break;
                case Type.Float:
                    return "/";
                    break;
                case Type.Datetime:
                    return "/";
                    break;
                case Type.Date:
                    return "/";
                    break;
                case Type.Decimal:
                    return "("+Length+","+DecimalLength+")";
                    break;
                case Type.Tinyint:
                    return "/";
                    break;
                case Type.Smallint:
                    return "/";
                    break;
                case Type.Bigint:
                    return "/";
                    break;
                default:
                    return "/";
                    break;
            }
        }

        public override bool Equals(object obj)
        {

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Column column = (Column)obj;
            if (this.isNull != column.isNull || this.Length != column.Length || this.Name != column.Name || this.Type != column.Type || this.DecimalLength != column.DecimalLength)
                return false;
            return true;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            int hash = 17;
            hash = 23 * hash + this.isNull.GetHashCode();
            hash = 23 * hash + this.Length.GetHashCode();
            hash = 23 * hash + this.Name.GetHashCode();
            hash = 23 * hash + this.Type.GetHashCode();
            hash = 23 * hash + this.DecimalLength.GetHashCode();
            return hash;
        }
    }
}

