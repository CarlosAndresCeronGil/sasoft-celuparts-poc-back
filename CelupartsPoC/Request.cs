namespace CelupartsPoC
{
    public class Request
    {
        public int IdRepairString { get; set; }
        public string RequestType { get; set; } = string.Empty;
        public string PickUpAddress { get; set; } = string.Empty;
        public string DeliveryAddress { get; set; } = string.Empty;
        public DateTime PickUpTime { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int Quote { get; set; } = 0;
        public string StatusQuote { get; set; } = string.Empty;
        public string TechnicalRemarks { get; set; } = string.Empty;
    }
}
