﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using LightCore.ExtensionMethods.LightCore.Registration;
using LightCore.Registration;

namespace LightCore.Activation.Components
{
    /// <summary>
    /// Represents the constructor search stratetgy.
    /// </summary>
    internal class ConstructorSelector
    {
        /// <summary>
        /// Selects the right constructor for current context.
        /// </summary>
        /// <param name="constructors">The constructors.</param>
        /// <param name="resolutionContext">The resolution context.</param>
        /// <returns></returns>
        public ConstructorInfo SelectConstructor(IEnumerable<ConstructorInfo> constructors, ResolutionContext resolutionContext)
        {
            var constructorsWithParameters = constructors.OrderByDescending(constructor => constructor.GetParameters().Length);

            ConstructorInfo finalConstructor = constructorsWithParameters.Last();

            if (constructorsWithParameters.Count() == 1)
            {
                return finalConstructor;
            }

            // Loop througth all constructors, from the most to the least parameters.
            foreach (ConstructorInfo constructorCandidate in constructorsWithParameters)
            {
                ParameterInfo[] parameters = constructorCandidate.GetParameters();
                var dependencyParameters = parameters.Where(p => resolutionContext.Registrations.IsRegisteredAsAnything(p.ParameterType));

                if (resolutionContext.Arguments == null)
                {
                    resolutionContext.Arguments = new ArgumentContainer();
                }

                if (resolutionContext.RuntimeArguments == null)
                {
                    resolutionContext.RuntimeArguments = new ArgumentContainer();
                }

                // Parameters and registered dependencies match.
                if (resolutionContext.Arguments.CountOfAllArguments + resolutionContext.RuntimeArguments.CountOfAllArguments == 0 && parameters.Length == dependencyParameters.Count())
                {
                    finalConstructor = constructorCandidate;
                    break;
                }

                if (resolutionContext.Arguments.CountOfAllArguments > 0 || resolutionContext.RuntimeArguments.CountOfAllArguments > 0)
                {
                    if (resolutionContext.Arguments.CountOfAllArguments + resolutionContext.RuntimeArguments.CountOfAllArguments >= parameters.Count() - dependencyParameters.Count())
                    {
                        bool canSupply = true;

                        foreach (ParameterInfo parameter in parameters)
                        {
                            bool dependenciesCanSupplyValue = dependencyParameters.Contains(parameter);
                            bool argumentsCanSupplyValue = resolutionContext.Arguments.CanSupplyValue(parameter);
                            bool runtimeArgumentsCanSupplyValue = resolutionContext.RuntimeArguments.CanSupplyValue(parameter);

                            if (!(dependenciesCanSupplyValue || argumentsCanSupplyValue || runtimeArgumentsCanSupplyValue))
                            {
                                canSupply = false;
                            }
                        }

                        if (canSupply)
                        {
                            finalConstructor = constructorCandidate;
                            break;
                        }
                    }
                }
            }

            return finalConstructor;
        }
    }
}