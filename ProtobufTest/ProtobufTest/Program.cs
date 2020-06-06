using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;

namespace ProtobufTest
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var dcontext = new DynamicContext
            {
                length = 100,
                number = 99
            };
            
            var data = new ObjectMS
            {
                tick = 16,
                ign = "hello",
                myclass = dcontext
            };

            byte[] msg = Serialize(data);
            var response = Deserialize<ObjectMS>(msg);
            Console.WriteLine(response.tick + " " + response.ign + " " + response.myclass.length + " " + response.myclass.number);

        }
        
        public static byte[] Serialize<T>(T tData) {
            using (var ms = new MemoryStream()) {
                ProtoBuf.Serializer.Serialize(ms, tData);
                return ms.ToArray();
            }            
        }

        public static T Deserialize<T>(byte[] tData) {
            using (var ms = new MemoryStream(tData)) {
                return ProtoBuf.Serializer.Deserialize<T>(ms);
            }
        }
        
    }
}