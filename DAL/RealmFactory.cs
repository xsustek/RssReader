using Realms;

namespace DAL
{
    public class RealmFactory
    {
        private static readonly RealmConfigurationBase Configuration = RealmConfiguration.DefaultConfiguration; 
        public Realm Realm => Realm.GetInstance(Configuration);
        public void Reset() => Realm.DeleteRealm(Configuration);
    }
}