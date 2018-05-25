using System;
using System.Threading.Tasks;
using Realms;

namespace DAL
{
    public static class RealmProvider
    {
        private static Realm realm;
        public static void CreateRealm() => realm = Realm.GetInstance();
        public static void CloseRealm() => realm.Dispose();

        public static void Do(Action<Realm> func)
        {
            func(realm);
        }

        public static T Do<T>(Func<Realm, T> func)
        {
            return func(realm);
        }

        public static async Task<T> Do<T>(Func<Realm, Task<T>> func)
        {
            return await func(realm);
        }

        public static async Task Do(Func<Realm, Task> func)
        {
            await func(realm);
        }
    }
}