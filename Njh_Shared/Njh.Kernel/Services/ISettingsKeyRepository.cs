// Copyright (c) NJH. All rights reserved.

namespace Njh.Kernel.Services
{
    using Njh.Kernel.Repositories;

    public interface ISettingsKeyRepository
        : IRepository
    {
        T GetValue<T>(string keyName, string culture = null, string siteName = null);
    }
}