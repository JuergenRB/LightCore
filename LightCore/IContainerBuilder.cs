﻿using System;

using LightCore.Fluent;

namespace LightCore
{
    /// <summary>
    /// Represents a builder that is reponsible for accepting, validating registrations
    /// and builds the container with that registrations.
    /// </summary>
    public interface IContainerBuilder
    {
        /// <summary>
        /// Builds the container.
        /// </summary>
        /// <returns>The builded container.</returns>
        IContainer Build();

        /// <summary>
        /// Registers a module with registrations.
        /// </summary>
        /// <param name="module">The module.</param>
        void RegisterModule(RegistrationModule module);

        /// <summary>
        /// Registers a contract with an existing instance.
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <returns>An instance of <see cref="IFluentRegistration"  /> that exposes fluent registration.</returns>
        IFluentRegistration Register<TContract>(TContract instance);

        /// <summary>
        /// Registers a contract with an activatorFunction function.
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <param name="activatorFunction">The activator as function..</param>
        /// <returns>An instance of <see cref="IFluentRegistration"  /> that exposes fluent registration.</returns>
        IFluentRegistration Register<TContract>(Func<IContainer, TContract> activatorFunction);

        /// <summary>
        /// Registers a contract with its implementationtype.
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation for the contract</typeparam>
        /// <returns>An instance of <see cref="IFluentRegistration"  /> that exposes fluent registration.</returns>
        IFluentRegistration Register<TContract, TImplementation>() where TImplementation : TContract;

        /// <summary>
        /// Registers a contract with its implementationtype.
        /// </summary>
        /// <param name="typeOfContract">The type of the contract.</param>
        /// <param name="typeOfImplementation">The type of the implementation for the contract</param>
        /// <returns>An instance of <see cref="IFluentRegistration"  /> that exposes fluent registration.</returns>
        IFluentRegistration Register(Type typeOfContract, Type typeOfImplementation);
    }
}