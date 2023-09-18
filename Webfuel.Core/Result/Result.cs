using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public class Result<TValue, TError>
    {
        private readonly TValue? _value;
        private readonly TError? _error;

        public Result(TValue value) 
        {
            _value = value;
            _error = default;
            IsError = false;
        }

        public Result(TError error)
        {
            _value = default;
            _error = error;
            IsError = true;
        }

        public bool IsError { get; }

        public bool IsSuccess => !IsError;

        public static implicit operator Result<TValue, TError>(TValue value) => new(value);

        public static implicit operator Result<TValue, TError>(TError error) => new(error);

        public TResult Match<TResult>(
            Func<TValue, TResult> success,
            Func<TError, TResult> failure) =>
            IsError ? failure(_error!) : success(_value!);
    }
}
