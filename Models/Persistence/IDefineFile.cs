using System;
using System.IO;

using JoshCodes.Web.Models.Persistence;

namespace JoshCodes.Web.Models.Persistence
{
    public interface IDefineFile : IDefineModelObject
    {
        void WriteFile(Stream outputStream, out string responseType, out string filename);
    }
}
