using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using AppliancesStore.Models;
using AppliancesStore.Models.Repository;
using System.Web.ModelBinding;

namespace AppliancesStore.Pages.Admin
{
    public partial class Appliances : System.Web.UI.Page
    {
        private Repository repository = new Repository();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IEnumerable<Appliance> GetAppliances()
        {
            return repository.Appliances;
        }

        public void UpdateAppliance(int ApplianceID)     
        {
            Appliance myAppliance = repository.Appliances
                .Where(p => p.ApplianceId == ApplianceID).FirstOrDefault();
            if (myAppliance != null && TryUpdateModel(myAppliance, new FormValueProvider(ModelBindingExecutionContext)))
            {
                repository.SaveAppliance(myAppliance);
            }        
        }

        public void DeleteAppliance(int ApplianceID) 
        {
            Appliance myAppliance = repository.Appliances
                 .Where(p => p.ApplianceId == ApplianceID).FirstOrDefault();
            if (myAppliance != null)
            {
                repository.DeleteAppliance(myAppliance);
            }
        }

        public void InsertAppliance()
        {
            Appliance myAppliance = new Appliance();
            if(TryUpdateModel(myAppliance, new FormValueProvider(ModelBindingExecutionContext)))
            {
                repository.SaveAppliance(myAppliance);
            }
        }
    }
}
