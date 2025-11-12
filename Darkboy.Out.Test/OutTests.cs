namespace Darkboy.Out.Test;

public class OutTests {
    [Test]
    public void OkShouldCreateSuccessResult() {
        var result = Out.Ok();

        using (Assert.EnterMultipleScope()) {
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.Null);
            Assert.That(result.Exception, Is.Null);
        }
    }

    [Test]
    public void OkWithMessageShouldCreateSuccessResultWithMessage() {
        const string message = "Operation completed successfully";
        var result = Out.Ok(message);

        using (Assert.EnterMultipleScope()) {
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo(message));
            Assert.That(result.Exception, Is.Null);
        }
    }

    [Test]
    public void FailShouldCreateFailureResult() {
        var result = Out.Fail();

        using (Assert.EnterMultipleScope()) {
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.Null);
            Assert.That(result.Exception, Is.Null);
        }
    }

    [Test]
    public void FailWithMessageShouldCreateFailureResultWithMessage() {
        const string message = "Operation failed";
        var result = Out.Fail(message);

        using (Assert.EnterMultipleScope()) {
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo(message));
            Assert.That(result.Exception, Is.Null);
        }
    }

    [Test]
    public void FailWithExceptionShouldCreateFailureResultWithException() {
        var exception = new InvalidOperationException("Test exception");
        var result = Out.Fail(exception);

        using (Assert.EnterMultipleScope()) {
            Assert.That(result.Success, Is.False);
            Assert.That(result.Exception, Is.EqualTo(exception));
            Assert.That(result.Message, Is.Null);
        }
    }

    [Test]
    public void FailWithExceptionAndMessageShouldCreateFailureResultWithBoth() {
        var exception = new InvalidOperationException("Test exception");
        const string message = "Operation failed with error";
        var result = Out.Fail(exception, message);

        using (Assert.EnterMultipleScope()) {
            Assert.That(result.Success, Is.False);
            Assert.That(result.Exception, Is.EqualTo(exception));
            Assert.That(result.Message, Is.EqualTo(message));
        }
    }

    [Test]
    public void OfWithTrueShouldCreateSuccessResult() {
        var result = Out.Of(true);

        Assert.That(result.Success, Is.True);
    }

    [Test]
    public void OfWithFalseShouldCreateFailureResult() {
        var result = Out.Of(false);

        Assert.That(result.Success, Is.False);
    }

    [Test]
    public void WithSuccessShouldSetSuccessFlag() {
        var result = Out.Empty.WithSuccess(true);

        Assert.That(result.Success, Is.True);
    }

    [Test]
    public void WithMessageShouldSetMessage() {
        const string message = "Test message";
        var result = Out.Empty.WithMessage(message);

        Assert.That(result.Message, Is.EqualTo(message));
    }

    [Test]
    public void WithExceptionShouldSetException() {
        var exception = new Exception("Test exception");
        var result = Out.Empty.WithException(exception);

        Assert.That(result.Exception, Is.EqualTo(exception));
    }

    [Test]
    public void WithShouldSetExceptionAndMessage() {
        var exception = new Exception("Test exception");
        const string message = "Test message";
        var result = Out.Empty.With(exception, message);

        using (Assert.EnterMultipleScope()) {
            Assert.That(result.Exception, Is.EqualTo(exception));
            Assert.That(result.Message, Is.EqualTo(message));
        }
    }

    [Test]
    public void FluentApiShouldChainMethods() {
        var exception = new Exception("Test exception");
        const string message = "Test message";
        var result = Out.Empty
            .WithSuccess(false)
            .WithMessage(message)
            .WithException(exception);

        using (Assert.EnterMultipleScope()) {
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo(message));
            Assert.That(result.Exception, Is.EqualTo(exception));
        }
    }

    [Test]
    public void MatchWithSuccessShouldCallSuccessAction() {
        var result = Out.Ok();
        var successCalled = false;
        var failCalled = false;

        result.Match(
            success: _ => successCalled = true,
            fail: _ => failCalled = true
        );

        using (Assert.EnterMultipleScope()) {
            Assert.That(successCalled, Is.True);
            Assert.That(failCalled, Is.False);
        }
    }

    [Test]
    public void MatchWithFailureShouldCallFailAction() {
        var result = Out.Fail();
        var successCalled = false;
        var failCalled = false;

        result.Match(
            success: _ => successCalled = true,
            fail: _ => failCalled = true
        );

        using (Assert.EnterMultipleScope()) {
            Assert.That(successCalled, Is.False);
            Assert.That(failCalled, Is.True);
        }
    }

    [Test]
    public void MatchShouldPassResultToAction() {
        const string message = "Test message";
        var result = Out.Ok(message);
        Out? capturedResult = null;

        result.Match(
            success: r => capturedResult = r,
            fail: _ => { }
        );

        Assert.That(capturedResult, Is.Not.Null);
        Assert.That(capturedResult!.Message, Is.EqualTo(message));
    }

    [Test]
    public void FromResultShouldCopyProperties() {
        const string originalMessage = "Original message";
        var originalException = new Exception("Original exception");
        var original = Out.Fail(originalException, originalMessage);

        var copy = Out.From(original);

        using (Assert.EnterMultipleScope()) {
            Assert.That(copy.Success, Is.EqualTo(original.Success));
            Assert.That(copy.Message, Is.EqualTo(original.Message));
            Assert.That(copy.Exception, Is.EqualTo(original.Exception));
        }
    }

    [Test]
    public void ImplicitConversionFromExceptionShouldCreateFailureResult() {
        var exception = new InvalidOperationException("Test exception");
        Out @out = exception;

        using (Assert.EnterMultipleScope()) {
            Assert.That(@out.Success, Is.False);
            Assert.That(@out.Exception, Is.EqualTo(exception));
        }
    }

    [Test]
    public void ImplicitConversionFromNullExceptionShouldCreateFailureResult() {
        Exception? exception = null;
        Out @out = exception;

        using (Assert.EnterMultipleScope()) {
            Assert.That(@out.Success, Is.False);
            Assert.That(@out.Exception, Is.Null);
        }
    }

    [Test]
    public void EmptyShouldCreateDefaultResult() {
        var result = Out.Empty;

        using (Assert.EnterMultipleScope()) {
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.Null);
            Assert.That(result.Exception, Is.Null);
        }
    }

    [Test]
    public void FromResultTShouldCopyPropertiesWithoutValue() {
        const int value = 42;
        const string message = "Test message";
        var exception = new Exception("Test exception");
        var original = Out<int>.Fail(value, message, exception);

        var copy = Out.From(original);

        using (Assert.EnterMultipleScope()) {
            Assert.That(copy.Success, Is.EqualTo(original.Success));
            Assert.That(copy.Message, Is.EqualTo(original.Message));
            Assert.That(copy.Exception, Is.EqualTo(original.Exception));
        }
    }

    [Test]
    public void WithShouldSetOnlyException() {
        var exception = new Exception("Test exception");
        var result = Out.Empty.With(exception);

        using (Assert.EnterMultipleScope()) {
            Assert.That(result.Exception, Is.EqualTo(exception));
            Assert.That(result.Message, Is.Null);
        }
    }
}
