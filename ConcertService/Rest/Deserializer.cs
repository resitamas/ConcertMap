using RestSharp.Deserializers;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertService.Rest
{
    public class Deserializer
    {

        private static JsonDeserializer deserializer;

        private Deserializer()
        {

        }

        public static JsonDeserializer Instance
        {
            get
            {
                if (deserializer == null)
                {
                    deserializer = new JsonDeserializer();
                }

                return deserializer;
            }
        }

    }
}
