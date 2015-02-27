using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPence.Models
{
    [Table("parameter_list")]
    public class ParameterList
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        public static ParameterList Createparameter_list(long id)
        {
            var parameterList = new ParameterList {Id = id};

            return parameterList;
        }
    }
}