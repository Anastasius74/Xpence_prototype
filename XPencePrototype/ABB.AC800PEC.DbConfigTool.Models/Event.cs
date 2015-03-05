using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABB.AC800PEC.DbConfigTool.Models
{
    [Table("event")]
    public class Event
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("parameter")]
        public long Parameter { get; set; }

        [Required]
        [Column("class")]
        public long ClassItem { get; set; }

        [Required]
        [Column("trigger")]
        public string Trigger { get; set; }

        public static Event Create(long id, long parameter, long classItem, string trigger)
        {
            var eventObject = new Event {Id = id, Parameter = parameter, ClassItem = classItem, Trigger = trigger};

            return eventObject;
        }
    }
}