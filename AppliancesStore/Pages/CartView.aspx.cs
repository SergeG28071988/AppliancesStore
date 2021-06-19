using AppliancesStore.Models;
using AppliancesStore.Models.Repository;
using AppliancesStore.Pages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

namespace AppliancesStore.Pages
{
    public partial class CartView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Repository repository = new Repository();
                int applianceId;
                if (int.TryParse(Request.Form["remove"], out applianceId))
                {
                    Appliance applianceToRemove = repository.Appliances
                        .Where(a => a.ApplianceId == applianceId).FirstOrDefault();
                    if (applianceToRemove != null)
                    {
                        SessionHelper.GetCart(Session).RemoveLine(applianceToRemove);
                    }
                }
            }
        }

        public IEnumerable<CartLine> GetCartLines()
        {
            return SessionHelper.GetCart(Session).Lines;
        }

        public decimal CartTotal
        {
            get
            {
                return SessionHelper.GetCart(Session).ComputeTotalValue();
            }
        }

        public string ReturnUrl
        {
            get
            {
                return SessionHelper.Get<string>(Session, SessionKey.RETURN_URL);
            }
        }

        public string CheckoutUrl
        {
            get
            {
                return RouteTable.Routes.GetVirtualPath(null, "checkout",
                    null).VirtualPath;
            }
        }
    }        
}