using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using prueba_newshore.ValueObjects;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace prueba_newshore.Controllers{
    public class FlightController : Controller
    {
        private const string StatusCodeOk = "OK";

        public ActionResult Flights()
        {
            ViewBag.Message = "Your flights page.";
            return View();
        }

        public JsonResult GetFlights(string Origen, string Destino, DateTime FechaVuelo)
        {
            try
            {
                RestClient restClient = new RestClient("http://testapi.vivaair.com/otatest/api/values");

                JObject jObject = new JObject {
                    { "Origin", Origen },
                    { "Destination", Destino },
                    { "From", FechaVuelo }
                };

                RestRequest restRequest = new RestRequest("/register", Method.POST);

                restRequest.AddParameter("application/json", jObject, ParameterType.RequestBody);

                IRestResponse respuesta = restClient.Execute(restRequest);
                if (respuesta.StatusCode.ToString() == StatusCodeOk)
                {
                    string respuestaFormato = FixApiResponseString(respuesta.Content);

                    IEnumerable<FlightJson> flights = JsonConvert.DeserializeObject<IEnumerable<FlightJson>>(respuestaFormato);

                    return Json(flights, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    throw new Exception("Ocurrió un error al realizar la búsqueda (" + respuesta.Content + ")");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveFlight(string Vuelo, string Origen, string Destino, string Precio, string Moneda, string Fecha )
        {         

            SqlConnection conexion = new SqlConnection("Data Source=SQL5066.site4now.net;Initial Catalog=DB_A6E4B3_dbflights;User Id=DB_A6E4B3_dbflights_admin;Password=dbnewshore2021");
            conexion.Open();
            string insertTransport = "insert into Transport (FlightNumber) values (@FlightNumber)";                                               

            SqlCommand comandoTransport = new SqlCommand(insertTransport, conexion);
            comandoTransport.Parameters.AddWithValue("@FlightNumber", Vuelo);
            comandoTransport.ExecuteNonQuery();

            SqlCommand commandMaxTransport = new SqlCommand("SELECT max(id) FROM Transport", conexion);
            int maxTransportId = Convert.ToInt32(commandMaxTransport.ExecuteScalar());

            string insertFlight = "insert into Flight (DepartureStation, ArrivalStation, DepartureDate, Price, Currency ,TransportId) values (@DepartureStation, @ArrivalStation, @DepartureDate, @Price, @Currency , @TransportId)";
            SqlCommand comandoFlight = new SqlCommand(insertFlight, conexion);            
            comandoFlight.Parameters.AddWithValue("@DepartureStation", Origen);
            comandoFlight.Parameters.AddWithValue("@ArrivalStation", Destino);
            comandoFlight.Parameters.AddWithValue("@Price", Precio);
            comandoFlight.Parameters.AddWithValue("@Currency", Moneda);
            comandoFlight.Parameters.AddWithValue("@DepartureDate", Convert.ToDateTime(Fecha));
            comandoFlight.Parameters.AddWithValue("@TransportId", maxTransportId);
            comandoFlight.ExecuteNonQuery();

            conexion.Close();

        }

        private static string FixApiResponseString(string input)
        {
            input = input.Replace("\\", string.Empty);
            input = input.Trim('"');
            return input;
        }
    }
}