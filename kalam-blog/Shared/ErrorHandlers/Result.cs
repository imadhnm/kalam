public class Result
{
    private Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid Error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public Error Error { get; }

    public static Result Succeeded() => new(true, Error.None);
    public static Result Failed(Error error) => new(false, error);
}

public sealed record Error(string code, string description)
{
    public static readonly Error None = new(string.Empty, string.Empty);
}