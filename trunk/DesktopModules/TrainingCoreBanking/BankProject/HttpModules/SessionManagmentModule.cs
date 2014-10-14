namespace BankProject.HttpModules
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Web;

    using BankProject.Entity.Administration;

    using DotNetNuke.Common;
    using DotNetNuke.Entities.Users;
    using DotNetNuke.Security;

    public class SessionManagmentModule : IHttpModule
    {
        private static readonly object Sync = new object();

        private static bool isInited;

        private static SessionManagment sharedSessionManagement;

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            if (!isInited)
            {
                lock (Sync)
                {
                    if (!isInited)
                    {
                        isInited = true;
                        sharedSessionManagement = SessionManagment.Default;
                    }
                }
            }

            context.AcquireRequestState += this.OnAcquireRequestState;
        }

        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed. Suppression is OK here.")]
        private void OnAcquireRequestState(object sender, EventArgs eventArgs)
        {
            Debug.WriteLine("SessionManagement OnAuthenticateRequest");
            var context = HttpContext.Current;

            if (context.User == null || context.User.Identity == null || !context.User.Identity.IsAuthenticated)
            {
                return;
            }

            var session = context.Session;
            if (session == null)
            {
                return;
            }

            var userInfo = UserController.GetCurrentUserInfo();
            if (userInfo.IsSuperUser)
            {
                return;
            }

            var userId = userInfo.UserID;
            var sessionId = session.SessionID;
            var ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!sharedSessionManagement.RegisterSession(userId, sessionId, ipAddress))
            {
                // Logout user
                var objPortalSecurity = new PortalSecurity();
                objPortalSecurity.SignOut();

                // Redirect the user to the current page
                context.Response.Redirect(Globals.NavigateURL());
            }
        }
    }
}