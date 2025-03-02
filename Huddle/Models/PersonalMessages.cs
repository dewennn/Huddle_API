using System.ComponentModel.DataAnnotations.Schema;

namespace Huddle.Models
{
    public class PersonalMessages
    {
        public PersonalMessages()
        {}

        public Guid MessageId { get; set; }
        public Guid SenderId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Content { get; set; }
        public Guid ReceiverId { get; set; }

        [ForeignKey("SenderId")]
        public User Sender { get; set; }
        [ForeignKey("ReceiverId")]
        public User Receiver { get; set; }
    }
}
