using MaxMind.GeoIP2;
using System;
using System.Globalization;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Frontend;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.LanguageSelector;
using Telerik.Sitefinity.Localization.UrlLocalizationStrategies;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Events;

namespace SitefinityWebApp
{
  static class Regions
  {
    public const string NorthAmerica = "NA";
    public const string SouthAmerica = "SA";
    public const string Europe = "EU";
    public const string Africa = "AF";
  }

  static class Cultures
  {
    public const string NorthAmerica = "en-us";
    public const string SouthAmerica = "en-as";
    public const string Europe = "en-150";
    public const string Africa = "en-za";
    public const string Default = "en";
  }

  public class Global : System.Web.HttpApplication
  {

    protected void Application_Start(object sender, EventArgs e)
    {
      Bootstrapper.Bootstrapped += Bootstrapper_Bootstrapped;

    }

    protected void Session_Start(object sender, EventArgs e)
    {
      EnableGeoIPRedirect = 1;

      if (WebConfigurationManager.AppSettings["MaxMind:Enable"] != "0" && EnableGeoIPRedirect == 1)
      {
        this.UrlService = this.InitializeUrlLocalizationService();
        ToLog("------- Starting GEO-IP -------");
        this.HandleGeoIP();
      }

      EnableGeoIPRedirect = 0;
    }

    protected void Session_End(object sender, EventArgs e)
    {
      EnableGeoIPRedirect = 0;
    }


    private void Bootstrapper_Bootstrapped(object sender, EventArgs e)
    {
      FrontendModule.Current.DependencyResolver.Rebind<ILanguageSelectorModel>().To<CustomLanguageSelectorModel>();
      EventHub.Subscribe<IPagePreRenderCompleteEvent>(this.OnPagePreRenderCompleteEventHandler);

    }

    protected virtual UrlLocalizationService InitializeUrlLocalizationService()
    {
      return ObjectFactory.Resolve<UrlLocalizationService>();
    }

    private void OnPagePreRenderCompleteEventHandler(IPagePreRenderCompleteEvent evt)
    {
      var page = evt.Page;
      var isBackendPage = page.IsBackend();

      var pageNode = evt.PageSiteNode;

      if (!isBackendPage && !page.Title.Contains(" | INX Software"))
      {
        page.Title += " | INX Software";
      }
    }

    private void HandleGeoIP()
    {
      var UserIPAddress = GetIpAddressFromCurrentRequest();
      ToLog("[GEO IP] Users IP: " + UserIPAddress);
      string UserCountry = FindLocation(UserIPAddress).ToString();
      ToLog("[GEO IP] Users Country: " + UserCountry);
      string url = HttpContext.Current.Request.Url.ToString();
      string scheme = HttpContext.Current.Request.Url.Scheme;
      string host = HttpContext.Current.Request.Url.Host;
      string path_and_query = HttpContext.Current.Request.Url.PathAndQuery;

      Server.ClearError();
      Response.Clear();

      switch (UserCountry)
      {
        case Regions.NorthAmerica:
          ToLog("Redirecting to north america");
          Response.Redirect(scheme + "://" + host + "/ca" + path_and_query, true);
          break;
        case Regions.SouthAmerica:
          ToLog("Redirecting to north america from South america");
          Response.Redirect(scheme + "://" + host + "/ca" + path_and_query, true);
          break;
      }
    }

    public void RedirectToCulture(string culture, string url)
    {
      if (this.UrlService == null)
      {
        return;
      }

      var targetCulture = CultureInfo.GetCultureInfo(culture);

      //var url = evt.PageSiteNode.GetUrl(targetCulture, true, true);

      url = this.UrlService.ResolveUrl(url, targetCulture);
      url = RouteHelper.ResolveUrl(url, UrlResolveOptions.Absolute);

      if (url.Contains("404"))
      {
        return;
      }

      if (url.ToLower().Contains("home"))
      {
        int index = url.IndexOf("home");
        url = url.Remove(index);
        url = url.TrimEnd('/');
      }

      EnableGeoIPRedirect = 0;

      ToLog("Redirecting URL " + url);
      ToLog("END ------------ ");
      SystemManager.CurrentHttpContext.Response.Redirect(url, true);
    }

    internal static string GetIpAddressFromCurrentRequest()
    {
      string ipAddress = SystemManager.CurrentHttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

      ToLog("[GEO IP] IP from HTTP_X_FORWARDED_FOR: " + ipAddress);

      if (ipAddress.IsNullOrEmpty())
      {
        ipAddress = SystemManager.CurrentHttpContext.Request.ServerVariables["REMOTE_ADDR"];
        ToLog("[GEO IP] IP from REMOTE_ADDR: " + ipAddress);
      }

      //strip port for Azure
      var portIndex = ipAddress.LastIndexOf(':');
      if (portIndex > 0)
      {
        ipAddress = ipAddress.Remove(portIndex);
      }

      ToLog("IpAddress after port number removed: " + ipAddress);
      return ipAddress;
    }


    public string FindLocation(string UserIPAddress)
    {
      var MaxMindAccountID = Convert.ToInt32(WebConfigurationManager.AppSettings["MaxMind:AccountID"]);
      var MaxMindClientKey = WebConfigurationManager.AppSettings["MaxMind:ClientKey"];

      using (var client = new WebServiceClient(MaxMindAccountID, MaxMindClientKey))
      {
        try
        {
          var response = client.Country(UserIPAddress);
          ToLog("Country from Maxmind " + response.Continent.Code);
          return response.Continent.Code;
        }
        catch (Exception ex)
        {
          ToLog("Error from Maxmind " + ex.Message);
          return ex.Message;
        }

      }
    }

    internal static void ToLog(string message)
    {
      if (WebConfigurationManager.AppSettings["MaxMind:Logging"] == "0")
      {
        return;
      }

      Log.Write(message);
    }

    static int EnableGeoIPRedirect;

    private UrlLocalizationService UrlService;
  }


}
