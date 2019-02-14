using System.ComponentModel.DataAnnotations;

namespace ConversationManager
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        public int Amount { get; set; }
    }
}