using System.Diagnostics;

namespace User.Domain.Extensions;

public static class ActivityCurrentUserExtension
{    

    public static void SetUser(this Activity activity,Guid guid)
    {
        activity.SetCustomProperty("IdUser", guid);
    }

    public static Guid? GetCurrentUser(this Activity activity)
    {
        return activity.GetCustomProperty("IdUser") as Guid?;
    }
}


