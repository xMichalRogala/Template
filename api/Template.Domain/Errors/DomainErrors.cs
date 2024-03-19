using Template.Domain.Utilities;

namespace Template.Domain.Errors
{
    public static class DomainErrors
    {
        public static class General
        {
            public static Error ServerError => new Error("General.ServerError", "The server encountered an unrecoverable error.");
        }
    }
}
