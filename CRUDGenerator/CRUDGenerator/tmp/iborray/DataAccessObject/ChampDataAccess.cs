using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.Entity.Validation;

namespace DataAccess.DataAccessObject
{
    public class ChampDataAccess
    {
        
        public MessageModel AddChamp(ref Champ champ)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Champ.Add(champ);
                    db.SaveChanges();
                    return new MessageModel(true, "Champ ajouté avec succès.", champ);
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

        public MessageModel UpdateChamp(Champ champ)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Champ champ_fromDB = (from tmp in db.Champ where tmp.Id_Champ == champ.Id_Champ select tmp).FirstOrDefault();
                    if (champ_fromDB != null)
                    {
                        db.Entry(champ_fromDB).CurrentValues.SetValues(champ);
                        db.SaveChanges();
                        return new MessageModel(true, "La modification a eu lieu avec succès.", champ_fromDB);
                    }
                    else return new MessageModel(false, "Champ non éxistant.", null);
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


        public MessageModel RemoveChamp(int Id_Champ)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Champ champ_fromDB = (from tmp in db.Champ where tmp.Id_Champ == Id_Champ select tmp).FirstOrDefault();
                    champ_fromDB.IsSuppr = true;
                    db.Entry(champ_fromDB).CurrentValues.SetValues(champ_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "Champ effacé", null);
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
    
        public MessageModel RemoveChamp(int Id_Champ, bool setIsSuppr)
        {
            if(setIsSuppr)
            {
                return RemoveChamp(Id_Champ);
            }
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Champ champ_fromDB = (from tmp in db.Champ where tmp.Id_Champ == Id_Champ select tmp).FirstOrDefault();
                    db.Champ.Remove(champ_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "Champ effacé", null);
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

        public MessageModel GetChampById(int Id_Champ)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Champ champ = (from tmp in db.Champ where tmp.Id_Champ == Id_Champ select tmp).FirstOrDefault();
                    return new MessageModel(true, "", champ);
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

        public MessageModel RemoveAllChamp()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM Champ");
                    // Exemple with parameter
                    //db.Database.ExecuteSqlCommand("DELETE FROM Champ where Id_Champ = {0}", Id_Champ);
                    return new MessageModel(true, "Champs effacé(e)s", null);
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

        public MessageModel GetAllChamp()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Champ> list = (from tmp in db.Champ where tmp.IsSuppr == false select tmp).ToList();
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

        public MessageModel GetByRoleId(int roleId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Champ> list = (from tmp in db.Champ
                                where tmp.IsSuppr == false && tmp.Id_RoleEncart == roleId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des champ", list);
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