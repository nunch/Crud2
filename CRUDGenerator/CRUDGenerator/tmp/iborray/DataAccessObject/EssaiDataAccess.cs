using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.Entity.Validation;

namespace DataAccess.DataAccessObject
{
    public class EssaiDataAccess
    {
        
        public MessageModel AddEssai(ref Essai essai)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Essai.Add(essai);
                    db.SaveChanges();
                    return new MessageModel(true, "Essai ajouté avec succès.", essai);
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

        public MessageModel UpdateEssai(Essai essai)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Essai essai_fromDB = (from tmp in db.Essai where tmp.Id_Essai == essai.Id_Essai select tmp).FirstOrDefault();
                    if (essai_fromDB != null)
                    {
                        db.Entry(essai_fromDB).CurrentValues.SetValues(essai);
                        db.SaveChanges();
                        return new MessageModel(true, "La modification a eu lieu avec succès.", essai_fromDB);
                    }
                    else return new MessageModel(false, "Essai non éxistant.", null);
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


        public MessageModel RemoveEssai(int Id_Essai)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Essai essai_fromDB = (from tmp in db.Essai where tmp.Id_Essai == Id_Essai select tmp).FirstOrDefault();
                    essai_fromDB.IsSuppr = true;
                    db.Entry(essai_fromDB).CurrentValues.SetValues(essai_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "Essai effacé", null);
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
    
        public MessageModel RemoveEssai(int Id_Essai, bool setIsSuppr)
        {
            if(setIsSuppr)
            {
                return RemoveEssai(Id_Essai);
            }
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Essai essai_fromDB = (from tmp in db.Essai where tmp.Id_Essai == Id_Essai select tmp).FirstOrDefault();
                    db.Essai.Remove(essai_fromDB);
                    db.SaveChanges();
                    return new MessageModel(true, "Essai effacé", null);
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

        public MessageModel GetEssaiById(int Id_Essai)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    Essai essai = (from tmp in db.Essai where tmp.Id_Essai == Id_Essai select tmp).FirstOrDefault();
                    return new MessageModel(true, "", essai);
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

        public MessageModel RemoveAllEssai()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM Essai");
                    // Exemple with parameter
                    //db.Database.ExecuteSqlCommand("DELETE FROM Essai where Id_Essai = {0}", Id_Essai);
                    return new MessageModel(true, "Essais effacé(e)s", null);
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

        public MessageModel GetAllEssai()
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essai> list = (from tmp in db.Essai where tmp.IsSuppr == false select tmp).ToList();
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
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Programme == programmeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Programme == programmeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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

        public MessageModel GetByProgrammeId_ProgrammeId(int programmeId, int programmeId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Programme == programmeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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

        public MessageModel GetByProgrammeId_ProgrammeId(int programmeId, int programmeId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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

        public MessageModel GetByProgrammeId_ProgrammeId(int programmeId, int programmeId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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

        public MessageModel GetByProgrammeId_ProgrammeId_ProgrammeId(int programmeId, int programmeId, int programmeId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Programme == programmeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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

        public MessageModel GetByProgrammeId_ProgrammeId(int programmeId, int programmeId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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

        public MessageModel GetByProgrammeId_ProgrammeId(int programmeId, int programmeId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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

        public MessageModel GetByProgrammeId_ProgrammeId_ProgrammeId(int programmeId, int programmeId, int programmeId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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

        public MessageModel GetByProgrammeId_ProgrammeId(int programmeId, int programmeId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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

        public MessageModel GetByProgrammeId_ProgrammeId_ProgrammeId(int programmeId, int programmeId, int programmeId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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

        public MessageModel GetByProgrammeId_ProgrammeId_ProgrammeId(int programmeId, int programmeId, int programmeId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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

        public MessageModel GetByProgrammeId_ProgrammeId_ProgrammeId_ProgrammeId(int programmeId, int programmeId, int programmeId, int programmeId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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

        public MessageModel GetByTypeId(int typeId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Type == typeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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

        public MessageModel GetByProgrammeId_TypeId(int programmeId, int typeId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Programme == programmeId && tmp.Id_Type == typeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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

        public MessageModel GetByProgrammeId_TypeId(int programmeId, int typeId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Programme == programmeId && tmp.Id_Type == typeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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

        public MessageModel GetByProgrammeId_ProgrammeId_TypeId(int programmeId, int programmeId, int typeId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId && tmp.Id_Type == typeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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

        public MessageModel GetByProgrammeId_TypeId(int programmeId, int typeId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Programme == programmeId && tmp.Id_Type == typeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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

        public MessageModel GetByProgrammeId_ProgrammeId_TypeId(int programmeId, int programmeId, int typeId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId && tmp.Id_Type == typeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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

        public MessageModel GetByProgrammeId_ProgrammeId_TypeId(int programmeId, int programmeId, int typeId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId && tmp.Id_Type == typeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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

        public MessageModel GetByProgrammeId_ProgrammeId_ProgrammeId_TypeId(int programmeId, int programmeId, int programmeId, int typeId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId && tmp.Id_Type == typeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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

        public MessageModel GetByProgrammeId_TypeId(int programmeId, int typeId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Programme == programmeId && tmp.Id_Type == typeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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

        public MessageModel GetByProgrammeId_ProgrammeId_TypeId(int programmeId, int programmeId, int typeId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId && tmp.Id_Type == typeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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

        public MessageModel GetByProgrammeId_ProgrammeId_TypeId(int programmeId, int programmeId, int typeId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId && tmp.Id_Type == typeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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

        public MessageModel GetByProgrammeId_ProgrammeId_ProgrammeId_TypeId(int programmeId, int programmeId, int programmeId, int typeId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId && tmp.Id_Type == typeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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

        public MessageModel GetByProgrammeId_ProgrammeId_TypeId(int programmeId, int programmeId, int typeId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId && tmp.Id_Type == typeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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

        public MessageModel GetByProgrammeId_ProgrammeId_ProgrammeId_TypeId(int programmeId, int programmeId, int programmeId, int typeId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId && tmp.Id_Type == typeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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

        public MessageModel GetByProgrammeId_ProgrammeId_ProgrammeId_TypeId(int programmeId, int programmeId, int programmeId, int typeId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId && tmp.Id_Type == typeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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

        public MessageModel GetByProgrammeId_ProgrammeId_ProgrammeId_ProgrammeId_TypeId(int programmeId, int programmeId, int programmeId, int programmeId, int typeId)
        {
            try
            {
                using (var db = new DataAccessContainer())
                {
                    List<Essai> list = (from tmp in db.Essai
                                where tmp.IsSuppr == false && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId && tmp.Id_Programme == programmeId && tmp.Id_Type == typeId
                                select tmp).ToList();
                    return new MessageModel(true, "Liste des essai", list);
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