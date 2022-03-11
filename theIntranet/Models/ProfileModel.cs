namespace theIntranet.Models
{
    public class ProfileModel
    {
        public int Id { get; set; }
        public string DisplayName { get; set; } = "";
        public string Mail { get; set; } = "";
        public string OfficeLocation { get; set; } = "";
        public string JobTitle { get; set; } = "";
    }
}
