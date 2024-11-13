using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
﻿using Microsoft.AspNetCore.Identity;

namespace HelpPlatform.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string? Initials {get; set;}
    
}