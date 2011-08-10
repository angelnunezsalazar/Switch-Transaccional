namespace Web.Binders
{
    using System.Web.Mvc;
    using System;

    using BusinessEntity;

    public class CustomBinder:DefaultModelBinder
    {
        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            if (modelType.IsInterface || modelType.IsAbstract)
            {
                var bindingType = bindingContext.ValueProvider.GetValue("bindingType");
                if (bindingType!=null)
                {
                    var assemblyQualifiedName = modelType.AssemblyQualifiedName;
                    var baseFullName = modelType.FullName;
                    var childtype = ((string[])bindingType.RawValue)[0];
                    modelType = Type.GetType(assemblyQualifiedName.Replace(baseFullName,baseFullName+childtype));
                }
            }

            var model = base.CreateModel(controllerContext, bindingContext, modelType);
            return model;
        }
    }
}