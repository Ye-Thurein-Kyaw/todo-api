using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    [Table("tbl_customer")]
    public class Customer
    {
        [Column("customer_id")]
        [Key]
        public long CustomerId { get; set; }

        [Required]
        [Column("customer_name")]
        [StringLength(50)]
        public string CustomerName { get; set; } = string.Empty;

        [Column("register_date")]
        public DateTime? RegisterDate { get; set; }

        [Column("customer_address")]
        [StringLength(100)]
        public string CustomerAddress { get; set; } = string.Empty;

        [Column("cus_customertype_id")]
        public int? CusCustomerTypeId { get; set; }
        
        [ForeignKey("CusCustomerTypeId")]
        public CustomerType? CusCustomerType { get; set; }
    }
}