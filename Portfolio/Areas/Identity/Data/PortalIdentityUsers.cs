using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Portfolio.Areas.Identity.Data;

// Add profile data for application users by adding properties to the PortalIdentityUsers class
// User logins will be in default AspNetUsers table not a PortalIdentityUsers table even though class name is PortalIdentityUsers
public class PortalIdentityUsers : IdentityUser
{
}

