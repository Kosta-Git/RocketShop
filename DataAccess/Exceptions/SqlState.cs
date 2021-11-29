using System;

namespace DataAccess.Exceptions;

public class SqlState
{
    public static SqlException Parse( string state )
    {
        if ( !int.TryParse( state, out var errorCode ) ) return SqlException.Unknown;

        try
        {
            return ( SqlException )errorCode;
        }
        catch ( Exception )
        {
            return SqlException.Unknown;
        }
    }
}