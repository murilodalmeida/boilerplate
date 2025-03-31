using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FwksLabs.Libs.AspNetCore.Abstractions;

public interface IExceptionHandlerService
{
    Task HandleAsync<TException>(HttpContext context, TException exception)
        where TException : Exception;
}