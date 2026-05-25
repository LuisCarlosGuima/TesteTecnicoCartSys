using Domain.Enumerators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.NotificationService;
using System.Net;

namespace TesteTecnicoCartSys.ControllerExtension;

[ApiExplorerSettings(IgnoreApi = false)]
[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiOutput<Object>))]
[ApiController]
[Authorize]
public abstract class CartSysController : Controller
{
    protected readonly INotifications _notifications;

    protected CartSysController(INotifications notifications)
    {
        _notifications = notifications;
    }

    protected ActionResult CartSysResponse<T>(T objeto = default) where T : class
    {
        if (_notifications.HasErrorNotifications())
        {
            return BadRequest(new ApiOutput<T>()
            {
                Errors = (List<string>)_notifications.GetNotifications(ENotificationLevel.Error),
                Infos = (List<string>)_notifications.GetNotifications(ENotificationLevel.Info),
                Warnings = (List<string>)_notifications.GetNotifications(ENotificationLevel.Warning)
            });
        }

        return Ok(new ApiOutput<T>()
        {
            Data = objeto,
            Infos = (List<string>)_notifications.GetNotifications(ENotificationLevel.Info),
            Warnings = (List<string>)_notifications.GetNotifications(ENotificationLevel.Warning),
            Errors = (List<string>)_notifications.GetNotifications(ENotificationLevel.Error)
        });
    }
    protected ActionResult CartSysResponse(object obj = null) => CartSysResponse<object>(obj);

    // Error
    protected void AdicionarError(string message) => _notifications.AddError(message);
    protected void AdicionarError(List<string> messages) => _notifications.AddError(messages);
    protected void AddNotificationError(string message) => _notifications.AddError(message);
    protected void AddNotificationError(List<string> messages) => _notifications.AddError(messages);
    protected void AddNotificationError(params string[] messages) => _notifications.AddError(messages.ToList());

    // Warning
    protected void AdicionarWarning(string message) => _notifications.AddWarning(message);
    protected void AddNotificationWarning(string message) => _notifications.AddWarning(message);
    protected void AddNotificationWarning(List<string> messages) => _notifications.AddWarning(messages);
    protected void AddNotificationWarning(params string[] messages) => _notifications.AddWarning(messages.ToList());

    // Info
    protected void AdicionarInfo(string infoMessage) => _notifications.AddInfo(infoMessage);
    protected void AddNotificationInfo(List<string> messages) => _notifications.AddInfo(messages);
    protected void AddNotificationInfo(params string[] messages) => _notifications.AddInfo(messages.ToList());

    protected bool HasNotifications(ENotificationLevel? notificationLevel = null)
        => notificationLevel is not null ? _notifications.GetNotifications(notificationLevel).Any() : _notifications.GetNotifications().Any();

    protected string GetToken => HttpContext.Request?.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");

    protected int GetUsuarioId() { return int.Parse(User.Claims.Where(c => c.Type == "usuarioId").FirstOrDefault().Value); }

    protected int? TryGetUsuarioId()
    {
        if (int.TryParse(User.Claims.Where(c => c.Type == "usuarioId").FirstOrDefault().Value, out var _usuarioId))
        {
            return _usuarioId;
        }
        else
        {
            return null;
        }
    }

    protected int GetCartSysUserId()
    {
        return GetUsuarioId();
    }

    protected int? TryGetCartSysUserId()
    {
        return TryGetUsuarioId();
    }

    protected int GetUserId()
    {
        return int.Parse(User.Claims.FirstOrDefault(c => c.Type == "Id").Value);
    }

    protected int? TryGetUserId()
    {
        if (int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "Id").Value, out var _portalwebUsuarioId))
        {
            return _portalwebUsuarioId;
        }
        else
        {
            return null;
        }
    }

    protected string GetEmailUserId()
    {
        return User.Claims.Where(c => c.Type.ToUpper().Contains("EMAILADDRESS"))?.FirstOrDefault()?.Value ??
               User.Claims.Where(c => c.Type.ToUpper().Contains("EMAIL"))?.FirstOrDefault()?.Value;
    }

    protected List<string> GetModelStateErrors(ModelStateDictionary modelState)
    {
        var errors = new List<string>();

        foreach (var modelStateEntry in modelState.Values)
        {
            foreach (var error in modelStateEntry.Errors)
            {
                errors.Add(error.ErrorMessage);
            }
        }

        return errors;
    }
}
