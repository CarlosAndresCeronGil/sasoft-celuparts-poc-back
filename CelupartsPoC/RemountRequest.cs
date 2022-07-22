namespace CelupartsPoC
{
    public class RemountRequest
    {
        public int IdRemountRequest {  get; set; }
        public int IdUser { get; set; }
        public string PickUpAddress { get; set; } = string.Empty;
        public string DeliveryAddress { get; set; } = string.Empty;
        public DateTime PickUpTime { get; set; }
        public string EquipmentInvoice { get; set; } = string.Empty;
    }
}
