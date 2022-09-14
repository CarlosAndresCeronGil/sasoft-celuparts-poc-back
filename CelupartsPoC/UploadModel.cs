namespace CelupartsPoC
{
    public class UploadModel
    {
        public string TypeOfEquipment { get; set; } = string.Empty;
        public string EquipmentBrand { get; set; } = string.Empty;
        public string ModelOrReference { get; set; } = string.Empty;
        public string ImeiOrSerial { get; set; } = string.Empty;
        public IFormFile EquipmentInvoice { get; set; }
    }
}
