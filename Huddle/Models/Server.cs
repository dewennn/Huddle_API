using System;
using System.Collections.Generic;

namespace Huddle.Models
{
    public partial class Server
    {
        public Server()
        {}

        public Guid Id { get; set; }
        public string? ProfilePictureUrl { get; set; }

    }
}
