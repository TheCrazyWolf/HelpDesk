using System.ComponentModel.DataAnnotations.Schema;
using HelpDesk.Models.DLA.Common;
using HelpDesk.Models.DLA.Identity;

namespace HelpDesk.Models.DLA.Documents;

public class DeskDocument : DlaEntity
{
    public string FileName { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; } = DateTime.Now;
    public long? UserId { get; set; }
    [ForeignKey("UserId")] public Account? User { get; set; }
}