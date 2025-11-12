namespace Darkboy.Out;

public class Out : IOut {
    public bool Success { get; private set; }
    public string? Message { get; private set; }
    public Exception? Exception { get; private set; }

    protected Out() { }

    public Out WithSuccess(bool success) {
        Success = success;
        return this;
    }

    public Out WithMessage(string? message) {
        Message = message;
        return this;
    }

    public Out WithException(Exception? exception) {
        Exception = exception;
        return this;
    }

    public Out With(Exception? exception, string? message = null) {
        return WithException(exception).WithMessage(message);
    }

    public void Match(Action<Out> success, Action<Out> fail) {
        if (Success) {
            success(this);
        } else {
            fail(this);
        }
    }

    public static Out Empty => new();

    public static Out Ok() => new() { Success = true };
    public static Out Ok(string? message) => new() { Success = true, Message = message };

    public static Out Fail() => new() { Success = false };
    public static Out Fail(string? message) => new() { Success = false, Message = message };
    public static Out Fail(Exception? exception, string? message = null) => new() { Success = false, Exception = exception, Message = message };

    public static Out Of(bool success) => new() { Success = success };

    public static Out From(Out @out) => new() { Success = @out.Success, Message = @out.Message, Exception = @out.Exception };
    public static Out From<T>(Out<T> @out) => new() { Success = @out.Success, Message = @out.Message, Exception = @out.Exception };

    public static implicit operator Out(Exception? exception) => new() { Success = false, Exception = exception };
}
