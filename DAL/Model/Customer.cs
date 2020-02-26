using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    [DataContract]
    public class Customer : BaseModel
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string PersonCode { get; set; }
        [DataMember]
        public string Name { get; set; }

        [JsonIgnore]
        public List<Agreement> Agreements { get; set; }
    }
}
