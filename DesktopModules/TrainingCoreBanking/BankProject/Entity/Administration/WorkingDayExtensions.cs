namespace BankProject.Entity.Administration
{
    public static class WorkingDayExtensions
    {
        public static bool Is(this WorkingDay workingDay, WorkingDay dayToCompare)
        {
            return (workingDay & dayToCompare) == dayToCompare;
        }
    }
}