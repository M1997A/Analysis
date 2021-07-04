using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analysis.Infrastructure
{
    public class ModelStateTransfer :ActionFilterAttribute
    {
        protected const string Key = nameof(ModelStateTransfer);
    }
    public class ExportModelStateAttribute : ModelStateTransfer
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.ModelState.IsValid)
            {
                if(context.Result is RedirectResult ||
                    context.Result is RedirectToRouteResult ||
                    context.Result is RedirectToActionResult)
                {
                    var controller = context.Controller as Controller;
                    if(controller != null && context.ModelState != null)
                    {
                        var modelState = ModelStateHelpers.SerializeModelState(context.ModelState);
                        controller.TempData[Key] = modelState;
                    }
                }
            }
            base.OnActionExecuted(context);
        }
        
    }
    public class ImportModelSatateAttribute : ModelStateTransfer
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = context.Controller as Controller;
            var serializedModelSatet = controller?.TempData[Key] as string;
            if(serializedModelSatet != null)
            {
                if(context.Result is ViewResult)
                {
                    var modelSatet = ModelStateHelpers.DeserializeModelState(serializedModelSatet);
                    context.ModelState.Merge(modelSatet);
                }
                else
                {
                    controller.TempData.Remove(Key);
                }
            }
            base.OnActionExecuted(context);
        }
    }
}
