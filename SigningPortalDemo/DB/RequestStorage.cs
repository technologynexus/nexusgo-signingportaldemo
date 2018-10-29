using LiteDB;
using SigningPortalDemo.Models;
using SigningPortalDemo.SignApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SigningPortalDemo.DB
{
    public class RequestStorage
    {
        public static void Upsert(RequestInfo requestInfo)
        {
            using (var db = GetDatabase())
            {
                var requests = db.GetCollection<RequestInfo>("requestInfo");
                requests.Upsert(requestInfo);
            }
        }

        public static RequestInfo[] FindAll()
        {
            using (var db = GetDatabase())
            {
                var requests = db.GetCollection<RequestInfo>("requestInfo");
                return requests.FindAll().ToArray();
            }
        }

        public static RequestInfo FindById(string id)
        {
            using (var db = GetDatabase())
            {
                var requests = db.GetCollection<RequestInfo>("requestInfo");
                return requests.FindById(id);
            }
        }

        public static int Count()
        {
            using (var db = GetDatabase())
            {
                var requests = db.GetCollection<RequestInfo>("requestInfo");
                return requests.Count();
            }
        }

        public static void Drop()
        {
            using (var db = GetDatabase())
            {
                db.DropCollection("requestInfo");
            }
        }

        private static LiteDatabase GetDatabase()
        {
            return new LiteDatabase(@"demo.db");
        }
    }
}
