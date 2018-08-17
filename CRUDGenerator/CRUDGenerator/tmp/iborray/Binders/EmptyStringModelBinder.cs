using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Binders
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Mvc.DefaultModelBinder" />
    public class EmptyStringModelBinder : DefaultModelBinder
    {
        /// <summary>
        /// Binds the model.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="bindingContext">The binding context.</param>
        /// <returns></returns>
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string modelName = bindingContext.ModelName;
            object objectValue =
                bindingContext.ValueProvider.GetValue(modelName);
            if(objectValue == null) {
                return "";
            }
            return base.BindModel(controllerContext, bindingContext);
        }
    }
}