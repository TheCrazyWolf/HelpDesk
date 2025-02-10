using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Models.DLA.Common;

public class DlaEntity
{
    [Key] public long Id { get; set; }
}