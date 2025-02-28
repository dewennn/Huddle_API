using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Huddle.Models
{
    public class FriendshipRequest
    {
        public FriendshipRequest() { }
        public FriendshipRequest(Guid id1, Guid id2)
        {
            this.SenderId = id1;
            this.ReceiverId = id2;
        }

        [Key, Column(Order = 0)]
        public Guid SenderId { get; set; }

        [Key, Column(Order = 1)]
        public Guid ReceiverId { get; set; }

        // Navigation Properties
        [ForeignKey("SenderId")]
        public User Sender { get; set; } = null!;

        [ForeignKey("ReceiverId")]
        public User Receiver { get; set; } = null!;
    }

}
