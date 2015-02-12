using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Telemetry.Services;

namespace WebApplication.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			Random rnd = new Random();
			int rndInt = rnd.Next(10);
			//if (int == 1)
			//{
			//	throw new ApplicationException("Test exception");
			//}
			string eventString = "server/" + DateTime.UtcNow.ToShortTimeString() + "/" + rndInt;
			ServerAnalytics.CurrentRequest.LogEvent(eventString);
			TelemetryClient tc = new TelemetryClient();
			tc.TrackEvent(eventString);
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}