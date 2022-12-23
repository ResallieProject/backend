using System.ComponentModel.DataAnnotations.Schema;

namespace Resallie.Models;

public class Conversation : Model
{
    [ForeignKey("Advertisement")]
    public int AdvertisementId { get; set; }
    public virtual Advertisement? Advertisement { get; set; }

    [ForeignKey("Participant")]
    public int ParticipantId { get; set; }
    public virtual User? Participant { get; set; }
}