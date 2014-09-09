
namespace BankProject.Entity
{
    public class City
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(this.Code))
            {
                return string.Empty;
            }

            return string.Format("{0}-{1}", this.Code, this.Name);
        }
    }
}