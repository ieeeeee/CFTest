namespace OA.Data.Entity
{
    public class B_LoginLogEntity:BaseEntity
    {
        public string UserID { get; set; }
        public string LoginName { get; set; }

        public string IP { get; set; }

        public string Mac { get; set; }
    }
}