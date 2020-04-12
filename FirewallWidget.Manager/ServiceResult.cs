using System.Collections.Generic;
using System.Linq;

namespace FirewallWidget.Manager
{
    public class ServiceResult<TDto>
    {
        public ServiceResult(
            TDto dto, FailureReason failureReason, params string[] errors)
        {
            DTO = dto;
            Errors = new List<string>(errors ?? new string[0]);
            FailureReason = failureReason;
        }

        public TDto DTO { get; private set; }

        public IEnumerable<string> Errors { get; private set; }

        public FailureReason FailureReason { get; private set; }

        public bool Successful => Errors.Count() == 0;

        public static ServiceResult<TDto> Success(TDto dto = default)
        { return new ServiceResult<TDto>(dto, FailureReason.None); }

        private static ServiceResult<TDto> Failure(
            FailureReason failureReason, params string[] errors)
        {
            errors = errors?.Length == 0
                ? new[] { "Unknown error" }
                : errors;
            return new ServiceResult<TDto>(default, failureReason, errors);
        }

        public static ServiceResult<TDto> BadInput(params string[] errors)
        { return Failure(FailureReason.BadInput, errors); }

        public static ServiceResult<TDto> NotFound(params string[] errors)
        { return Failure(FailureReason.NotFound, errors); }
    }

    public enum FailureReason
    {
        None,
        BadInput,
        NotFound,
        Exception,
        Other
    }
}
