using FrontLineCleaners.Domain.Constants;
using FrontLineCleaners.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontLineCleaners.Domain.Interfaces;

public interface ICleanerAuthorizationService
{
    bool Authorize(Cleaner cleaner, ResourceOperation resourceOperation);
}
