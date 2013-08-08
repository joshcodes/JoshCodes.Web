using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JoshCodes.Web.Exceptions
{
    public interface IIssue
    {
        long Code { get; }
        string Message { get; }
    }

    public class Issue : IIssue
    {
        public Issue(long code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        public long Code
        {
            get;
            private set;
        }

        public string Message
        {
            get;
            private set;
        }
    }

    public enum ResolutionOptionActions
    {
        Get,
        Repost,
        Reput,
    }

    public interface IResolutionOption
    {
        string Description { get; }

        ResolutionOptionActions Action { get; }

        object DomainObject { get; }

        ulong Code { get; }
    }

    public class ResolutionOption : IResolutionOption
    {
        public ResolutionOption(string description, ResolutionOptionActions action, object domainObject, ulong code)
        {
            this.Description = description;
            this.Action = action;
            this.DomainObject = domainObject;
            this.Code = code;
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

        public Object DomainObject
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

    public interface IAPIException
    {
        IEnumerable<IIssue> Issues { get; }

        int HttpStatusCode { get; }

        int HttpSubStatusCode { get; }

        IIssue Reason { get; }

        string Suggestion { get; }

        IDictionary<string, IResolutionOption> ResolutionOptions { get; }
    }
    
    public abstract class APIException : Exception, IAPIException
    {
        public APIException(string message)
            : base (message)
        {
        }

        public APIException(long code, string message)
        {
            this.Issues = MakeIssues(code, message);
        }

        private IEnumerable<IIssue> MakeIssues(long code, string message)
        {
            yield return new Issue(code, message);
        }

        public IEnumerable<IIssue> Issues
        {
            get;
            protected set;
        }

        public int HttpStatusCode
        {
            get;
            protected set;
        }

        public int HttpSubStatusCode
        {
            get;
            protected set;
        }

        public abstract IIssue Reason { get; }

        public abstract string Suggestion { get; }

        public abstract IDictionary<string, IResolutionOption> ResolutionOptions { get; }
    }
}