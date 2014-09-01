
namespace JoshCodes.Web.Models.Persistence
{
    public interface IDefineKvp : IDefineModelObject
    {
        string Container { get; }
        string LookupKey { get; }
        string Value { get; }
    }
}
