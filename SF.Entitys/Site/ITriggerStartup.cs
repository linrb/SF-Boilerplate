﻿

namespace SF.Entitys
{
    /// <summary>
    /// when new folder sites are created from the ui we need a way to restart the app
    /// or more specifically we want to trigger the execution of the startup logic
    /// so that authentication and routes can be wired up for the new folder site
    /// </summary>
    public interface ITriggerStartup
    {
        bool TriggerStartup();
    }
}
