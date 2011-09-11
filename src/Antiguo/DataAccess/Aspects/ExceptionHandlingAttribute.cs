namespace DataAccess.Aspects
{
    using PostSharp.Aspects;
    using DataAccess.Errors;

    using StructureMap;
    using System;

    [Serializable]
    public class ExceptionHandlingAttribute : OnExceptionAspect
    {
        public override void OnException(MethodExecutionArgs args)
        {
            ObjectFactory.GetInstance<IErrorStorage>().Save(args.Exception.Message);
            args.FlowBehavior = FlowBehavior.RethrowException;
        }
    }
}