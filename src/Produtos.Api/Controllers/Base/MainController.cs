using Microsoft.AspNetCore.Mvc;
using Produtos.Domain.Notifications;
using Produtos.Domain.Interfaces.Notifications;

namespace Produtos.Api.Controllers.Base
{
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("/api/[controller]")]
    public class MainController : ControllerBase
    {
        protected IHandler<DomainNotification> Notifications { get; }

        public MainController(IHandler<DomainNotification> notifications)
        {
            Notifications = notifications;
        }

        protected bool IsValidOperation() => !Notifications.HasNotifications();

        protected IActionResult ResponseProcessed()
        {
            if (IsValidOperation())
                return Ok();

            return ResponseBadRequest();
        }

        protected ActionResult ResponsePutPatch()
        {
            if (IsValidOperation())
                return NoContent();

            return ResponseBadRequest();
        }

        protected ActionResult ResponseAccepted()
        {
            if (IsValidOperation())
                return Accepted();

            return ResponseBadRequest();
        }

        protected ActionResult ResponseDelete()
        {
            if (IsValidOperation())
                return NoContent();

            return ResponseBadRequest();
        }

        protected ActionResult ResponseBadRequest()
        {
            return BadRequest(new ValidationProblemDetails(Notifications.GetNotificationsByKey()));
        }

        protected ActionResult<T> ResponsePost<T>(string action, object route, T result)
        {
            if (IsValidOperation())
            {
                if (result is null)
                    return NoContent();

                return CreatedAtAction(action, route, result);
            }

            return ResponseBadRequest();
        }

        protected ActionResult<T> ResponsePost<T>(string action, string controller, object route, T result)
        {
            if (IsValidOperation())
            {
                if (result is null)
                    return NoContent();

                return CreatedAtAction(action, controller, route, result);
            }

            return ResponseBadRequest();
        }
        protected ActionResult<IEnumerable<T>> ResponseGet<T>(IEnumerable<T> result)
        {
            if (IsValidOperation())
            {
                if (result is null)
                    return NoContent();

                return Ok(result);
            }

            return ResponseBadRequest();
        }

        protected ActionResult<T> ResponseGet<T>(T result)
        {
            if (IsValidOperation())
            {
                if (result is null)
                    return NotFound();

                return Ok(result);
            }

            return ResponseBadRequest();
        }

        
        protected void NotifyModelStateErrors()
        {
            var result = from ms in ModelState
                         where ms.Value.Errors.Any()
                         let fieldKey = ms.Key
                         let errors = ms.Value.Errors
                         from error in errors
                         select new
                         {
                             code = fieldKey.Replace("ENTITY.", "", StringComparison.InvariantCultureIgnoreCase)
                            .Replace("VIEWMODEL.", "", StringComparison.InvariantCultureIgnoreCase)
                            .Replace("MODEL.", "", StringComparison.InvariantCultureIgnoreCase)
                            .Replace("$.", "", StringComparison.InvariantCultureIgnoreCase),
                             message = error.Exception is null ? error.ErrorMessage : error.Exception.Message
                         };

            result.ToList().ForEach(x =>
            {
                Notifications.Handle(DomainNotification.ModelValidation(x.code, x.message));
            });
        }

        protected void NotifyError(string code, string message)
        {
            Notifications.Handle(DomainNotification.Error(code, message));
        }

        protected ActionResult ModelStateErrorResponseError()
        {
            return BadRequest(new ValidationProblemDetails(ModelState));
        }
    }
}
