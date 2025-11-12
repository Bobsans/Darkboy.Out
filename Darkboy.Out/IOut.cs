namespace Darkboy.Out;

public interface IOut {
    bool Success { get; }
    string? Message { get; }
    Exception? Exception { get; }
}
