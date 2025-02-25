using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Huddle.Models
{
    public class Friendship
    {
        [Key, Column(Order = 0)]
        public Guid UserOneId { get; set; }

        [Key, Column(Order = 1)]
        public Guid UserTwoId { get; set; }

        // Navigation Properties
        [ForeignKey("UserOneId")]
        public User UserOne { get; set; } = null!;

        [ForeignKey("UserTwoId")]
        public User UserTwo { get; set; } = null!;
    }

}
