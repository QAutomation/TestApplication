using api.Tests.Model;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace api.Controllers
{
    public class GradesController : ApiController
    {
        private const string DB_NAME = "sample_training";
        private const string GRADES_COLL_NAME = "grades";
        public static IEnumerable<GradesDbModel.ApiModel> Grades { get; set; } = 
            GetFromDb<IEnumerable<GradesDbModel.ApiModel>, GradesDbModel>(
                ConfigurationManager.AppSettings["connectionString"].ToString(),
                DB_NAME,
                GRADES_COLL_NAME,
                0,
                0,
                true
            );

        // GET api/values
        public IEnumerable<GradesDbModel.ApiModel> Get(int pageNumber, int pageSize)
        {
            var startIndex = (pageNumber * pageSize) - pageSize;
            return Grades.ToList().GetRange(startIndex, pageSize);      
        }

        private static T GetFromDb<T, T1>(string uri, string dbName, string collectionName, int from, int to, bool getAll = false)
            where T : IEnumerable<GradesDbModel.ApiModel>
            where T1 : GradesDbModel
        {
            var settings = MongoClientSettings.FromConnectionString(uri);
            var client = new MongoClient(settings);
            var database = client.GetDatabase(DB_NAME);
            IMongoCollection<T1> list = database.GetCollection<T1>(GRADES_COLL_NAME);
            var res = list.Find(FilterDefinition<T1>.Empty);
            List<T1> obj = res.ToList();
            return getAll ? (T)obj.Select(x => x.ToApiModel()) : (T)obj.GetRange(from, to).Select(x => x.ToApiModel());
        }

        //// GET api/values/5
        //public string Get(int id)
        //{
        //    return "{\"name\":\"Andrii\"}";
        //}

        //// POST api/values
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }
}
