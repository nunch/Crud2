using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Net;
using DataAccess;
using DataAccess.DataAccessObject;

namespace GEEC.Controllers.Ajax
{
    public class EssaiDateController : Controller
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
        public string AddEssaiDate(EssaiDate essaiDate)
        {
            MessageModel msg = null;
            MessageModel msgAdd = EssaiDateInstance.AddEssaiDate(ref essaiDate);
            
            return JsonConvert.SerializeObject(msgAdd);
        }

        [HttpPost]
        public string RemoveEssaiDate(int Id_EssaiDate)
        {
            MessageModel msg = EssaiDateInstance.RemoveEssaiDate(Id_EssaiDate);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string UpdateEssaiDate(EssaiDate essaiDate)
        {
            MessageModel msg = null;
            MessageModel updateMsg = EssaiDateInstance.UpdateEssaiDate(essaiDate);
            
            
            return JsonConvert.SerializeObject(updateMsg);
        }

        [HttpPost]
        public string GetEssaiDateById(int Id_EssaiDate)
        {
            MessageModel msg = EssaiDateInstance.GetEssaiDateById(Id_EssaiDate);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetAllEssaiDate()
        {
            MessageModel msg = EssaiDateInstance.GetAllEssaiDate();
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByEssaiId(int EssaiId)
        {
            MessageModel msg = EssaiDateInstance.GetByEssaiId(EssaiId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByDateId(int DateId)
        {
            MessageModel msg = EssaiDateInstance.GetByDateId(DateId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByEssaiId_DateId(int EssaiId, int DateId)
        {
            MessageModel msg = EssaiDateInstance.GetByEssaiId_DateId(EssaiId, DateId);
            return JsonConvert.SerializeObject(msg);
        }

    }
}
