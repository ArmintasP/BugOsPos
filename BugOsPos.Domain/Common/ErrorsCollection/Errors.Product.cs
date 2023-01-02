using ErrorOr;

namespace BugOsPos.Domain.Common.ErrorsCollection;
public static partial class Errors
{
    public static class Product
    {
        public static Error NotFound => Error.NotFound(
            code: "Product.NotFound",
            description: "The product was not found.");
    }
}