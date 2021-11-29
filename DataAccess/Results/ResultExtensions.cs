using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DataAccess.Results;

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

    public static ActionResult ToActionResult( this Result result )
    {
        return string.IsNullOrEmpty( result.Error )
            ? new ObjectResult( null )
            {
                StatusCode = ( int )result.Status
            }
            : new ObjectResult( result.Error )
            {
                StatusCode = ( int )result.Status
            };
    }

    public static ActionResult<T> ToActionResult<T>( this Result<T> result )
    {
        if ( result.Failure )
            return string.IsNullOrEmpty( result.Error )
                ? new ObjectResult( null )
                {
                    StatusCode = ( int )result.Status
                }
                : new ObjectResult( result.Error )
                {
                    StatusCode = ( int )result.Status
                };

        return new ObjectResult( result.Value )
        {
            StatusCode = ( int )result.Status
        };
    }

    public static async Task<ActionResult> ToActionResult( this Task<Result> result )
    {
        var res = await result;

        return string.IsNullOrEmpty( res.Error )
            ? new ObjectResult( null )
            {
                StatusCode = ( int )res.Status
            }
            : new ObjectResult( res.Error )
            {
                StatusCode = ( int )res.Status
            };
    }

    public static async Task<ActionResult<T>> ToActionResult<T>( this Task<Result<T>> result )
    {
        var res = await result;

        if ( res.Failure )
            return string.IsNullOrEmpty( res.Error )
                ? new ObjectResult( null )
                {
                    StatusCode = ( int )res.Status
                }
                : new ObjectResult( res.Error )
                {
                    StatusCode = ( int )res.Status
                };

        return new ObjectResult( res.Value )
        {
            StatusCode = ( int )res.Status
        };
    }
}