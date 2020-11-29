namespace Bizca.Core.Domain
{
    using System;
    public sealed class RuleResult
    {
        public bool Sucess { get; }
        public string Message { get; }
        public Type ExceptionType { get; set; }
        public RuleResult(bool sucess, string message, Type type)
        {
            Sucess = sucess;
            Message = message;
            ExceptionType = type;
        }
    }
}
