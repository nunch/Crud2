using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.Entity.Validation;

namespace DataAccess.DataAccessObject
{
    public class TypeDataAccess
    {
        
        public MessageModel AddType(ref Type type)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Type.Add(type);
                    db.SaveChanges();
                    return new MessageModel(true, "Type ajouté avec succès.", type);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }

        public MessageModel UpdateType(Type type)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Type type_fromDB = (from tmp in db.Type where tmp.Id_Type == type.Id_Type select tmp).FirstOrDefault();
                    if (type_fromDB != null)
                    {
                        db.Entry(type_fromDB).CurrentValues.SetValues(type);
                        db.SaveChanges();
                        return new MessageModel(true, "La modification a eu lieu avec succès.", type_fromDB);
                    }
                    else return new MessageModel(false, "Type non éxistant.", null);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }


        public MessageModel RemoveType(int Id_Type)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Type type_fromDB = (from tmp in db.Type where tmp.Id_Type == Id_Type select tmp).FirstOrDefault();
                    type_fromDB.IsSuppr = true;
                    db.Entry(type_fromDB).CurrentValues.SetValues(type_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "Type effacé", null);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }
    
        public MessageModel RemoveType(int Id_Type, bool setIsSuppr)
        {
            if(setIsSuppr)
            {
                return RemoveType(Id_Type);
            }
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Type type_fromDB = (from tmp in db.Type where tmp.Id_Type == Id_Type select tmp).FirstOrDefault();
                    db.Type.Remove(type_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "Type effacé", null);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }

        public MessageModel GetTypeById(int Id_Type)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Type type = (from tmp in db.Type where tmp.Id_Type == Id_Type select tmp).FirstOrDefault();
                    return new MessageModel(true, "", type);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }

        public MessageModel RemoveAllType()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM Type");
                    // Exemple with parameter
                    //db.Database.ExecuteSqlCommand("DELETE FROM Type where Id_Type = {0}", Id_Type);
                    return new MessageModel(true, "Types effacé(e)s", null);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }

        public MessageModel GetAllType()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Type> list = (from tmp in db.Type where tmp.IsSuppr == false select tmp).ToList();
                    return new MessageModel(true, "", list);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                List<string> errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add("Error Property Name " + error.PropertyName +" : Error Message: " + error.ErrorMessage);
                    }
                }
                return new MessageModel(false, dbEx.Message.ToString(), errorList);
            }
            catch (Exception e)
            {
                return new MessageModel(false, e.Message.ToString(), e);
            }
        }

    }
}