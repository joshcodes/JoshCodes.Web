using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Routing;

using JoshCodes.Web.Attributes;

namespace JoshCodes.Web.Routing.Extensions
{
    public static class RoutingExtensions
    {
        /// <summary>
        /// Gets the route values for controller / action from a method call expression.
        /// </summary>
        /// <returns>
        /// The route values with controller and action populated.
        /// </returns>
        /// <param name='methodExpression'>
        /// Method expression for calling a controller endpoint.
        /// </param>
        public static RouteValueDictionary GetRouteValues(this MethodCallExpression methodExpression)
        {
            var routeValues = methodExpression.Method.GetRouteValues((paramName, paramIndex) => {
                // Get the argument expression associated with the parameter
                var paramExpr = methodExpression.Arguments[paramIndex];

                // retrieve the value of the parameter
                var value = Expression.Lambda(paramExpr).Compile().DynamicInvoke();

                return value;
            });

            routeValues["controller"] = methodExpression.Object.Type.Name.Replace("Controller", string.Empty);

            return routeValues;
        }

        public static RouteValueDictionary GetRouteValues(this System.Reflection.MethodInfo method, ParameterLookupDelegate resolveParam = null)
        {
            var routeValues = new RouteValueDictionary();

            // does not work for interfaced actions as declaring type is interface - this is the best we can do with System.Reflection.MethodInfo though
            routeValues.Add("controller", method.DeclaringType.Name.Replace("Controller", string.Empty));
            routeValues.Add("action", method.Name);
            routeValues.Add("id", null); // clear id value before parsing params

            if(resolveParam != null)
            {
                var acceptVerbsByParameterAttributes = (AcceptVerbsByParameterAttribute[]) method.GetCustomAttributes(typeof(AcceptVerbsByParameterAttribute), true);

                // populate route values from method arguments
                int argIndex = 0;
                foreach(var paramInfo in method.GetParameters())
                {
                    var parameterName = paramInfo.Name;
                    var value = resolveParam.Invoke(parameterName, argIndex);
                    var parameterAttribute = acceptVerbsByParameterAttributes.GetAcceptVerbsByParameterAttributeForParameter(parameterName);
                        
                    if(parameterAttribute == null || parameterAttribute.ShouldUseParameterInActionUri())
                    {
                        // update route values
                        if(routeValues.ContainsKey(parameterName))
                        {
                            routeValues[parameterName] = value;
                        } else
                        {
                            routeValues.Add(parameterName, value);
                        }
                    }

                    argIndex++;
                }
            }

            return routeValues;
        }
    }
}
