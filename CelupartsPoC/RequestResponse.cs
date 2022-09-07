namespace CelupartsPoC
{
    public class RequestResponse
    {
        public List<RequestWithEquipments> Requests { get; set; } = new List<RequestWithEquipments>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
