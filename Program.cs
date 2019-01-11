using System;
using System.IO;
using ProtoBuf;
using ProtoDatabase;

namespace productDBTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new Database();
            var retailDB = Serializer.Deserialize<ProductInstall>(File.OpenRead(@"C:\Program Files (x86)\World of Warcraft\.product.db"));
            var ptrDB = Serializer.Deserialize<ProductInstall>(File.OpenRead(@"C:\Program Files (x86)\World of Warcraft Public Test\.product.db"));
            ptrDB.Settings.InstallPath = retailDB.Settings.InstallPath;

            db.productInstalls.Add(retailDB);
            db.productInstalls.Add(ptrDB);

            using (var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, db);
                File.WriteAllBytes(".product.db.movemetowowfolder", stream.ToArray());
            }
        }
    }
}
