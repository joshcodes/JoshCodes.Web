using System;

using JoshCodes.Web.Models.Persistence;
using JoshCodes.Web.Attributes;

namespace JoshCodes.Web.Models.Domain
{
    [UrnNamespaceIdentifier("x-storage-file")]
    public class File : ModelObject<IDefineFile>
    {
        public File(IDefineFile definition)
            : base(definition)
        {
        }

        public void WriteFile(System.IO.Stream outputStream, out string responseType, out string filename)
        {
            definition.WriteFile(outputStream, out responseType, out filename);
        }
    }
}
