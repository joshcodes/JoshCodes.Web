using System.IO;


namespace JoshCodes.Web.Models.Persistence
{
    public interface IDefineFile : IDefineModelObject
    {
        void WriteFile(Stream outputStream, out string responseType, out string filename);
    }
}
