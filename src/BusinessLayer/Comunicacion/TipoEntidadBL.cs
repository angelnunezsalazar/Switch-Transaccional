using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;

namespace BusinessLayer.Comunicacion
{
    using System.Linq;

    using DataAccess.Services;
    using DataAccess.Aspects;

    [DataObject(true)]
    [ExceptionHandling]
    public class TipoEntidadBL : Service<TipoEntidad>
    {

    }
}
