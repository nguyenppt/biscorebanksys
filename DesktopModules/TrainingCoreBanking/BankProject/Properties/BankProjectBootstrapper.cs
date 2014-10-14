namespace BankProject.Properties
{
    using BankProject.HttpModules;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    public static class BankProjectBootstrapper
    {
        public static void Initialize()
        {
            DynamicModuleUtility.RegisterModule(typeof(SessionManagmentModule));
        }
    }
}