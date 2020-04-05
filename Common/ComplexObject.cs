using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [Serializable]
    public class ComplexObject
    {
        public string Name { get; set; }
        public int Age { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }
    }

    [Serializable]
    public enum Gender
    {
        [EnumMember(Value = "Male")]
        Male,
        [EnumMember(Value = "Female")]
        Female
    }
}
