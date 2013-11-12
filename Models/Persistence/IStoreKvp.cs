using System;

namespace JoshCodes.Web.Models.Persistence
{
    public interface IStoreKvp : IStoreObjects<IDefineKvp>
    {
        string Get(string container, string key);

        void Create(string container, string key, string value);
    }
}
