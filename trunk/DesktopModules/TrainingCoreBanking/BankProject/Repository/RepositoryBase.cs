namespace BankProject.Repository
{
    using System.Configuration;

    using BankProject.Entity;

    public class RepositoryBase
    {
        protected readonly int PortalId;

        public RepositoryBase()
        {
            var currentPortal = DotNetNuke.Entities.Portals.PortalController.GetCurrentPortalSettings();
            if (currentPortal != null)
            {
                this.PortalId = currentPortal.PortalId;
            }
        }

        protected BankProjectModelsDataContext CreateDbContext()
        {
            var connectionString =
                ConfigurationManager.ConnectionStrings["VietVictoryCoreBanking"].ConnectionString;
            return new BankProjectModelsDataContext(connectionString);
        }
    }
}