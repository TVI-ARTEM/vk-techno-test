using Microsoft.AspNetCore.Mvc;

namespace Users.API.ActionFilters;

internal sealed class ResponseTypeAttribute : ProducesResponseTypeAttribute
{
    public ResponseTypeAttribute(int statusCode)
        : base(typeof(ErrorResponse), statusCode)
    {
    }
}