using System.Net;

namespace OPP.Domain.Exceptions;

public sealed class NotFoundException : AppException
{
    public NotFoundException(string entity, object key)
        : base((int)HttpStatusCode.NotFound,
            "not_found",
            $"{entity} with key '{key}' was not found.")
    {
    }
}

public class UnauthorizedException : AppException
{
    public UnauthorizedException(string message = "Invalid credentials")
        : base((int)HttpStatusCode.Unauthorized,
            "unauthorized",
            message)
    {
    }
}

public class ValidationException : AppException
{
    public IReadOnlyDictionary<string, string[]> Errors { get; }

    public ValidationException(
        string message,
        IReadOnlyDictionary<string, string[]> errors
    ) : base((int)HttpStatusCode.BadRequest, "validation_problem", message )
    {
        Errors = errors;
    }
}

public class PermissionException : AppException
{
    public PermissionException(string message = "You dont have permission to do this")
        : base((int)HttpStatusCode.Forbidden,
            "permission_forbidden",
            message)
    {
    }
}