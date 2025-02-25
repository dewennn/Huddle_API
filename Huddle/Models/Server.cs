using System;
using System.Collections.Generic;

namespace Huddle.Models
{
    public partial class Server
    {
        public Server()
        {
            Messages = new HashSet<Message>();
        }

        public Guid Id { get; set; }
        public string? ProfilePictureUrl { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
