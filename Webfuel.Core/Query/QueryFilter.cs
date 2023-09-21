using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel
{
    [JsonConverter(typeof(QueryFilterConverter))]
    public class QueryFilter
    {
        public QueryFilter() { }

        public QueryFilter(string field, string op, object? value)
        {
            Field = field;
            Op = op;
            Value = value;
        }

        [ApiOptional]
        public string Field { get; set; } = String.Empty;

        public string Op { get; set; } = QueryOp.None;

        [ApiOptional]
        public object? Value { get; set; }

        [ApiOptional]
        public List<QueryFilter>? Filters { get; set; }
    }

    public class QueryFilterConverter : JsonConverter<QueryFilter>
    {
        public override QueryFilter? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException("QueryFilterConverter: Expected StartObject");

            var filter = new QueryFilter();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }
                else if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var name = reader.GetString();

                    if (String.Compare("Field", name, options.PropertyNameCaseInsensitive) == 0)
                    {
                        reader.Read();
                        filter.Field = reader.GetString() ?? String.Empty;
                    }
                    else if (String.Compare("Op", name, options.PropertyNameCaseInsensitive) == 0)
                    {
                        reader.Read();
                        filter.Op = reader.GetString() ?? QueryOp.None;
                    }
                    else if (String.Compare("Value", name, options.PropertyNameCaseInsensitive) == 0)
                    {
                        reader.Read();

                        if (reader.TokenType == JsonTokenType.Null)
                        {
                            filter.Value = null;
                        }
                        else if (reader.TokenType == JsonTokenType.String)
                        {
                            if (reader.TryGetGuid(out Guid guidValue))
                                filter.Value = guidValue;
                            else
                                filter.Value = reader.GetString();
                        }
                        else
                        {
                            throw new JsonException("QueryFilterConverter: Invalid Value");
                        }
                    }
                    else if (String.Compare("Filters", name, options.PropertyNameCaseInsensitive) == 0)
                    {
                        reader.Read();

                        if(reader.TokenType == JsonTokenType.Null)
                        {
                            filter.Filters = null;
                        }
                        else if(reader.TokenType == JsonTokenType.StartArray)
                        {
                            var filters = new List<QueryFilter>();
                            while (reader.Read())
                            {
                                if (reader.TokenType == JsonTokenType.EndArray)
                                    break;
                                filters.Add(Read(ref reader, typeToConvert, options) ?? throw new JsonException());
                            }
                            filter.Filters = filters;
                        }
                        else
                        {
                            throw new JsonException("QueryFilterConverter: Expected Array or Null");
                        }
                    }
                    else
                    {
                        throw new JsonException("QueryFilterConverter: Expected Property");
                    }
                }
            }
            return filter;
        }

        public override void Write(Utf8JsonWriter writer, QueryFilter value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
