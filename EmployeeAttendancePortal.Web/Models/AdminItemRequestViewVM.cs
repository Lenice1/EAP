using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Web.Models
{
    public class AdminItemRequestViewVM
    {
        [Display(Name ="Total Number of Requests")]
        public int TotalRequests { get; set; }
        [Display(Name = "Approved Requests")]
        public int ApprovedRequests { get; set; }
        [Display(Name = "Pending Requests")]
        public int PendingRequests { get; set; }
        [Display(Name = "Denied Requests")]
        public int DeniedRequests { get; set; }

        public List<ItemRequestVM> ItemRequests { get; set; }
    }
}
