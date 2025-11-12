namespace Darkboy.Out;

public class Out<T> : IOut {
    public bool Success { get; private set; }
    public string? Message { get; set; }
    public Exception? Exception { get; set; }
    public T? Value { get; set; }

    protected Out() { }

    public Out<T> WithData(T? value) {
        Value = value;
        return this;
    }

    public Out<T> WithMessage(string? message) {
        Message = message;
        return this;
    }

    public Out<T> WithException(Exception? exception) {
        Exception = exception;
        return this;
    }

    public Out<T> With(T? value, string? message = null) {
        return WithData(value).WithMessage(message);
    }

    public Out<T> With(T? value, string? message, Exception? exception) {
        return WithData(value).WithMessage(message).WithException(exception);
    }

    public void Match(Action<Out<T>> success, Action<Out<T>> fail) {
        if (Success) {
            success(this);
        } else {
            fail(this);
        }
    }

    public static Out<T> Empty => new();

    public static Out<T> Ok() => new() { Success = true };
    public static Out<T> Ok(T? value) => new() { Success = true, Value = value };
    public static Out<T> Ok(T? value, string? message) => new() { Success = true, Value = value, Message = message };

    public static Out<T> Fail() => new() { Success = false };
    public static Out<T> Fail(T? value) => new() { Success = false, Value = value };
    public static Out<T> Fail(T? value, string? message) => new() { Success = false, Value = value, Message = message };
    public static Out<T> Fail(T? value, string? message, Exception? exception) => new() { Success = false, Value = value, Message = message, Exception = exception };
    public static Out<T> Fail(string? message, Exception? exception) => new() { Success = false, Message = message, Exception = exception };

    public static Out<T> Of(bool success) => new() { Success = success };

    public static Out<T> From(Out<T> other) => new() { Success = other.Success, Value = other.Value, Message = other.Message, Exception = other.Exception };
    public static Out<T> From(Out other) => new() { Success = other.Success, Message = other.Message, Exception = other.Exception };

    public static implicit operator Out<T>(T? value) => new() { Success = true, Value = value };
    public static implicit operator Out<T>(Exception? exception) => new() { Success = false, Exception = exception };
    public static implicit operator Out<T>(Out other) => From(other);
    public static explicit operator Out(Out<T> other) => Out.From(other);
}
