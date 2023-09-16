using FluentValidation;

namespace Webfuel
{
    internal class EventLogRepositoryAccessor: IRepositoryAccessor<EventLog>
    {
        private readonly EventLogRepositoryValidator _validator = new EventLogRepositoryValidator();
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
            _validator.ValidateAndThrow(entity);
        }
        public IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "Level";
                yield return "Message";
            }
        }
        public IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Level";
                yield return "Message";
            }
        }
    }
}

