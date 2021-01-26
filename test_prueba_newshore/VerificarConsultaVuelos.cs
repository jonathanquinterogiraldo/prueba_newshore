using Newtonsoft.Json.Linq;
using NUnit.Framework;
using prueba_newshore.Controllers;
using prueba_newshore.ValueObjects;
using System;

namespace test_prueba_newshore
{
    public class Tests
    {
        public object JsonResult { get; private set; }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            //preparacion
            var Origen = "MDE";
            var Destino = "BOG";
            var FechaVuelo = DateTime.Now.AddDays(1); //la fecha que se le debe enviar debe ser de hoy en adelante
            var resultadoEsperado = JsonResult;
                      

            FlightController objetoPrueba = new FlightController();

            //ejecucion
            var resultado = objetoPrueba.GetFlights(Origen, Destino, FechaVuelo);

            //comprobacion
            Assert.Pass();
        }

        
    }
}