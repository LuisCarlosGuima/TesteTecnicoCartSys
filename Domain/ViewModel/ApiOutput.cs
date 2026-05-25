using Domain.Enumerators;

public sealed class ApiOutput<T>
{

    public List<string> Errors { get; set; } = new List<string>();
    public List<string> Warnings { get; set; } = new List<string>();
    public List<string> Infos { get; set; } = new List<string>();
    public T Data { get; set; }

    public bool HasNotifications(ENotificationLevel? notificationLevel = null)
    {
        if (!notificationLevel.HasValue)
        {
            if (notificationLevel.Value == ENotificationLevel.Error)
            {
                return Errors != null && Errors.Any();
            }
            else if (notificationLevel.Value == ENotificationLevel.Warning)
            {
                return Warnings != null && Warnings.Any();
            }
            else if (notificationLevel.Value == ENotificationLevel.Info)
            {
                return Infos != null && Infos.Any();
            }
        }

        return (Errors != null && Errors.Any()) || (Warnings != null && Warnings.Any()) || (Infos != null && Infos.Any());
    }

    public bool HasData()
    {
        return (Data != null);
    }
}