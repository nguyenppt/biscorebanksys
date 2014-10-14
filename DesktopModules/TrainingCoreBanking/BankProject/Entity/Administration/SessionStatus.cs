namespace BankProject.Entity.Administration
{
    using System;

    public class SessionStatus
    {
        public SessionStatus(DateTime lastTimeAccess, string sessionId, string ipAddress)
        {
            this.LastTimeAccess = lastTimeAccess; 
            this.SessionId = sessionId;
            this.IpAddress = ipAddress;
        }

        public DateTime LastTimeAccess { get; set; }

        public string SessionId { get; private set; }

        public string IpAddress { get; private set; }
    }
}