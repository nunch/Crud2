using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.Entity.Validation;

namespace DataAccess.DataAccessObject
{
    public class RoleDataAccess
    {
        
        public MessageModel AddRole(ref Role role)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Role.Add(role);
                    db.SaveChanges();
                    return new MessageModel(true, "Role ajouté avec succès.", role);
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

        public MessageModel UpdateRole(Role role)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Role role_fromDB = (from tmp in db.Role where tmp.Id_Role == role.Id_Role select tmp).FirstOrDefault();
                    if (role_fromDB != null)
                    {
                        db.Entry(role_fromDB).CurrentValues.SetValues(role);
                        db.SaveChanges();
                        return new MessageModel(true, "La modification a eu lieu avec succès.", role_fromDB);
                    }
                    else return new MessageModel(false, "Role non éxistant.", null);
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


        public MessageModel RemoveRole(int Id_Role)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Role role_fromDB = (from tmp in db.Role where tmp.Id_Role == Id_Role select tmp).FirstOrDefault();
                    role_fromDB.IsSuppr = true;
                    db.Entry(role_fromDB).CurrentValues.SetValues(role_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "Role effacé", null);
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
    
        public MessageModel RemoveRole(int Id_Role, bool setIsSuppr)
        {
            if(setIsSuppr)
            {
                return RemoveRole(Id_Role);
            }
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Role role_fromDB = (from tmp in db.Role where tmp.Id_Role == Id_Role select tmp).FirstOrDefault();
                    db.Role.Remove(role_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "Role effacé", null);
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

        public MessageModel GetRoleById(int Id_Role)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Role role = (from tmp in db.Role where tmp.Id_Role == Id_Role select tmp).FirstOrDefault();
                    return new MessageModel(true, "", role);
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

        public MessageModel RemoveAllRole()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM Role");
                    // Exemple with parameter
                    //db.Database.ExecuteSqlCommand("DELETE FROM Role where Id_Role = {0}", Id_Role);
                    return new MessageModel(true, "Roles effacé(e)s", null);
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

        public MessageModel GetAllRole()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Role> list = (from tmp in db.Role where tmp.IsSuppr == false select tmp).ToList();
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