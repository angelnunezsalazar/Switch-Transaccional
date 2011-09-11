namespace Web.Mapping.Converters
{
    using System;

    using AutoMapper;

    using Web.Models;

    public class AbstractConverter
    {
        protected object MapAbstractModel(ResolutionContext context)
        {
            var assemblyQualifiedName = context.DestinationType.AssemblyQualifiedName;
            var baseFullName = context.DestinationType.FullName;
            var childtype = ((AbstractForm)context.SourceValue).BindingType;
            var destinationType = Type.GetType(assemblyQualifiedName.Replace(baseFullName, baseFullName + childtype));
            return Mapper.Map(context.SourceValue, context.SourceType, destinationType);
        }
    }
}