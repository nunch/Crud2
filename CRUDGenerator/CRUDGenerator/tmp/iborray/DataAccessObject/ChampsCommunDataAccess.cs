using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.Entity.Validation;

namespace DataAccess.DataAccessObject
{
    public class ChampsCommunDataAccess
    {
        
        public MessageModel AddChampsCommun(ref ChampsCommun champsCommun)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.ChampsCommun.Add(champsCommun);
                    db.SaveChanges();
                    return new MessageModel(true, "ChampsCommun ajouté avec succès.", champsCommun);
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

        public MessageModel UpdateChampsCommun(ChampsCommun champsCommun)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    ChampsCommun champsCommun_fromDB = (from tmp in db.ChampsCommun where tmp.Id_ChampsCommun == champsCommun.Id_ChampsCommun select tmp).FirstOrDefault();
                    if (champsCommun_fromDB != null)
                    {
                        db.Entry(champsCommun_fromDB).CurrentValues.SetValues(champsCommun);
                        db.SaveChanges();
                        return new MessageModel(true, "La modification a eu lieu avec succès.", champsCommun_fromDB);
                    }
                    else return new MessageModel(false, "ChampsCommun non éxistant.", null);
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


        public MessageModel RemoveChampsCommun(int Id_ChampsCommun)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    ChampsCommun champsCommun_fromDB = (from tmp in db.ChampsCommun where tmp.Id_ChampsCommun == Id_ChampsCommun select tmp).FirstOrDefault();
                    champsCommun_fromDB.IsSuppr = true;
                    db.Entry(champsCommun_fromDB).CurrentValues.SetValues(champsCommun_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "ChampsCommun effacé", null);
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
    
        public MessageModel RemoveChampsCommun(int Id_ChampsCommun, bool setIsSuppr)
        {
            if(setIsSuppr)
            {
                return RemoveChampsCommun(Id_ChampsCommun);
            }
            try
            {
                using (var db = new DataAccessContainer())
                {
                    ChampsCommun champsCommun_fromDB = (from tmp in db.ChampsCommun where tmp.Id_ChampsCommun == Id_ChampsCommun select tmp).FirstOrDefault();
                    db.ChampsCommun.Remove(champsCommun_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "ChampsCommun effacé", null);
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

        public MessageModel GetChampsCommunById(int Id_ChampsCommun)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    ChampsCommun champsCommun = (from tmp in db.ChampsCommun where tmp.Id_ChampsCommun == Id_ChampsCommun select tmp).FirstOrDefault();
                    return new MessageModel(true, "", champsCommun);
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

        public MessageModel RemoveAllChampsCommun()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM ChampsCommun");
                    // Exemple with parameter
                    //db.Database.ExecuteSqlCommand("DELETE FROM ChampsCommun where Id_ChampsCommun = {0}", Id_ChampsCommun);
                    return new MessageModel(true, "ChampsCommuns effacé(e)s", null);
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

        public MessageModel GetAllChampsCommun()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<ChampsCommun> list = (from tmp in db.ChampsCommun where tmp.IsSuppr == false select tmp).ToList();
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

        public MessageModel GetByEssaiId(int essaiId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<ChampsCommun> list = (from tmp in db.ChampsCommun
                                where tmp.IsSuppr == false && tmp.Id_Essai == essaiId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des champsCommun", list);
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