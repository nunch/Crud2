using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.Entity.Validation;

namespace DataAccess.DataAccessObject
{
    public class ProgrammeDataAccess
    {
        
        public MessageModel AddProgramme(ref Programme programme)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Programme.Add(programme);
                    db.SaveChanges();
                    return new MessageModel(true, "Programme ajouté avec succès.", programme);
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

        public MessageModel UpdateProgramme(Programme programme)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Programme programme_fromDB = (from tmp in db.Programme where tmp.Id_Programme == programme.Id_Programme select tmp).FirstOrDefault();
                    if (programme_fromDB != null)
                    {
                        db.Entry(programme_fromDB).CurrentValues.SetValues(programme);
                        db.SaveChanges();
                        return new MessageModel(true, "La modification a eu lieu avec succès.", programme_fromDB);
                    }
                    else return new MessageModel(false, "Programme non éxistant.", null);
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


        public MessageModel RemoveProgramme(int Id_Programme)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Programme programme_fromDB = (from tmp in db.Programme where tmp.Id_Programme == Id_Programme select tmp).FirstOrDefault();
                    programme_fromDB.IsSuppr = true;
                    db.Entry(programme_fromDB).CurrentValues.SetValues(programme_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "Programme effacé", null);
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
    
        public MessageModel RemoveProgramme(int Id_Programme, bool setIsSuppr)
        {
            if(setIsSuppr)
            {
                return RemoveProgramme(Id_Programme);
            }
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Programme programme_fromDB = (from tmp in db.Programme where tmp.Id_Programme == Id_Programme select tmp).FirstOrDefault();
                    db.Programme.Remove(programme_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "Programme effacé", null);
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

        public MessageModel GetProgrammeById(int Id_Programme)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Programme programme = (from tmp in db.Programme where tmp.Id_Programme == Id_Programme select tmp).FirstOrDefault();
                    return new MessageModel(true, "", programme);
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

        public MessageModel RemoveAllProgramme()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM Programme");
                    // Exemple with parameter
                    //db.Database.ExecuteSqlCommand("DELETE FROM Programme where Id_Programme = {0}", Id_Programme);
                    return new MessageModel(true, "Programmes effacé(e)s", null);
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

        public MessageModel GetAllProgramme()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Programme> list = (from tmp in db.Programme where tmp.IsSuppr == false select tmp).ToList();
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

        public MessageModel GetByProgrammeId(int programmeId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Programme> list = (from tmp in db.Programme
                                where tmp.IsSuppr == false && tmp.Id_Ancien_Programme == programmeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des programme", list);
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