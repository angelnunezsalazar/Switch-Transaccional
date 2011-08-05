namespace DataAccess.Aspects
{
    using System;

    using DataAccess.Persistence;

    using PostSharp.Aspects;
    using StructureMap;

    [Serializable]
    public class TransactionAttribute : OnMethodBoundaryAspect
    {
        public override void OnExit(MethodExecutionArgs args)
        {
            ObjectFactory.GetInstance<DatabaseContext>().SaveChanges();
        }
    }
}