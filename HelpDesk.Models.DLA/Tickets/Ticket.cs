﻿using System.ComponentModel.DataAnnotations.Schema;
using HelpDesk.Models.DLA.Common;
using HelpDesk.Models.DLA.Identity;
using HelpDesk.Models.Enums.Tickets;

namespace HelpDesk.Models.DLA.Tickets;

public class Ticket : DlaEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PlaceOfIssue { get; set; } = string.Empty;
    public TicketType Type { get; set; }
    public TicketStatus Status { get; set; }
    public TicketLevelPriority Priority { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? Deadline { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public long? UserId { get; set; }
    [ForeignKey("UserId")] public Account? User { get; set; }
}