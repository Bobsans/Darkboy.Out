namespace Darkboy.Out.Test;

public class ResultTTests {
    [Test]
    public void EmptyShouldCreateEmptyResult() {
        var result = Out<int>.Empty;

        using (Assert.EnterMultipleScope()) {
            Assert.That(result.Success, Is.False);
            Assert.That(result.Value, Is.EqualTo(0));
            Assert.That(result.Message, Is.Null);
            Assert.That(result.Exception, Is.Null);
        }
    }

    [Test]
    public void OkShouldCreateSuccessResult() {
        var result = Out<int>.Ok();

        using (Assert.EnterMultipleScope()) {
            Assert.That(result.Success, Is.True);
            Assert.That(result.Value, Is.EqualTo(0));
            Assert.That(result.Message, Is.Null);
            Assert.That(result.Exception, Is.Null);
        }
    }

    [Test]
    public void OkWithValueShouldCreateSuccessResultWithValue() {
        const int value = 42;
        var result = Out<int>.Ok(value);

        using (Assert.EnterMultipleScope()) {
            Assert.That(result.Success, Is.True);
            Assert.That(result.Value, Is.EqualTo(value));
            Assert.That(result.Message, Is.Null);
            Assert.That(result.Exception, Is.Null);
        }
    }

    [Test]
    public void OkWithValueAndMessageShouldCreateSuccessResultWithBoth() {
        const int value = 42;
        const string message = "Operation successful";
        var result = Out<int>.Ok(value, message);

        using (Assert.EnterMultipleScope()) {
            Assert.That(result.Success, Is.True);
            Assert.That(result.Value, Is.EqualTo(value));
            Assert.That(result.Message, Is.EqualTo(message));
            Assert.That(result.Exception, Is.Null);
        }
    }

    [Test]
    public void OkWithReferenceTypeShouldStoreValue() {
        const string value = "test string";
        var result = Out<string>.Ok(value);

        using (Assert.EnterMultipleScope()) {
            Assert.That(result.Success, Is.True);
            Assert.That(result.Value, Is.EqualTo(value));
        }
    }

    [Test]
    public void FailShouldCreateFailureResult() {
        var result = Out<int>.Fail();

        using (Assert.EnterMultipleScope()) {
            Assert.That(result.Success, Is.False);
            Assert.That(result.Value, Is.EqualTo(0));
            Assert.That(result.Message, Is.Null);
            Assert.That(result.Exception, Is.Null);
        }
    }

    [Test]
    public void FailWithValueShouldCreateFailureResultWithValue() {
        const int value = 42;
        var result = Out<int>.Fail(value);

        using (Assert.EnterMultipleScope()) {
            Assert.That(result.Success, Is.False);
            Assert.That(result.Value, Is.EqualTo(value));
            Assert.That(result.Message, Is.Null);
            Assert.That(result.Exception, Is.Null);
        }
    }

    [Test]
    public void FailWithValueAndMessageShouldCreateFailureResultWithBoth() {
        const int value = 42;
        const string message = "Operation failed";
        var result = Out<int>.Fail(value, message);

        using (Assert.EnterMultipleScope()) {
            Assert.That(result.Success, Is.False);
            Assert.That(result.Value, Is.EqualTo(value));
            Assert.That(result.Message, Is.EqualTo(message));
            Assert.That(result.Exception, Is.Null);
        }
    }

    [Test]
    public void FailWithValueExceptionAndMessageShouldCreateFailureResultWithAll() {
        const int value = 42;
        const string message = "Operation failed with error";
        var exception = new InvalidOperationException("Test exception");
        var result = Out<int>.Fail(value, message, exception);

        using (Assert.EnterMultipleScope()) {
            Assert.That(result.Success, Is.False);
            Assert.That(result.Value, Is.EqualTo(value));
            Assert.That(result.Exception, Is.EqualTo(exception));
            Assert.That(result.Message, Is.EqualTo(message));
        }
    }

    [Test]
    public void FailWithExceptionAndMessageShouldCreateFailureResultWithBoth() {
        const string message = "Operation failed";
        var exception = new InvalidOperationException("Test exception");
        var result = Out<int>.Fail(message, exception);

        using (Assert.EnterMultipleScope()) {
            Assert.That(result.Success, Is.False);
            Assert.That(result.Exception, Is.EqualTo(exception));
            Assert.That(result.Message, Is.EqualTo(message));
        }
    }

    [Test]
    public void OfWithTrueShouldCreateSuccessResult() {
        var result = Out<int>.Of(true);

        Assert.That(result.Success, Is.True);
    }

