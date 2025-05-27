// namespace TodoApi.Models
// {
//     public class TodoItemDTO
//     {
//         public long Id { get; set; }
//         public string? Name { get; set; }
//         public bool IsComplete { get; set; }
//     }
// }

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    [Table("tbl_todo")]
    public class TodoItemDTO
    {
        [Column("todo_id")]
        public long Id { get; set; }

        [Column("todo_name")]
        [Required]
        [StringLength(20)]
        public string Name { get; set; } = "";

        [Column("is_complete")]
        [Required]
        public bool IsComplete { get; set; } 

        
    }
}