using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JoshCodes.Web.Models.Api
{
    public enum ResolutionOptionActions
    {
        Get,
        Post,
        Put,
        Delete,
    }

    public interface IResolutionOption
    {
        string Title { get; }

        string Description { get; }

        ResolutionOptionActions Action { get; }

        Uri Endpoint { get; }

        ulong Code { get; }
    }

    public interface IAPIException
    {
        string Reason { get; }

        string Suggestion { get; }

        int HttpStatusCode { get; }

        int HttpSubStatusCode { get; }

        IEnumerable<IResolutionOption> ResolutionOptions { get; }

        string Message { get; }
    }

    public class ResolutionOption : IResolutionOption
    {
        public ResolutionOption(string title, string description, ResolutionOptionActions action, Uri endpoint, ulong code)
        {
            this.Title = title;
            this.Description = description;
            this.Action = action;
            this.Endpoint = endpoint;
            this.Code = code;
        }

        public string Title
        {
            get;
            private set;
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

        public Uri Endpoint
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

    public abstract class APIException : Exception, IAPIException
    {
        public APIException(string message)
            : base (message)
        {
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

        public abstract string Reason { get; }

        public abstract string Suggestion { get; }

        public abstract IEnumerable<IResolutionOption> ResolutionOptions { get; }
    }
}