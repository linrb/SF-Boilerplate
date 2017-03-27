﻿ 
using System;

namespace SF.Core.Abstraction.GenericServices
{

    public interface IGenericLogger
    {
        void Verbose(object message);
        void VerboseFormat(string format, params object[] args);
        void Info(object message);
        void InfoFormat(string format, params object[] args);
        void Warn(object message);
        void WarnFormat(string format, params object[] args);
        void Error(object message);
        void ErrorFormat(string format, params object[] args);
        void Critical(object message);
        void Critical(object message, Exception ex);                //this is for logging exceptions
        void CriticalFormat(string format, params object[] args);
    }
}