    [Test]
    public void OfWithFalseShouldCreateFailureResult() {
        var result = Out<int>.Of(false);

        Assert.That(result.Success, Is.False);
    }

    [Test]
    public void WithDataShouldSetValue() {
        const int value = 42;
        var result = Out<int>.Empty.WithData(value);

        Assert.That(result.Value, Is.EqualTo(value));
    }

    [Test]
    public void WithMessageShouldSetMessage() {
        const string message = "Test message";
        var result = Out<int>.Empty.WithMessage(message);

        Assert.That(result.Message, Is.EqualTo(message));
    }

    [Test]
    public void WithExceptionShouldSetException() {
        var exception = new Exception("Test exception");
        var result = Out<int>.Empty.WithException(exception);

        Assert.That(result.Exception, Is.EqualTo(exception));
    }

    [Test]
    public void WithWithValueAndMessageShouldSetBoth() {
        const int value = 42;
        const string message = "Test message";
        var result = Out<int>.Empty.With(value, message);

        using (Assert.EnterMultipleScope()) {
            Assert.That(result.Value, Is.EqualTo(value));
            Assert.That(result.Message, Is.EqualTo(message));
        }
    }

    [Test]
    public void WithWithValueExceptionAndMessageShouldSetAll() {
        const int value = 42;
        const string message = "Test message";
        var exception = new Exception("Test exception");
        var result = Out<int>.Empty.With(value, message, exception);

        using (Assert.EnterMultipleScope()) {
            Assert.That(result.Value, Is.EqualTo(value));
            Assert.That(result.Exception, Is.EqualTo(exception));
            Assert.That(result.Message, Is.EqualTo(message));
        }
    }

    [Test]
    public void FluentApiShouldChainMethods() {
        const int value = 42;
        const string message = "Test message";
        var exception = new Exception("Test exception");
        var result = Out<int>.Empty
            .WithData(value)
            .WithMessage(message)
            .WithException(exception);

        using (Assert.EnterMultipleScope()) {
            Assert.That(result.Value, Is.EqualTo(value));
            Assert.That(result.Message, Is.EqualTo(message));
            Assert.That(result.Exception, Is.EqualTo(exception));
        }
    }

    [Test]
    public void MatchWithSuccessShouldCallSuccessAction() {
        var result = Out<int>.Ok(42);
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
        var result = Out<int>.Fail();
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
        const int value = 42;
        var result = Out<int>.Ok(value);
        Out<int>? capturedResult = null;

        result.Match(
            success: r => capturedResult = r,
            fail: _ => { }
        );

        Assert.That(capturedResult, Is.Not.Null);
        Assert.That(capturedResult!.Value, Is.EqualTo(value));
    }

    [Test]
    public void FromResultTShouldCopyAllProperties() {
        const int value = 42;
        const string message = "Original message";
        var exception = new Exception("Original exception");
        var original = Out<int>.Fail(value, message, exception);

        var copy = Out<int>.From(original);

        using (Assert.EnterMultipleScope()) {
            Assert.That(copy.Success, Is.EqualTo(original.Success));
            Assert.That(copy.Value, Is.EqualTo(original.Value));
            Assert.That(copy.Message, Is.EqualTo(original.Message));
            Assert.That(copy.Exception, Is.EqualTo(original.Exception));
        }
    }

    [Test]
    public void FromResultShouldCopyPropertiesWithoutValue() {
        const string message = "Original message";
        var exception = new Exception("Original exception");
        var original = Out.Fail(exception, message);

        var copy = Out<int>.From(original);

        using (Assert.EnterMultipleScope()) {
            Assert.That(copy.Success, Is.EqualTo(original.Success));
            Assert.That(copy.Value, Is.EqualTo(0));
            Assert.That(copy.Message, Is.EqualTo(original.Message));
            Assert.That(copy.Exception, Is.EqualTo(original.Exception));
        }
    }

    [Test]
    public void ImplicitConversionFromValueShouldCreateSuccessResult() {
        const int value = 42;
        Out<int> @out = value;

        using (Assert.EnterMultipleScope()) {
            Assert.That(@out.Success, Is.True);
            Assert.That(@out.Value, Is.EqualTo(value));
        }
    }

    [Test]
    public void ImplicitConversionFromExceptionShouldCreateFailureResult() {
        var exception = new InvalidOperationException("Test exception");
        Out<int> @out = exception;

        using (Assert.EnterMultipleScope()) {
            Assert.That(@out.Success, Is.False);
            Assert.That(@out.Exception, Is.EqualTo(exception));
        }
    }

