using Domain.Enumerators;

namespace Service.NotificationService;

public class CustomNotification
{
    public CustomNotification(string message, ENotificationLevel notificationLevel = ENotificationLevel.Info)
    {
        NotificationLevel = notificationLevel;
        Message = message;
    }
    public ENotificationLevel NotificationLevel { get; set; }
    public string Message { get; set; } = string.Empty;
}

public interface INotifications
{
    void AddError(string message);
    void AddError(List<string> message);
    void AddError(string[] message);
    void AddNotification(string message, ENotificationLevel notificationLevel);
    void AddInfo(string message);
    void AddInfo(List<string> message);
    void AddWarning(string message);
    void AddWarning(List<string> message);
    bool HasErrorNotifications();
    bool HasWarningNotifications();
    bool HasInfoNotifications();
    bool HasNotifications();
    bool HasNotifications(ENotificationLevel eNotificationLevel);
    string ToString();
    void Clear();
    IList<string> GetNotifications(ENotificationLevel? notificationLevel = null);
}

public class Notifications : INotifications
{
    private List<CustomNotification> _notifications { get; set; } = new List<CustomNotification>();
    public void AddNotification(string message, ENotificationLevel notificationLevel)
    {
        _notifications.Add(new CustomNotification(message, notificationLevel));
    }

    public bool HasNotifications() => _notifications.Any();
    public bool HasNotifications(ENotificationLevel eNotificationLevel) => _notifications.Any(e => e.NotificationLevel.Equals(eNotificationLevel));
    public bool HasErrorNotifications() => _notifications.Where(n => n.NotificationLevel == ENotificationLevel.Error).Any();
    public bool HasWarningNotifications() => _notifications.Where(n => n.NotificationLevel == ENotificationLevel.Warning).Any();
    public bool HasInfoNotifications() => _notifications.Where(n => n.NotificationLevel == ENotificationLevel.Info).Any();
    public void AddError(string message) => _notifications.Add(new CustomNotification(message, ENotificationLevel.Error));
    public void AddError(List<string> messages)
    {
        foreach (var message in messages)
        {
            _notifications.Add(new CustomNotification(message, ENotificationLevel.Error));
        }
    }
    public void AddError(string[] messages)
    {
        foreach (var message in messages)
        {
            _notifications.Add(new CustomNotification(message, ENotificationLevel.Error));
        }
    }
    public void AddWarning(string message) => _notifications.Add(new CustomNotification(message, ENotificationLevel.Warning));
    public void AddWarning(List<string> messages)
    {
        foreach (var message in messages)
        {
            _notifications.Add(new CustomNotification(message, ENotificationLevel.Warning));
        }
    }
    public void AddInfo(string message) => _notifications.Add(new CustomNotification(message, ENotificationLevel.Info));
    public void AddInfo(List<string> messages)
    {
        foreach (var message in messages)
        {
            _notifications.Add(new CustomNotification(message, ENotificationLevel.Info));
        }
    }
    public IList<string> GetNotifications(ENotificationLevel? notificationLevel = null)
    {
        if (notificationLevel == null)
        {
            return _notifications.Select(d => d.Message).ToList();
        }
        else
        {
            return _notifications.Where(d => d.NotificationLevel == notificationLevel).Select(d => d.Message).ToList() ?? new List<string>();
        }
    }

    public override string ToString()
    {
        return string.Join(", ", _notifications);
    }

    public void Clear()
    {
        _notifications.Clear();
    }
}
