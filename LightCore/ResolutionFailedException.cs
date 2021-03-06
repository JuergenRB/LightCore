﻿using System;

namespace LightCore
{
    ///<summary>
    /// Thrown when resolving of a type failed.
    ///</summary>
    public class ResolutionFailedException : Exception
    {
        /// <summary>
        /// Gets the implementationtype which was failed to construct.
        /// </summary>
        public Type ImplementationType
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolutionFailedException"/> type.
        /// </summary>
        public ResolutionFailedException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolutionFailedException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public ResolutionFailedException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolutionFailedException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="implementationType">The implementationtype.</param>
        public ResolutionFailedException(string message, Type implementationType)
            :base(message)
        {
            ImplementationType = implementationType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolutionFailedException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception</param>
        public ResolutionFailedException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

    }
}