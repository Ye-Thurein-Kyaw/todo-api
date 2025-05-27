using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    public class CustomerResult
    {
        public long CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerAddress { get; set; } = string.Empty;
        public DateTime? RegisterDate { get; set; }
        public int? CusCustomerTypeId { get; set; }
        public string? CusCustomerTypeName { get; set; }
    }
}