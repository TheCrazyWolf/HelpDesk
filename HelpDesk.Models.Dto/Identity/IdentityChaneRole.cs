﻿using HelpDesk.Models.Dto.Common;
using HelpDesk.Models.Enums.Identity;

namespace HelpDesk.Models.Dto.Identity;

public class IdentityChaneRole : WithId
{
    public IdentityType IdentityType { get; set; }
}