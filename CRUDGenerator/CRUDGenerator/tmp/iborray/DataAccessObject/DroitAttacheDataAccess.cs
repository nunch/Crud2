using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.Entity.Validation;

namespace DataAccess.DataAccessObject
{
    public class DroitAttacheDataAccess
    {
        
        public MessageModel AddDroitAttache(ref DroitAttache droitAttache)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.DroitAttache.Add(droitAttache);
                    db.SaveChanges();
                    return new MessageModel(true, "DroitAttache ajouté avec succès.", droitAttache);
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

        public MessageModel UpdateDroitAttache(DroitAttache droitAttache)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    DroitAttache droitAttache_fromDB = (from tmp in db.DroitAttache where tmp.Id_DroitAttache == droitAttache.Id_DroitAttache select tmp).FirstOrDefault();
                    if (droitAttache_fromDB != null)
                    {
                        db.Entry(droitAttache_fromDB).CurrentValues.SetValues(droitAttache);
                        db.SaveChanges();
                        return new MessageModel(true, "La modification a eu lieu avec succès.", droitAttache_fromDB);
                    }
                    else return new MessageModel(false, "DroitAttache non éxistant.", null);
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


        public MessageModel RemoveDroitAttache(int Id_DroitAttache)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    DroitAttache droitAttache_fromDB = (from tmp in db.DroitAttache where tmp.Id_DroitAttache == Id_DroitAttache select tmp).FirstOrDefault();
                    droitAttache_fromDB.IsSuppr = true;
                    db.Entry(droitAttache_fromDB).CurrentValues.SetValues(droitAttache_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "DroitAttache effacé", null);
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
    
        public MessageModel RemoveDroitAttache(int Id_DroitAttache, bool setIsSuppr)
        {
            if(setIsSuppr)
            {
                return RemoveDroitAttache(Id_DroitAttache);
            }
            try
            {
                using (var db = new DataAccessContainer())
                {
                    DroitAttache droitAttache_fromDB = (from tmp in db.DroitAttache where tmp.Id_DroitAttache == Id_DroitAttache select tmp).FirstOrDefault();
                    db.DroitAttache.Remove(droitAttache_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "DroitAttache effacé", null);
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

        public MessageModel GetDroitAttacheById(int Id_DroitAttache)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    DroitAttache droitAttache = (from tmp in db.DroitAttache where tmp.Id_DroitAttache == Id_DroitAttache select tmp).FirstOrDefault();
                    return new MessageModel(true, "", droitAttache);
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

        public MessageModel RemoveAllDroitAttache()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM DroitAttache");
                    // Exemple with parameter
                    //db.Database.ExecuteSqlCommand("DELETE FROM DroitAttache where Id_DroitAttache = {0}", Id_DroitAttache);
                    return new MessageModel(true, "DroitAttaches effacé(e)s", null);
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

        public MessageModel GetAllDroitAttache()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<DroitAttache> list = (from tmp in db.DroitAttache where tmp.IsSuppr == false select tmp).ToList();
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

        public MessageModel GetByAttacheId(int attacheId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<DroitAttache> list = (from tmp in db.DroitAttache
                                where tmp.IsSuppr == false && tmp.Id_Attache == attacheId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des droitAttache", list);
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
                    List<DroitAttache> list = (from tmp in db.DroitAttache
                                where tmp.IsSuppr == false && tmp.Id_Role == roleId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des droitAttache", list);
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

        public MessageModel GetByAttacheId_RoleId(int attacheId, int roleId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<DroitAttache> list = (from tmp in db.DroitAttache
                                where tmp.IsSuppr == false && tmp.Id_Attache == attacheId && tmp.Id_Role == roleId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des droitAttache", list);
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