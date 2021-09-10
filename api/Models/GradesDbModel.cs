using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace api.Tests.Model
{
    [Serializable]
    public class GradesDbModel
    {
        public ObjectId Id { get; set; }
        public double student_id { get; set; }
        public BsonArray scores { get; set; }
        public BsonDouble class_id { get; set; }

        public class ApiModel
        {
            public string Id { get; set; }
            public double student_id { get; set; }
            public List<BsonValue> scores { get; set; }
            public double class_id { get; set; }
        }

        internal ApiModel ToApiModel()
        {
            return new ApiModel { class_id = class_id.ToDouble(), student_id=student_id, Id = Id.ToString()};
        }
    }
}
