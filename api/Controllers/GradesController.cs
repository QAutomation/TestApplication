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
        private const string DbName = "sample_training";
        private const string CollectionName = "grades";

        // GET api/values
        public IEnumerable<GradesDbModel.ApiModel> Get(int pageNumber, int pageSize=10)
        {
            var startIndex = (pageNumber * pageSize)-pageSize;
            var uri = ConfigurationManager.AppSettings["connectionString"].ToString();
            return GetFromDb<IEnumerable<GradesDbModel.ApiModel>,GradesDbModel>(uri,DbName,CollectionName, startIndex, startIndex+pageSize);
        }

        private static T GetFromDb<T,T1>(string uri, string dbName, string collectionName, int from, int to) 
            where T : IEnumerable<GradesDbModel.ApiModel> 
            where T1 : GradesDbModel
        {
            var settings = MongoClientSettings.FromConnectionString(uri);
            var client = new MongoClient(settings);
            var database = client.GetDatabase(DbName);
            IMongoCollection<T1> list = database.GetCollection<T1>(CollectionName);
            var res = list.Find(FilterDefinition<T1>.Empty);
            List<T1> obj = res.ToList();
            var resList = obj.GetRange(from, to).Select(x => x.ToApiModel());
            return (T)resList;
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
