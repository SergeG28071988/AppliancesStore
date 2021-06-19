using AppliancesStore.Models;
using AppliancesStore.Models.Repository;
using AppliancesStore.Pages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

namespace AppliancesStore.Pages
{
    public partial class Listing : System.Web.UI.Page
    {
        private Repository repository = new Repository();
        private int pageSize = 4;
        protected int CurrentPage
        {
            get
            {
                int page;
                page = GetPageFromRequest();
                return page > MaxPage ? MaxPage : page;
            }
        }

        private int GetPageFromRequest()
        {
            int page;
            string reqValue = (string)RouteData.Values["page"] ??
                Request.QueryString["page"];
            return reqValue != null && int.TryParse(reqValue, out page) ? page : 1;
        }

        // Новое свойство, возвращающее наибольший номер допустимой страницы
        protected int MaxPage
        {
            get
            {
                int prodCount = FilterAppliances().Count();
                return (int)Math.Ceiling((decimal)prodCount / pageSize);
            }
        }

        // Новый вспомогательный метод для фильтрации бытовой техники  по категориям
        private IEnumerable<Appliance> FilterAppliances()
        {
            IEnumerable<Appliance> appliances = repository.Appliances;
            string currentCategory = (string)RouteData.Values["category"] ??
                Request.QueryString["category"];
            return currentCategory == null ? appliances :
                appliances.Where(p => p.Category == currentCategory);
        }

        public IEnumerable<Appliance> GetAppliances()
        {
            return FilterAppliances()
                .OrderBy(a => a.ApplianceId) 
                .Skip((CurrentPage - 1) * pageSize)
                .Take(pageSize);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                int selectedGameId;
                if (int.TryParse(Request.Form["add"], out selectedGameId))
                {
                    Appliance selectedGame = repository.Appliances
                        .Where(a => a.ApplianceId == selectedGameId).FirstOrDefault();

                    if (selectedGame != null)
                    {
                        SessionHelper.GetCart(Session).AddItem(selectedGame, 1);
                        SessionHelper.Set(Session, SessionKey.RETURN_URL,
                            Request.RawUrl);

                        Response.Redirect(RouteTable.Routes
                            .GetVirtualPath(null, "cart", null).VirtualPath);
                    }
                }
            }
        }
    }
}