using System.Web.Routing;

//namespace AppliancesStore.App_Start
namespace AppliancesStore
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute(null, "list/{category}/{page}",
                                         "~/Pages/Listing.aspx");
            routes.MapPageRoute(null, "list/{page}", "~/Pages/Listing.aspx");
            routes.MapPageRoute(null, "", "~/Pages/Listing.aspx");
            routes.MapPageRoute(null, "list", "~/Pages/Listing.aspx");

            // Обратите внимание что это именованный маршрут
            routes.MapPageRoute("cart", "cart", "~/Pages/CartView.aspx");

            // Ссылка для оформления заказа
            routes.MapPageRoute("checkout", "checkout", "~/Pages/Checkout.aspx");

            // Новые маршруты для административных страниц
            routes.MapPageRoute("admin_orders", "admin/orders", "~/Pages/Admin/Orders.aspx");
            routes.MapPageRoute("admin_appliances", "admin/appliances", "~/Pages/Admin/Appliances.aspx");
        }
    }
}
