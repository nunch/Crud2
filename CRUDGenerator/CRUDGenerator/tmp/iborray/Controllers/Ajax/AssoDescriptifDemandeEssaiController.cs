using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Net;
using DataAccess;
using DataAccess.DataAccessObject;

namespace GEEC.Controllers.Ajax
{
    public class AssoDescriptifDemandeEssaiController : Controller
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
        public string AddAssoDescriptifDemandeEssai(AssoDescriptifDemandeEssai assoDescriptifDemandeEssai)
        {
            MessageModel msg = null;
            MessageModel msgAdd = AssoDescriptifDemandeEssaiInstance.AddAssoDescriptifDemandeEssai(ref assoDescriptifDemandeEssai);
            
            return JsonConvert.SerializeObject(msgAdd);
        }

        [HttpPost]
        public string RemoveAssoDescriptifDemandeEssai(int Id_AssoDescriptifDemandeEssai)
        {
            MessageModel msg = AssoDescriptifDemandeEssaiInstance.RemoveAssoDescriptifDemandeEssai(Id_AssoDescriptifDemandeEssai);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string UpdateAssoDescriptifDemandeEssai(AssoDescriptifDemandeEssai assoDescriptifDemandeEssai)
        {
            MessageModel msg = null;
            MessageModel updateMsg = AssoDescriptifDemandeEssaiInstance.UpdateAssoDescriptifDemandeEssai(assoDescriptifDemandeEssai);
            
            
            return JsonConvert.SerializeObject(updateMsg);
        }

        [HttpPost]
        public string GetAssoDescriptifDemandeEssaiById(int Id_AssoDescriptifDemandeEssai)
        {
            MessageModel msg = AssoDescriptifDemandeEssaiInstance.GetAssoDescriptifDemandeEssaiById(Id_AssoDescriptifDemandeEssai);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetAllAssoDescriptifDemandeEssai()
        {
            MessageModel msg = AssoDescriptifDemandeEssaiInstance.GetAllAssoDescriptifDemandeEssai();
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByDescriptifId(int DescriptifId)
        {
            MessageModel msg = AssoDescriptifDemandeEssaiInstance.GetByDescriptifId(DescriptifId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByDemandeEssaiId(int DemandeEssaiId)
        {
            MessageModel msg = AssoDescriptifDemandeEssaiInstance.GetByDemandeEssaiId(DemandeEssaiId);
            return JsonConvert.SerializeObject(msg);
        }

        [HttpPost]
        public string GetByDescriptifId_DemandeEssaiId(int DescriptifId, int DemandeEssaiId)
        {
            MessageModel msg = AssoDescriptifDemandeEssaiInstance.GetByDescriptifId_DemandeEssaiId(DescriptifId, DemandeEssaiId);
            return JsonConvert.SerializeObject(msg);
        }

    }
}
