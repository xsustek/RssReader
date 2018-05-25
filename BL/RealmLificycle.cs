using System;
using DAL;

namespace BL
{
    public class RealmLificycle
    {
        public static void Create() => RealmProvider.CreateRealm();
        public static void Close() => RealmProvider.CloseRealm();
    }
}
