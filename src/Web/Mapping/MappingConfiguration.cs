namespace Web.Mapping
{
    using System;

    using BusinessEntity;

    using AutoMapper;

    using Web.Mapping.Converters;
    using Web.Models;

    public class MappingConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<string, int>().ConvertUsing(Convert.ToInt32);

            Mapper.CreateMap<Protocolo, ProtocoloForm>().ConvertUsing<ProtocoloFormConverter>();
            Mapper.CreateMap<ProtocoloTCP, ProtocoloForm>();
            Mapper.CreateMap<ProtocoloComponente, ProtocoloForm>();

            Mapper.CreateMap<ProtocoloForm, Protocolo>().ConvertUsing<ProtocoloConverter>();
            Mapper.CreateMap<ProtocoloForm, ProtocoloTCP>();
            Mapper.CreateMap<ProtocoloForm, ProtocoloComponente>();

            Mapper.CreateMap<CampoMaestro, Campo>();
        }
    }
}