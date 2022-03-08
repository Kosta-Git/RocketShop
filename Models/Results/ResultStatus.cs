namespace Models.Results;

public enum ResultStatus
{
    Ok = 200,
    InternalError = 500,
    NotFound = 404,
    InvalidInput = 400,
    AlreadyExists = 400,
    UnknownError = 500
}