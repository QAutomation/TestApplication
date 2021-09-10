using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Tests.Model
{
    [Serializable]
    public class GradesModel
    {
        public ObjectId Id { get; set; }
        public double student_id { get; set; }
        public BsonArray scores { get; set; }
        public BsonDouble class_id;
    }
}
