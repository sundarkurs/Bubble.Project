using Sitecore;
using Sitecore.Configuration;
using Sitecore.Globalization;
using Sitecore.SecurityModel;

namespace SC.Test
{

    class Program
    {
        static void Main(string[] args)
        {
            MySitecore a = new MySitecore();
            a.GetItem();
        }
    }


    public class MySitecore
    {
        public void GetItem()
        {
            Context.SetLanguage(Language.Parse("en"), false);
            Sitecore.Context.SetActiveSite("CustomerPortalRedesign_Au");

            var db = Sitecore.Configuration.Factory.GetDatabase("master");
            var homeItem = db.GetItem("/sitecore/content/home");
        }
    }


}
