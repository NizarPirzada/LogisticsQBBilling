namespace FTDTO.Truck
{
    public class TruckUpdateDto
    {
        public int TruckID { get; set; }
        public int TruckTypeID { get; set; }
        public int DefaultDriverID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsInactive { get; set; }
    }
}
