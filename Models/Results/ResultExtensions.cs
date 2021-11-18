using System;
using System.Threading.Tasks;

namespace Models.Results
{
    public static class ResultExtensions
    {
        public static Result OnSuccess( this Result result, Func<Result> func )
        {
            return result.Failure ? result : func();
        }

        public static Result<T> OnSuccess<T>( this Result result, Func<Result<T>> func )
        {
            return result.Failure ? Result.Fail<T>( result.Error, result.Status ) : func();
        }

        public static async Task<Result<T>> OnSuccessAsync<T>(
            this Result result,
            Func<Task<T>> func,
            ResultStatus status = ResultStatus.Ok
        )
        {
            return result.Failure
                       ? Result.Fail<T>( result.Error, result.Status )
                       : Result.Ok( await func(), status );
        }

        public static async Task<Result<T>> OnSuccessAsync<T>( this Task<Result<T>> result, Action<T> func )
        {
            var res = await result;

            if ( res.Success ) func( res.Value );

            return res;
        }

        public static async Task<Result<T>> OnSuccessAsync<T>( this Result result, Func<Task<Result<T>>> func )
        {
            return result.Failure ? Result.Fail<T>( result.Error, result.Status ) : await func();
        }

        public static async Task<Result<T>> OnSuccessAsync<T>( this Task<Result> result, Func<Task<Result<T>>> func )
        {
            var res = await result;

            return res.Failure ? Result.Fail<T>( res.Error, res.Status ) : await func();
        }

        public static Result<TOut> Cast<TOut>( this Result result, Func<Result, Result<TOut>> func )
        {
            return func( result );
        }

        public static Result<TOut> Cast<TIn, TOut>( this Result<TIn> result, Func<Result<TIn>, Result<TOut>> func )
        {
            return func( result );
        }

        public static Result Cast<TIn>( this Result<TIn> result )
        {
            return result.Failure ? Result.Fail( result.Error, result.Status ) : Result.Ok( result.Status );
        }

        public static Result<T> OnFailure<T>( this Result<T> result, Action action )
        {
            if ( result.Failure ) action();

            return result;
        }

        public static Result OnFailure( this Result result, Action action )
        {
            if ( result.Failure ) action();

            return result;
        }

        public static Result OnFailure( this Result result, Action<Result> action )
        {
            if ( result.Failure )
                action( result );

            return result;
        }

        public static Result<T> OnFailure<T>( this Result<T> result, Action<Result<T>> action )
        {
            if ( result.Failure )
                action( result );

            return result;
        }

        public static Result OnBoth( this Result result, Action<Result> action )
        {
            action( result );
            return result;
        }
    }
}