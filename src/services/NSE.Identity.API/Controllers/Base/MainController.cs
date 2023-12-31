﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NSE.Identity.API.Controllers.Base
{
    [ApiController]
    public abstract class MainController : Controller
    {
        protected ICollection<string> Errors = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            if (IsValid()) return Ok(result);

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Messages", Errors.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(erro => erro.Errors);

            foreach (var error in errors) AddProcessingError(error.ErrorMessage);

            return CustomResponse();
        }

        protected bool IsValid() => !Errors.Any();

        protected void AddProcessingError(string error) => Errors.Add(error);

        protected void CleanProcessingErrors() => Errors.Clear();
    }
}
