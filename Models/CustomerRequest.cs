using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    public class CustomerRequest
    {
        public long CustomerId { get; set; }
        [Required]
        [StringLength(10)]
        public string CustomerName { get; set; } = string.Empty;
        public DateTime? RegisterDate { get; set; }
        public string CustomerAddress { get; set; } = string.Empty;
        public int? CusCustomerTypeId { get; set; }
    }
}