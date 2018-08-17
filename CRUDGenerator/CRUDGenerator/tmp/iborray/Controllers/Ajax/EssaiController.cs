using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Net;
using DataAccess;
using DataAccess.DataAccessObject;

namespace GEEC.Controllers.Ajax
{
    public class EssaiController : Controller
    {
        readonly ConditionDataAccess ConditionInstance = new ConditionDataAccess();
        readonly AideDataAccess AideInstance = new AideDataAccess();
        readonly ArchivageChampDataAccess ArchivageChampInstance = new ArchivageChampDataAccess();
        readonly ArchivageValeurDataAccess ArchivageValeurInstance = new ArchivageValeurDataAccess();
        readonly AssoDescriptifDemandeEssaiDataAccess AssoDescriptifDemandeEssaiInstance = new AssoDescriptifDemandeEssaiDataAccess();
        readonly AssoEssaiDemandeEssaiDataAccess AssoEssaiDemandeEssaiInstance = new AssoEssaiDemandeEssaiDataAccess();
        readonly AttacheDataAccess AttacheInstance = new AttacheDataAccess();
        readonly BEConcepteurDataAccess BEConcepteurInstance = new BEConcepteurDataAccess();
        readonly BESpecificateurDataAccess BESpecificateurInstance = new BESpecificateurDataAccess();
        readonly ChampDataAccess ChampInstance = new ChampDataAccess();
        readonly ChampsCommunDataAccess ChampsCommunInstance = new ChampsCommunDataAccess();
        readonly DateDataAccess DateInstance = new DateDataAccess();
        readonly DemandeEssaiDataAccess DemandeEssaiInstance = new DemandeEssaiDataAccess();
        readonly DescriptifDataAccess DescriptifInstance = new DescriptifDataAccess();
        readonly DroitAttacheDataAccess DroitAttacheInstance = new DroitAttacheDataAccess();
        readonly DroitUtilisateurDataAccess DroitUtilisateurInstance = new DroitUtilisateurDataAccess();
        readonly EssaiDataAccess EssaiInstance = new EssaiDataAccess();
        readonly EssaiDateDataAccess EssaiDateInstance = new EssaiDateDataAccess();
        readonly EssayeurDataAccess EssayeurInstance = new EssayeurDataAccess();
        readonly LiaisonArchivageDataAccess LiaisonArchivageInstance = new LiaisonArchivageDataAccess();
        readonly MarqueTechniqueDataAccess MarqueTechniqueInstance = new MarqueTechniqueDataAccess();
        readonly PermissionDataAccess PermissionInstance = new PermissionDataAccess();
        readonly ProgrammeDataAccess ProgrammeInstance = new ProgrammeDataAccess();
        readonly RoleDataAccess RoleInstance = new RoleDataAccess();
        readonly SupplyChainDataAccess SupplyChainInstance = new SupplyChainDataAccess();
        readonly TypeDataAccess TypeInstance = new TypeDataAccess();
        readonly UtilisateurDataAccess UtilisateurInstance = new UtilisateurDataAccess();
        readonly AdminEssaisListDataAccess AdminEssaisListInstance = new AdminEssaisListDataAccess();
        readonly AllEssaisListDataAccess AllEssaisListInstance = new AllEssaisListDataAccess();
        readonly AttacheRolesViewDataAccess AttacheRolesViewInstance = new AttacheRolesViewDataAccess();
        readonly BeConcepteurEssaisListDataAccess BeConcepteurEssaisListInstance = new BeConcepteurEssaisListDataAccess();
        readonly BeConcepteurNewEssaisListDataAccess BeConcepteurNewEssaisListInstance = new BeConcepteurNewEssaisListDataAccess();
        readonly BeSpecificateurEssaisListDataAccess BeSpecificateurEssaisListInstance = new BeSpecificateurEssaisListDataAccess();
        readonly ChampValuesViewDataAccess ChampValuesViewInstance = new ChampValuesViewDataAccess();
        readonly EssayeurEssaisListDataAccess EssayeurEssaisListInstance = new EssayeurEssaisListDataAccess();
        readonly EssayeurNewEssaisListDataAccess EssayeurNewEssaisListInstance = new EssayeurNewEssaisListDataAccess();
        readonly MarqueTechniqueEssaisListDataAccess MarqueTechniqueEssaisListInstance = new MarqueTechniqueEssaisListDataAccess();
        readonly MarqueTechniqueNewEssaisListDataAccess MarqueTechniqueNewEssaisListInstance = new MarqueTechniqueNewEssaisListDataAccess();
        readonly ProgrammeViewDataAccess ProgrammeViewInstance = new ProgrammeViewDataAccess();
        readonly RegroupementChampsDataAccess RegroupementChampsInstance = new RegroupementChampsDataAccess();
        readonly SupplyChainNewEssaisListDataAccess SupplyChainNewEssaisListInstance = new SupplyChainNewEssaisListDataAccess();
        readonly SupplyChainEssaisListDataAccess SupplyChainEssaisListInstance = new SupplyChainEssaisListDataAccess();
        readonly UserPermissionChampListDataAccess UserPermissionChampListInstance = new UserPermissionChampListDataAccess();
        readonly UtilisateurRolesViewDataAccess UtilisateurRolesViewInstance = new UtilisateurRolesViewDataAccess();
        readonly RapportDataAccess RapportInstance = new RapportDataAccess();
        readonly ColonneDataAccess ColonneInstance = new ColonneDataAccess();
        
        
        
