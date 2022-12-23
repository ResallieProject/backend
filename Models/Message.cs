using System.ComponentModel.DataAnnotations.Schema;

namespace Resallie.Models;

public class Message : Model
{
    [ForeignKey("User")]
    public int UserId { get; set; }
    public virtual User? User { get; set; }

    [ForeignKey("Conversation")]
    public int ConversationId { get; set; }
    public virtual Conversation? Conversation { get; set; }
    
    public string Content { get; set; }
}