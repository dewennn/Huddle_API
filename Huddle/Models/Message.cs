using System;
using System.Collections.Generic;

namespace Huddle.Models
{
    public partial class Message
    {
        public Guid MessageId { get; set; }
        public Guid SenderId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Content { get; set; } = null!;
        public Guid? UserTargetId { get; set; }
        public Guid? ChannelTargetId { get; set; }

        public virtual Server? ChannelTarget { get; set; }
        public virtual User Sender { get; set; } = null!;
        public virtual User? UserTarget { get; set; }
    }
}
