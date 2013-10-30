using System;
using System.Collections.Generic;
using System.Linq;

using JoshCodes.Collections;

namespace JoshCodes.Web.Models.Domain
{
    public enum ResolutionOptionActions
    {
        Fetch,
        Create,
        Update,
        Delete,
    }

    public interface IResolutionOption
    {
        string Title { get; }

        string Description { get; }

        ResolutionOptionActions Action { get; }

        object ModelObject { get; }

        ulong Code { get; }
    }

    public interface IDomainException
    {
        string Message { get; }

        string Reason { get; }

        string Suggestion { get; }

        IEnumerable<IResolutionOption> ResolutionOptions { get; }
    }

    public abstract class DomainException : Exception, IDomainException
    {
        public DomainException(string message)
            : base(message)
        {
        }

        public DomainException(string message, string reason, string suggestion)
            : base(message)
        {
            this.Reason = reason;
            this.Suggestion = suggestion;
        }

        public string Reason
        {
            get;
            protected set;
        }

        public string Suggestion
        {
            get;
            protected set;
        }

        public abstract IEnumerable<IResolutionOption> ResolutionOptions { get; }
    }

    public class ResolutionOption : IResolutionOption
    {
        public ResolutionOption(string description, ResolutionOptionActions action, object modelObject, ulong code)
        {
            this.Description = description;
            this.Action = action;
            this.ModelObject = modelObject;
            this.Code = code;
        }

        public string Title
        {
            get;
            protected set;
        }

        public string Description
        {
            get;
            private set;
        }

        public ResolutionOptionActions Action
        {
            get;
            private set;
        }

        public Object ModelObject
        {
            get;
            private set;
        }

        public ulong Code
        {
            get;
            private set;
        }
    }

    public class StandardDomainException : Exception, IDomainException
    {
        public StandardDomainException(Exception ex)
            : base(ex.Message, ex)
        {
            var reasons = ex.Data.Keys.Select((key) => String.Format("{0} => {1}", key, ex.Data[key]));
            Reason = String.Join(",", reasons);
        }

        public string Reason
        {
            get;
            protected set;
        }

        public string Suggestion
        {
            get;
            protected set;
        }

        public IEnumerable<IResolutionOption> ResolutionOptions
        {
            get
            {
                yield break;
            }
        }
    }
}
