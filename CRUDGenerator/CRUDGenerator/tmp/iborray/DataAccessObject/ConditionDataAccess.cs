using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.Entity.Validation;

namespace DataAccess.DataAccessObject
{
    public class ConditionDataAccess
    {
        
        public MessageModel AddCondition(ref Condition condition)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Condition.Add(condition);
                    db.SaveChanges();
                    return new MessageModel(true, "Condition ajouté avec succès.", condition);
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

        public MessageModel UpdateCondition(Condition condition)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Condition condition_fromDB = (from tmp in db.Condition where tmp.Id_Condition == condition.Id_Condition select tmp).FirstOrDefault();
                    if (condition_fromDB != null)
                    {
                        db.Entry(condition_fromDB).CurrentValues.SetValues(condition);
                        db.SaveChanges();
                        return new MessageModel(true, "La modification a eu lieu avec succès.", condition_fromDB);
                    }
                    else return new MessageModel(false, "Condition non éxistant.", null);
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


        public MessageModel RemoveCondition(int Id_Condition)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Condition condition_fromDB = (from tmp in db.Condition where tmp.Id_Condition == Id_Condition select tmp).FirstOrDefault();
                    condition_fromDB.IsSuppr = true;
                    db.Entry(condition_fromDB).CurrentValues.SetValues(condition_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "Condition effacé", null);
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
    
        public MessageModel RemoveCondition(int Id_Condition, bool setIsSuppr)
        {
            if(setIsSuppr)
            {
                return RemoveCondition(Id_Condition);
            }
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Condition condition_fromDB = (from tmp in db.Condition where tmp.Id_Condition == Id_Condition select tmp).FirstOrDefault();
                    db.Condition.Remove(condition_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "Condition effacé", null);
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

        public MessageModel GetConditionById(int Id_Condition)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Condition condition = (from tmp in db.Condition where tmp.Id_Condition == Id_Condition select tmp).FirstOrDefault();
                    return new MessageModel(true, "", condition);
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

        public MessageModel RemoveAllCondition()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM Condition");
                    // Exemple with parameter
                    //db.Database.ExecuteSqlCommand("DELETE FROM Condition where Id_Condition = {0}", Id_Condition);
                    return new MessageModel(true, "Conditions effacé(e)s", null);
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

        public MessageModel GetAllCondition()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Condition> list = (from tmp in db.Condition where tmp.IsSuppr == false select tmp).ToList();
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

        public MessageModel GetByRapportId(int rapportId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Condition> list = (from tmp in db.Condition
                                where tmp.IsSuppr == false && tmp.Id_Rapport == rapportId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des condition", list);
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