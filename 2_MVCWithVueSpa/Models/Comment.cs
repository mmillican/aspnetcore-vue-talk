using System;

namespace MVCWithVueSpa.Models
{
    public class Comment 
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public DateTime Timestamp { get; set; }

        public string Name { get; set; }
        
        public string Text { get; set; }
    }
}