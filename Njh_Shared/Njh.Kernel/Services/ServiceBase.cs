// Copyright (c) NJH. All rights reserved.

namespace Njh.Kernel.Services
{
    using System;

    public abstract class ServiceBase
       : IService
    {
        ~ServiceBase()
        {
            // Calling in the destructor, in case a derived class has unmanaged resources
            this.Dispose(false);
        }

        /// <summary>Dispose.</summary>
        public void Dispose()
        {
            // Dispose of managed and unmanaged resources
            this.Dispose(true);

            // This is necessary in case the child class has a destructor/finalizer defined on it,
            // to signal to GC that all resources have already been disposed of here and the object
            // shouldn't go into the finalizer queue
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(
            bool isDisposing)
        {
            // Don't have any unmanaged resources, so do nothing.
            // If any of the child classes, do have unmanaged resources to be disposed of,
            // they should override this method.
        }
    }
}
