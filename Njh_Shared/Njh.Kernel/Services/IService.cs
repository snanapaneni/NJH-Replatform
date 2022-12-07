// Copyright (c) NJH. All rights reserved.

namespace Njh.Kernel.Services
{
    using System;

    /// <summary>
    /// The default interface that all services should implement,
    /// in order to be automatically handled by the DI framework.
    /// </summary>
    public interface IService
        : IDisposable
    {
    }
}
