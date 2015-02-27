using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPence.Models
{
    [Table("list_parameter")]
    public class ListParameter
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("list")]
        public long List { get; set; }

        [Required]
        [Column("parameter")]
        public long Parameter { get; set; }

        public static ListParameter Createlist_parameter(long id, long list, long parameter)
        {
            var listParameter = new ListParameter {Id = id, List = list, Parameter = parameter};

            return listParameter;
        }
    }
}