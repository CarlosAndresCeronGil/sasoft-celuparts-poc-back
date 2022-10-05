namespace CelupartsPoC
{
    public class UploadModel
    {
        public string EquipmentBrand { get; set; } = string.Empty;
        public string ModelOrReference { get; set; } = string.Empty;
        public string ImeiOrSerial { get; set; } = string.Empty;
        public int IdTypeOfEquipment { get; set; }
        public IFormFile? EquipmentInvoice { get; set; }
    }
}
