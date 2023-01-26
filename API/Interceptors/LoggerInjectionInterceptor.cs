﻿using EfCore7.Interfaces;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EfCore7.Interceptors;

public class LoggerInjectionInterceptor : IMaterializationInterceptor
{
    private ILogger? _logger;

    public object InitializedInstance(MaterializationInterceptionData materializationData, object instance)
    {
        if (instance is IHasLogger hasLogger)
        {
            _logger ??= materializationData.Context.GetService<ILoggerFactory>().CreateLogger("CustomersLogger");
            hasLogger.Logger = _logger;
        }

        return instance;
    }
}