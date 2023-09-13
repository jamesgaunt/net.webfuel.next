using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webfuel
{
    public partial class EventLog
    {
        public Guid Id  { get; set; } = Guid.Empty;
        public int Level  { get; set; } = 0;
        public string Message  { get; set; } = String.Empty;
        public string Source  { get; set; } = String.Empty;
        public string Detail  { get; set; } = String.Empty;
        public string Context  { get; set; } = String.Empty;
        public Guid? EntityId  { get; set; } = null;
        public Guid? TenantId  { get; set; } = null;
        public string IdentityId  { get; set; } = String.Empty;
        public string IPAddress  { get; set; } = String.Empty;
        public string Exception  { get; set; } = String.Empty;
        public string RequestUrl  { get; set; } = String.Empty;
        public string RequestMethod  { get; set; } = String.Empty;
        public string RequestHeaders  { get; set; } = String.Empty;
        public EventLog Copy()
        {
            var entity = new EventLog();
            entity.Id = Id;
            entity.Level = Level;
            entity.Message = Message;
            entity.Source = Source;
            entity.Detail = Detail;
            entity.Context = Context;
            entity.EntityId = EntityId;
            entity.TenantId = TenantId;
            entity.IdentityId = IdentityId;
            entity.IPAddress = IPAddress;
            entity.Exception = Exception;
            entity.RequestUrl = RequestUrl;
            entity.RequestMethod = RequestMethod;
            entity.RequestHeaders = RequestHeaders;
            return entity;
        }
    }
}

