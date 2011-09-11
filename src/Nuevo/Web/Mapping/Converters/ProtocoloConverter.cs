namespace Web.Mapping.Converters
{
    using AutoMapper;

    using BusinessEntity;

    using Web.Models;

    public class ProtocoloConverter : AbstractConverter, ITypeConverter<ProtocoloForm, Protocolo>
    {
        public Protocolo Convert(ResolutionContext context)
        {
            return this.MapAbstractModel(context) as Protocolo;
        }
    }

    public class ProtocoloFormConverter : ITypeConverter<Protocolo, ProtocoloForm>
    {
        public ProtocoloForm Convert(ResolutionContext context)
        {
            var protocoloForm = Mapper.DynamicMap<ProtocoloForm>(context.SourceValue);
            return protocoloForm;
        }
    }
}