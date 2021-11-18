using System;
using FluentAssertions;
using Models.Results;
using Xunit;

namespace UnitTests
{
    public class ResultTests
    {
        [Fact( DisplayName = "OK Result with string value" )]
        public void ResultOkBasic()
        {
            var result = Result.Ok( "Hello, world!" );

            result.Value.Should().Be( "Hello, world!" );
            result.Success.Should().BeTrue();
            result.Failure.Should().BeFalse();
            result.Status.Should().Be( ResultStatus.Ok );
            result.Error.Should().BeEmpty();
        }

        [Fact( DisplayName = "Fail Result with error message and no value" )]
        public void ResultFailBasic()
        {
            var result = Result.Fail( "Here is an error" );

            result.Success.Should().BeFalse();
            result.Failure.Should().BeTrue();
            result.Status.Should().Be( ResultStatus.InternalError );
            result.Error.Should().Be( "Here is an error" );
        }
    }
}