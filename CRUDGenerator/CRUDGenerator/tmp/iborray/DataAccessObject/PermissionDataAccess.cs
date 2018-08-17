using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.Entity.Validation;

namespace DataAccess.DataAccessObject
{
    public class PermissionDataAccess
    {
        
        public MessageModel AddPermission(ref Permission permission)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Permission.Add(permission);
                    db.SaveChanges();
                    return new MessageModel(true, "Permission ajouté avec succès.", permission);
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

        public MessageModel UpdatePermission(Permission permission)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Permission permission_fromDB = (from tmp in db.Permission where tmp.Id_Permission == permission.Id_Permission select tmp).FirstOrDefault();
                    if (permission_fromDB != null)
                    {
                        db.Entry(permission_fromDB).CurrentValues.SetValues(permission);
                        db.SaveChanges();
                        return new MessageModel(true, "La modification a eu lieu avec succès.", permission_fromDB);
                    }
                    else return new MessageModel(false, "Permission non éxistant.", null);
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


        public MessageModel RemovePermission(int Id_Permission)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Permission permission_fromDB = (from tmp in db.Permission where tmp.Id_Permission == Id_Permission select tmp).FirstOrDefault();
                    permission_fromDB.IsSuppr = true;
                    db.Entry(permission_fromDB).CurrentValues.SetValues(permission_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "Permission effacé", null);
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
    
        public MessageModel RemovePermission(int Id_Permission, bool setIsSuppr)
        {
            if(setIsSuppr)
            {
                return RemovePermission(Id_Permission);
            }
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Permission permission_fromDB = (from tmp in db.Permission where tmp.Id_Permission == Id_Permission select tmp).FirstOrDefault();
                    db.Permission.Remove(permission_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "Permission effacé", null);
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

        public MessageModel GetPermissionById(int Id_Permission)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Permission permission = (from tmp in db.Permission where tmp.Id_Permission == Id_Permission select tmp).FirstOrDefault();
                    return new MessageModel(true, "", permission);
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

        public MessageModel RemoveAllPermission()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM Permission");
                    // Exemple with parameter
                    //db.Database.ExecuteSqlCommand("DELETE FROM Permission where Id_Permission = {0}", Id_Permission);
                    return new MessageModel(true, "Permissions effacé(e)s", null);
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

        public MessageModel GetAllPermission()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Permission> list = (from tmp in db.Permission where tmp.IsSuppr == false select tmp).ToList();
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
                    List<Permission> list = (from tmp in db.Permission
                                where tmp.IsSuppr == false && tmp.Id_Role == roleId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des permission", list);
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

        public MessageModel GetByChampId(int champId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Permission> list = (from tmp in db.Permission
                                where tmp.IsSuppr == false && tmp.Id_Champ == champId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des permission", list);
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

        public MessageModel GetByRoleId_ChampId(int roleId, int champId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Permission> list = (from tmp in db.Permission
                                where tmp.IsSuppr == false && tmp.Id_Role == roleId && tmp.Id_Champ == champId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des permission", list);
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