    [Test]
    public void ImplicitConversionFromNullExceptionShouldCreateFailureResult() {
        Exception? exception = null;
        Out<int> @out = exception;

        using (Assert.EnterMultipleScope()) {
            Assert.That(@out.Success, Is.False);
            Assert.That(@out.Exception, Is.Null);
        }
    }

    [Test]
    public void WithComplexTypeShouldWorkWithCustomObjects() {
        var person = new Person { Name = "John", Age = 30 };
        var result = Out<Person>.Ok(person);

        using (Assert.EnterMultipleScope()) {
            Assert.That(result.Success, Is.True);
            Assert.That(result.Value, Is.EqualTo(person));
        }

        using (Assert.EnterMultipleScope()) {
            Assert.That(result.Value?.Name, Is.EqualTo("John"));
            Assert.That(result.Value?.Age, Is.EqualTo(30));
        }
    }

    [Test]
    public void WithNullableValueTypeShouldHandleNullCorrectly() {
        int? value = null;
        var result = Out<int?>.Ok(value);

        using (Assert.EnterMultipleScope()) {
            Assert.That(result.Success, Is.True);
            Assert.That(result.Value, Is.Null);
        }
    }

    [Test]
    public void ImplicitConversionFromNullValueShouldCreateSuccessResultWithNull() {
        string? value = null;
        Out<string?> @out = value;

        using (Assert.EnterMultipleScope()) {
            Assert.That(@out.Success, Is.True);
            Assert.That(@out.Value, Is.Null);
        }
    }

    [Test]
    public void ImplicitConversionFromResultShouldCreateSuccessResult() {
        const string message = "Test message";
        var res = Out.Ok(message);
        Out<string?> @out = res;

        using (Assert.EnterMultipleScope()) {
            Assert.That(@out.Success, Is.True);
            Assert.That(@out.Exception, Is.Null);
            Assert.That(@out.Message, Is.EqualTo(message));
            Assert.That(@out.Value, Is.Null);
        }
    }

    [Test]
    public void ExplicitConversionToResultShouldCreateSuccessResult() {
        const string message = "Test message";
        var res = Out<int>.Ok(42, message);
        var result = (Out)res;

        using (Assert.EnterMultipleScope()) {
            Assert.That(result.Success, Is.True);
            Assert.That(result.Exception, Is.Null);
            Assert.That(result.Message, Is.EqualTo(message));
        }
    }

    [Test]
    public void WithWithValueOnlyShouldSetValue() {
        const int value = 42;
        var result = Out<int>.Empty.With(value);

        using (Assert.EnterMultipleScope()) {
            Assert.That(result.Value, Is.EqualTo(value));
            Assert.That(result.Message, Is.Null);
        }
    }

    [Test]
    public void MessagePropertySetterShouldWork() {
        var result = Out<int>.Empty;
        const string message = "Direct set message";

        result.Message = message;

        Assert.That(result.Message, Is.EqualTo(message));
    }

    [Test]
    public void ExceptionPropertySetterShouldWork() {
        var result = Out<int>.Empty;
        var exception = new Exception("Direct set exception");

        result.Exception = exception;

        Assert.That(result.Exception, Is.EqualTo(exception));
    }

    [Test]
    public void ValuePropertySetterShouldWork() {
        var result = Out<int>.Empty;
        const int value = 100;

        result.Value = value;

        Assert.That(result.Value, Is.EqualTo(value));
    }

    [Test]
    public void FromSuccessfulResultTShouldCopySuccessState() {
        const int value = 42;
        const string message = "Success message";
        var original = Out<int>.Ok(value, message);

        var copy = Out<int>.From(original);

        using (Assert.EnterMultipleScope()) {
            Assert.That(copy.Success, Is.True);
            Assert.That(copy.Value, Is.EqualTo(value));
            Assert.That(copy.Message, Is.EqualTo(message));
        }
    }

    [Test]
    public void FromSuccessfulResultShouldCopySuccessState() {
        const string message = "Success message";
        var original = Out.Ok(message);

        var copy = Out<int>.From(original);

        using (Assert.EnterMultipleScope()) {
            Assert.That(copy.Success, Is.True);
            Assert.That(copy.Message, Is.EqualTo(message));
        }
    }

    private class Person {
        public string Name { get; init; } = string.Empty;
        public int Age { get; init; }
    }
}
