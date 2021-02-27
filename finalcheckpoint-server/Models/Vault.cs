namespace finalcheckpoint_server.Models
{
    public class Vault
    {
        public int Id { get; set; }

        public string CreatorId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPrivate { get; set; }

        // This is essentially the virtual from node.js
        public Profile Creator { get; set; }
    }
}