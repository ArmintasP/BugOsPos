using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugOsPos.Contracts.Locations;
public sealed record UpdateLocationRequest(
    string? Name,
    string? Adress);
