using System;

namespace CRUDGenerator.Database
{
	public class TypeString
	{
		public static string ToString(Type t)
		{
            switch (t)
            {
                case Type.Int:
                    return "int";
                    break;
                case Type.Int_AI:
                    return "int AI";
                    break;
                case Type.Boolean:
                    return "bool";
                    break;
                case Type.String:
                    return "varchar";
                    break;
                case Type.Text:
                    return "text";
                    break;
                case Type.Float:
                    return "float";
                    break;
                case Type.Datetime:
                    return "datetime";
                    break;
                case Type.Date:
                    return "date";
                    break;
                case Type.Decimal:
                    return "decimal";
                    break;
                case Type.Bigint:
                    return "bigint";
                    break;
                case Type.Smallint:
                    return "smallint";
                    break;
                case Type.Tinyint:
                    return "tinyint";
                    break;
                default:
                    break;
            }
			return "";
		}

        public static Type toType(string typeString)
        {
            switch (typeString)
            {
                case "int":
                    return Type.Int;
                    break;
                case "int AI":
                    return Type.Int_AI;
                    break;
                case "bool":
                case "bit":
                    return Type.Boolean;
                    break;
                case "varchar":
                case "nvarchar":
                    return Type.String;
                    break;
                case "text":
                    return Type.Text;
                    break;
                case "float":
                    return Type.Float;
                    break;
                case "datetime":
                    return Type.Datetime;
                    break;
                case "date":
                    return Type.Date;
                    break;
                case "decimal":
                    return Type.Decimal;
                    break;
                case "tinyint":
                    return Type.Tinyint;
                    break;
                case "smallint":
                    return Type.Smallint;
                    break;
                case "bigint":
                    return Type.Bigint;
                    break;
                default:
                    break;
            }
            return Type.Int;
        }
	}

	public enum Type
    {
        Int,
        Int_AI,
        Boolean,
        String,
        Text,
        Float,
        Datetime,
        Date,
        Decimal,
        Tinyint,
        Smallint,
        Bigint
	}
}

