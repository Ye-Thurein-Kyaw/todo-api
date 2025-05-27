// namespace TodoApi.Models
// {
//     public class HeroItemDTO
//     {
//         public int Id { get; set; }
//         public string? Name { get; set; }
//         public string? Address { get; set; }
//         public string? Secret { get; set; }

//     }
// }

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    [Table("tbl_hero")]
    public class HeroItemDTO
    {
        [Column("hero_id")]
        public int Id { get; set; }

        [Column("hero_name")]
        [Required]
        [StringLength(20)]
        public string Name { get; set; } = "";

        [Column("address")]
        [StringLength(50)]
        public string Address { get; set; } = "";

      
    }
}