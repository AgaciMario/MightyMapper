using System.Linq.Expressions;
using System.Reflection;

namespace MightyMapper
{
    public class MightyMapperRule<F, T>(Expression<Func<F, dynamic>> sourceProperty, Expression<Func<T, dynamic>> destinationProperty, bool DoNothing = false) where F : class where T : class, new()    
    {
        public Expression<Func<F, dynamic>> Source { get; set; } = sourceProperty;

        public Expression<Func<T, dynamic>> Destination { get; set; } = destinationProperty;

        public bool DoNothing { get; set; } = DoNothing;

        public PropertyInfo GetSourcePropertyInfo()
        {
            if (Source == null)
                return null;

            if (Source.Body is MemberExpression memberExpression)
            {
                if (memberExpression.Member is PropertyInfo propertyInfo)
                {
                    return propertyInfo;
                }
            }
            else if (Source.Body is UnaryExpression unaryExpression)
            {
                if (unaryExpression.Operand is MemberExpression innerMemberExpression)
                {
                    if (innerMemberExpression.Member is PropertyInfo propertyInfo)
                    {
                        return propertyInfo;
                    }
                }
            }

            // Expressão não representa o acesso a uma propriedade
            return null;
        }

        public PropertyInfo GetDestinationPropertyInfo()
        {
            if (Destination == null)
                return null;

            if (Destination.Body is MemberExpression memberExpression)
            {
                if (memberExpression.Member is PropertyInfo propertyInfo)
                {
                    return propertyInfo;
                }
            }
            else if (Destination.Body is UnaryExpression unaryExpression)
            {
                if (unaryExpression.Operand is MemberExpression innerMemberExpression)
                {
                    if (innerMemberExpression.Member is PropertyInfo propertyInfo)
                    {
                        return propertyInfo;
                    }
                }
            }
             
            // Expressão não representa o acesso a uma propriedade
            return null;
        }
    }
}

