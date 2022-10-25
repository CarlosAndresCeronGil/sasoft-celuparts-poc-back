namespace CelupartsPoC
{
    public class RetomaPaymentUploadModel
    {
        public int IdRetomaPayment { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public DateTime? PaymentDate { get; set; }
        public string VoucherNumber { get; set; } = string.Empty;
        public int IdRetoma { get; set; }
        public IFormFile? RetomaBillPayment { get; set; }
    }
}
