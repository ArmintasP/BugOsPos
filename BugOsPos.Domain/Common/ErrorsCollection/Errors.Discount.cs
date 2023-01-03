using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugOsPos.Domain.Common.ErrorsCollection;
public static partial class Errors
{
    public static class Discount
    {
        public static Error NotFound => Error.NotFound(
            code: "Discount.NotFound",
            description: "The discount was not found.");
    }
}