        [HttpPost]
        public string AddEssai(Essai essai)
        {
            MessageModel msg = null;
            MessageModel msgAdd = EssaiInstance.AddEssai(ref essai);
            
            return JsonConvert.SerializeObject(msgAdd);
        }

        [HttpPost]
        public string RemoveEssai(int Id_Essai)
        {
            MessageModel msg = EssaiInstance.RemoveEssai(Id_Essai);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string UpdateEssai(Essai essai)
        {
            MessageModel msg = null;
            MessageModel updateMsg = EssaiInstance.UpdateEssai(essai);
            
            
            return JsonConvert.SerializeObject(updateMsg);
        }

        [HttpPost]
        public string GetEssaiById(int Id_Essai)
        {
            MessageModel msg = EssaiInstance.GetEssaiById(Id_Essai);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetAllEssai()
        {
            MessageModel msg = EssaiInstance.GetAllEssai();
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByProgrammeId(int ProgrammeId)
        {
            MessageModel msg = EssaiInstance.GetByProgrammeId(ProgrammeId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByProgrammeId(int ProgrammeId)
        {
            MessageModel msg = EssaiInstance.GetByProgrammeId(ProgrammeId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByProgrammeId_ProgrammeId(int ProgrammeId, int ProgrammeId)
        {
            MessageModel msg = EssaiInstance.GetByProgrammeId_ProgrammeId(ProgrammeId, ProgrammeId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByProgrammeId(int ProgrammeId)
        {
            MessageModel msg = EssaiInstance.GetByProgrammeId(ProgrammeId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByProgrammeId_ProgrammeId(int ProgrammeId, int ProgrammeId)
        {
            MessageModel msg = EssaiInstance.GetByProgrammeId_ProgrammeId(ProgrammeId, ProgrammeId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByProgrammeId_ProgrammeId(int ProgrammeId, int ProgrammeId)
        {
            MessageModel msg = EssaiInstance.GetByProgrammeId_ProgrammeId(ProgrammeId, ProgrammeId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByProgrammeId_ProgrammeId_ProgrammeId(int ProgrammeId, int ProgrammeId, int ProgrammeId)
        {
            MessageModel msg = EssaiInstance.GetByProgrammeId_ProgrammeId_ProgrammeId(ProgrammeId, ProgrammeId, ProgrammeId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByProgrammeId(int ProgrammeId)
        {
            MessageModel msg = EssaiInstance.GetByProgrammeId(ProgrammeId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByProgrammeId_ProgrammeId(int ProgrammeId, int ProgrammeId)
        {
            MessageModel msg = EssaiInstance.GetByProgrammeId_ProgrammeId(ProgrammeId, ProgrammeId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByProgrammeId_ProgrammeId(int ProgrammeId, int ProgrammeId)
        {
            MessageModel msg = EssaiInstance.GetByProgrammeId_ProgrammeId(ProgrammeId, ProgrammeId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByProgrammeId_ProgrammeId_ProgrammeId(int ProgrammeId, int ProgrammeId, int ProgrammeId)
        {
            MessageModel msg = EssaiInstance.GetByProgrammeId_ProgrammeId_ProgrammeId(ProgrammeId, ProgrammeId, ProgrammeId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByProgrammeId_ProgrammeId(int ProgrammeId, int ProgrammeId)
        {
            MessageModel msg = EssaiInstance.GetByProgrammeId_ProgrammeId(ProgrammeId, ProgrammeId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByProgrammeId_ProgrammeId_ProgrammeId(int ProgrammeId, int ProgrammeId, int ProgrammeId)
        {
            MessageModel msg = EssaiInstance.GetByProgrammeId_ProgrammeId_ProgrammeId(ProgrammeId, ProgrammeId, ProgrammeId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByProgrammeId_ProgrammeId_ProgrammeId(int ProgrammeId, int ProgrammeId, int ProgrammeId)
        {
            MessageModel msg = EssaiInstance.GetByProgrammeId_ProgrammeId_ProgrammeId(ProgrammeId, ProgrammeId, ProgrammeId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByProgrammeId_ProgrammeId_ProgrammeId_ProgrammeId(int ProgrammeId, int ProgrammeId, int ProgrammeId, int ProgrammeId)
        {
            MessageModel msg = EssaiInstance.GetByProgrammeId_ProgrammeId_ProgrammeId_ProgrammeId(ProgrammeId, ProgrammeId, ProgrammeId, ProgrammeId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByTypeId(int TypeId)
        {
            MessageModel msg = EssaiInstance.GetByTypeId(TypeId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByProgrammeId_TypeId(int ProgrammeId, int TypeId)
        {
            MessageModel msg = EssaiInstance.GetByProgrammeId_TypeId(ProgrammeId, TypeId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByProgrammeId_TypeId(int ProgrammeId, int TypeId)
        {
            MessageModel msg = EssaiInstance.GetByProgrammeId_TypeId(ProgrammeId, TypeId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByProgrammeId_ProgrammeId_TypeId(int ProgrammeId, int ProgrammeId, int TypeId)
        {
            MessageModel msg = EssaiInstance.GetByProgrammeId_ProgrammeId_TypeId(ProgrammeId, ProgrammeId, TypeId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByProgrammeId_TypeId(int ProgrammeId, int TypeId)
        {
            MessageModel msg = EssaiInstance.GetByProgrammeId_TypeId(ProgrammeId, TypeId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByProgrammeId_ProgrammeId_TypeId(int ProgrammeId, int ProgrammeId, int TypeId)
        {
            MessageModel msg = EssaiInstance.GetByProgrammeId_ProgrammeId_TypeId(ProgrammeId, ProgrammeId, TypeId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByProgrammeId_ProgrammeId_TypeId(int ProgrammeId, int ProgrammeId, int TypeId)
        {
            MessageModel msg = EssaiInstance.GetByProgrammeId_ProgrammeId_TypeId(ProgrammeId, ProgrammeId, TypeId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByProgrammeId_ProgrammeId_ProgrammeId_TypeId(int ProgrammeId, int ProgrammeId, int ProgrammeId, int TypeId)
        {
            MessageModel msg = EssaiInstance.GetByProgrammeId_ProgrammeId_ProgrammeId_TypeId(ProgrammeId, ProgrammeId, ProgrammeId, TypeId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByProgrammeId_TypeId(int ProgrammeId, int TypeId)
        {
            MessageModel msg = EssaiInstance.GetByProgrammeId_TypeId(ProgrammeId, TypeId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByProgrammeId_ProgrammeId_TypeId(int ProgrammeId, int ProgrammeId, int TypeId)
        {
            MessageModel msg = EssaiInstance.GetByProgrammeId_ProgrammeId_TypeId(ProgrammeId, ProgrammeId, TypeId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByProgrammeId_ProgrammeId_TypeId(int ProgrammeId, int ProgrammeId, int TypeId)
        {
            MessageModel msg = EssaiInstance.GetByProgrammeId_ProgrammeId_TypeId(ProgrammeId, ProgrammeId, TypeId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByProgrammeId_ProgrammeId_ProgrammeId_TypeId(int ProgrammeId, int ProgrammeId, int ProgrammeId, int TypeId)
        {
            MessageModel msg = EssaiInstance.GetByProgrammeId_ProgrammeId_ProgrammeId_TypeId(ProgrammeId, ProgrammeId, ProgrammeId, TypeId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByProgrammeId_ProgrammeId_TypeId(int ProgrammeId, int ProgrammeId, int TypeId)
        {
            MessageModel msg = EssaiInstance.GetByProgrammeId_ProgrammeId_TypeId(ProgrammeId, ProgrammeId, TypeId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByProgrammeId_ProgrammeId_ProgrammeId_TypeId(int ProgrammeId, int ProgrammeId, int ProgrammeId, int TypeId)
        {
            MessageModel msg = EssaiInstance.GetByProgrammeId_ProgrammeId_ProgrammeId_TypeId(ProgrammeId, ProgrammeId, ProgrammeId, TypeId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByProgrammeId_ProgrammeId_ProgrammeId_TypeId(int ProgrammeId, int ProgrammeId, int ProgrammeId, int TypeId)
        {
            MessageModel msg = EssaiInstance.GetByProgrammeId_ProgrammeId_ProgrammeId_TypeId(ProgrammeId, ProgrammeId, ProgrammeId, TypeId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByProgrammeId_ProgrammeId_ProgrammeId_ProgrammeId_TypeId(int ProgrammeId, int ProgrammeId, int ProgrammeId, int ProgrammeId, int TypeId)
        {
            MessageModel msg = EssaiInstance.GetByProgrammeId_ProgrammeId_ProgrammeId_ProgrammeId_TypeId(ProgrammeId, ProgrammeId, ProgrammeId, ProgrammeId, TypeId);
            return JsonConvert.SerializeObject(msg);
        }

    }
}
