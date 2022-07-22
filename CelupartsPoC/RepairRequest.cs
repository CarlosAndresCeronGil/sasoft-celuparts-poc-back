namespace CelupartsPoC
{
    public class RepairRequest
    {
        public int IdRepairRequest { get; set; }
        public int IdUser { get; set; }
        public string PickUpAddress { get; set; } = string.Empty;
        public string DeliveryAddress { get; set; } = string.Empty;
        public DateTime PickUpTime { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
    }
}
