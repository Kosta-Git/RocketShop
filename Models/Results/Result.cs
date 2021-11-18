namespace Models.Results
{
    public class Result
    {
        protected Result( bool success, string error, ResultStatus code )
        {
            Success = success;
            Failure = !success;
            Error   = error;
            Status  = code;
        }

        public bool Success { get; }
        public bool Failure { get; }
        public string Error { get; }
        public ResultStatus Status { get; set; }

        public static Result Fail( string message, ResultStatus status = ResultStatus.InternalError )
        {
            return new Result( false, message, status );
        }

        public static Result<T> Fail<T>( string message, ResultStatus status = ResultStatus.InternalError )
        {
            return new Result<T>( false, message, status );
        }

        public static Result Ok( ResultStatus status = ResultStatus.Ok )
        {
            return new Result( true, string.Empty, status );
        }

        public static Result<T> Ok<T>( T value, ResultStatus status = ResultStatus.Ok )
        {
            return new Result<T>( value, true, string.Empty, status );
        }
    }

    public class Result<T> : Result
    {
        public T Value;

        protected internal Result( T value, bool success, string error, ResultStatus status ) :
            base( success, error, status )
        {
            Value = value;
        }

        protected internal Result( bool success, string error, ResultStatus status ) : base( success, error, status )
        {
            Value = default!;
        }
    }
}