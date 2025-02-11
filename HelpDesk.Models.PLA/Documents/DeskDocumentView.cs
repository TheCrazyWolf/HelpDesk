using HelpDesk.Models.PLA.Common;

namespace HelpDesk.Models.PLA.Documents;

public class DeskDocumentView: PlaEntity
{
    public string FileName { get; set; } = string.Empty;
}