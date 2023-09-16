using FluentValidation;

namespace Webfuel
{
    public partial class EventLog
    {
        public Guid Id  { get; set; } = Guid.Empty;
        public int Level  { get; set; } = 0;
        public string Message  { get; set; } = String.Empty;
        public EventLog Copy()
        {
            var entity = new EventLog();
            entity.Id = Id;
            entity.Level = Level;
            entity.Message = Message;
            return entity;
        }
    }
}

