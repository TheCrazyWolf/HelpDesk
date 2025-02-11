using HelpDesk.Models.PLA.Common;

namespace HelpDesk.Models.PLA.Documents;

public class ViewDocument: PlaEntity
{
    public string FileName { get; set; } = string.Empty;
}