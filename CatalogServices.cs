using System;
using System.Threading.Tasks;
using System.Json;
using System.Net;
using System.IO;
using d = DRGShared;

namespace MacysAPIs
{
	public static class CatalogServices
	{


		public static async Task<JsonValue> GetCategoryIndex()
		{
			string strUri = "http://api.macys.com/v3/catalog/category/index?category=118&depth=1";
			WebResponse wrResponse;
			try
			{
				// Create an HTTP web request using the URL:
				HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(strUri));
				request.ContentType = "Accept: application/json" +
				d.Strings.NewLine() +
				"X-Macys-Webservice-Client-Id: mcomhackdays2015" +
				d.Strings.NewLine() +
				"X-Originating-Ip: 206.169.185.30";

				request.Method = "GET";

				// Send the request to the server and wait for the response:
				using (wrResponse = await request.GetResponseAsync())
				{
					// Get a stream representation of the HTTP web response:
					using (Stream stream = wrResponse.GetResponseStream())
					{
						// Use this stream to build a JSON document object:
						JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
						Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

						// Return the JSON document:
						return jsonDoc;
					}
				}
			} catch(Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return null;
			}
		}



		public static async Task<JsonValue> FetchWeatherAsync()
		{
			// Create an HTTP web request using the URL:
			string strUrl = "http://api.geonames.org/findNearByWeatherJSON?lat=" +
			                "37.7833" +
			                "&lng=" +
			                "122.4167" +
			                "&username=demo";
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(strUrl));
			request.ContentType = "application/json";
			request.Method = "GET";

			// Send the request to the server and wait for the response:
			using (WebResponse response = await request.GetResponseAsync())
			{
				// Get a stream representation of the HTTP web response:
				using (Stream stream = response.GetResponseStream())
				{
					// Use this stream to build a JSON document object:
					JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
					Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

					// Return the JSON document:
					return jsonDoc;
				}
			}
		}






		//Example of Parse and Display
		private static void ParseAndDisplay(JsonValue json)
		{
			/*
			// Get the weather reporting fields from the layout resource:
			TextView location = FindViewById<TextView>(Resource.Id.locationText);
			TextView temperature = FindViewById<TextView>(Resource.Id.tempText);
			TextView humidity = FindViewById<TextView>(Resource.Id.humidText);
			TextView conditions = FindViewById<TextView>(Resource.Id.condText);

			// Extract the array of name/value results for the field name "weatherObservation". 
			JsonValue weatherResults = json ["weatherObservation"];

			// Extract the "stationName" (location string) and write it to the location TextBox:
			location.Text = weatherResults ["stationName"];

			// The temperature is expressed in Celsius:
			double temp = weatherResults ["temperature"];
			// Convert it to Fahrenheit:
			temp = ((9.0 / 5.0) * temp) + 32;
			// Write the temperature (one decimal place) to the temperature TextBox:
			temperature.Text = String.Format("{0:F1}", temp) + "° F";

			// Get the percent humidity and write it to the humidity TextBox:
			double humidPercent = weatherResults ["humidity"];
			humidity.Text = humidPercent.ToString() + "%";

			// Get the "clouds" and "weatherConditions" strings and 
			// combine them. Ignore strings that are reported as "n/a":
			string cloudy = weatherResults ["clouds"];
			if (cloudy.Equals("n/a"))
				cloudy = "";
			string cond = weatherResults ["weatherCondition"];
			if (cond.Equals("n/a"))
				cond = "";

			// Write the result to the conditions TextBox:
			conditions.Text = cloudy + " " + cond;
			*/
		}


	}
}

