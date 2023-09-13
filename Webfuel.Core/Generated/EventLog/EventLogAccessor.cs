using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webfuel
{
    internal class EventLogRepositoryAccessor: IRepositoryAccessor<EventLog>
    {
        public string DatabaseSchema => "next";
        public string DatabaseTable => "EventLog";
        public string DefaultOrderBy => "ORDER BY Id DESC";
        public object? GetValue(EventLog entity, string property)
        {
            switch(property)
            {
                case nameof(EventLog.Id):
                    return entity.Id;
                case nameof(EventLog.Level):
                    return entity.Level;
                case nameof(EventLog.Message):
                    return entity.Message;
                case nameof(EventLog.Source):
                    return entity.Source;
                case nameof(EventLog.Detail):
                    return entity.Detail;
                case nameof(EventLog.Context):
                    return entity.Context;
                case nameof(EventLog.EntityId):
                    return entity.EntityId;
                case nameof(EventLog.TenantId):
                    return entity.TenantId;
                case nameof(EventLog.IdentityId):
                    return entity.IdentityId;
                case nameof(EventLog.IPAddress):
                    return entity.IPAddress;
                case nameof(EventLog.Exception):
                    return entity.Exception;
                case nameof(EventLog.RequestUrl):
                    return entity.RequestUrl;
                case nameof(EventLog.RequestMethod):
                    return entity.RequestMethod;
                case nameof(EventLog.RequestHeaders):
                    return entity.RequestHeaders;
                    default: throw new InvalidOperationException($"Unrecognised entity property {property}");
            }
        }
        public void SetValue(EventLog entity, string property, object? value)
        {
            switch(property)
            {
                case nameof(EventLog.Id):
                    entity.Id = (Guid)value!;
                    break;
                case nameof(EventLog.Level):
                    entity.Level = (int)value!;
                    break;
                case nameof(EventLog.Message):
                    entity.Message = (string)value!;
                    break;
                case nameof(EventLog.Source):
                    entity.Source = (string)value!;
                    break;
                case nameof(EventLog.Detail):
                    entity.Detail = (string)value!;
                    break;
                case nameof(EventLog.Context):
                    entity.Context = (string)value!;
                    break;
                case nameof(EventLog.EntityId):
                    entity.EntityId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                    break;
                case nameof(EventLog.TenantId):
                    entity.TenantId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                    break;
                case nameof(EventLog.IdentityId):
                    entity.IdentityId = (string)value!;
                    break;
                case nameof(EventLog.IPAddress):
                    entity.IPAddress = (string)value!;
                    break;
                case nameof(EventLog.Exception):
                    entity.Exception = (string)value!;
                    break;
                case nameof(EventLog.RequestUrl):
                    entity.RequestUrl = (string)value!;
                    break;
                case nameof(EventLog.RequestMethod):
                    entity.RequestMethod = (string)value!;
                    break;
                case nameof(EventLog.RequestHeaders):
                    entity.RequestHeaders = (string)value!;
                    break;
            }
        }
        public EventLog CreateInstance()
        {
            return new EventLog();
        }
        public void Validate(EventLog entity)
        {
            entity.Message = entity.Message ?? String.Empty;
            entity.Message = entity.Message.Trim();
            entity.Source = entity.Source ?? String.Empty;
            entity.Source = entity.Source.Trim();
            entity.Detail = entity.Detail ?? String.Empty;
            entity.Detail = entity.Detail.Trim();
            entity.Context = entity.Context ?? String.Empty;
            entity.Context = entity.Context.Trim();
            entity.IdentityId = entity.IdentityId ?? String.Empty;
            entity.IdentityId = entity.IdentityId.Trim();
            if(entity.IdentityId.Length > 64) throw new InvalidOperationException("IdentityId: Cannot be longer than 64 characters.");
            entity.IPAddress = entity.IPAddress ?? String.Empty;
            entity.IPAddress = entity.IPAddress.Trim();
            if(entity.IPAddress.Length > 64) throw new InvalidOperationException("IPAddress: Cannot be longer than 64 characters.");
            entity.Exception = entity.Exception ?? String.Empty;
            entity.Exception = entity.Exception.Trim();
            entity.RequestUrl = entity.RequestUrl ?? String.Empty;
            entity.RequestUrl = entity.RequestUrl.Trim();
            entity.RequestMethod = entity.RequestMethod ?? String.Empty;
            entity.RequestMethod = entity.RequestMethod.Trim();
            entity.RequestHeaders = entity.RequestHeaders ?? String.Empty;
            entity.RequestHeaders = entity.RequestHeaders.Trim();
        }
        public IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "Level";
                yield return "Message";
                yield return "Source";
                yield return "Detail";
                yield return "Context";
                yield return "EntityId";
                yield return "TenantId";
                yield return "IdentityId";
                yield return "IPAddress";
                yield return "Exception";
                yield return "RequestUrl";
                yield return "RequestMethod";
                yield return "RequestHeaders";
            }
        }
        public IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Level";
                yield return "Message";
                yield return "Source";
                yield return "Detail";
                yield return "Context";
                yield return "EntityId";
                yield return "TenantId";
                yield return "IdentityId";
                yield return "IPAddress";
                yield return "Exception";
                yield return "RequestUrl";
                yield return "RequestMethod";
                yield return "RequestHeaders";
            }
        }
    }
}

