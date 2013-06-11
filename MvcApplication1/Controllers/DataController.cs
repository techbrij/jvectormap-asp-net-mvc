using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcApplication1.Models;
using Newtonsoft.Json.Linq;


namespace MvcApplication1.Controllers
{
    public class DataController : ApiController
    {
       private dbEntities db = new dbEntities();
 
        // GET api/data
        public dynamic Get()
        {
            var ret = new
            {
                states = db.UnemploymentRates.Select(x=> new{ Key = x.StateCode,Value =x.Rate}).ToDictionary(x=>x.Key,x=>x.Value), 
                metro =
                    new
                    {
                        codes = db.Metros.Select(x => x.Codes).ToList(),
                        coords = db.Metros.Select(x => new List<decimal> { x.Latitude, x.Longitude }).ToList(),
                        names = db.Metros.Select(x => x.Name).ToList(),
                        population = db.Metros.Select(x => x.Population).ToList(),
                        unemployment = db.Metros.Select(x => x.Unemployment).ToList()
                    }
            }; 
            return ret;        
        }

        // GET api/data/5
        public dynamic Get(int index)
        {
            return db.Metros.ToList().Where((x,i) => i==index).FirstOrDefault();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }   
}