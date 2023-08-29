using System.Diagnostics;

namespace User.Domain.Extensions;

public static class ActivityUserExtension
{    
    public static void SetUser(this Activity activity,Guid guid)
    {
        activity.SetCustomProperty("IdUser", guid);
    }

    public static Guid? GetUser(this Activity activity)
    {
        return activity.GetCustomProperty("IdUser") as Guid?;
    }
}
