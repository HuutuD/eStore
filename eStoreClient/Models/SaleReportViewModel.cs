using eStoreClient.Services;

namespace eStoreClient.Models
{
    public class SaleReportViewModel
    {

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<SalesReportDto> SalesReport { get; set; }
    }


}
