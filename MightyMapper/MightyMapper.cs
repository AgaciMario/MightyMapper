using System.Reflection;

namespace MightyMapper
{
    public class MightyMapper<F, T> where F : class where T : class, new()
    {
        public List<MightyMapperRule<F, T>> MightyMapperRuleList { get; set; } = new List<MightyMapperRule<F, T>>();

        public T Map(F from)
        {
            if (from == null)
                return null;

            Dictionary<string, dynamic> hashTable = new Dictionary<string, dynamic>();
            foreach (PropertyInfo property in from.GetType().GetProperties())
            {
                hashTable.Add(property.Name + property.PropertyType.ToString(), property.GetValue(from));
            }

            T to = new T();
            foreach (PropertyInfo property in to.GetType().GetProperties())
            {
                if (MightyMapperRuleList.Any(x => x.GetDestinationPropertyInfo() == property))
                    continue;

                if (hashTable.TryGetValue(property.Name + property.PropertyType.ToString(), out var value))
                    property.SetValue(to, value);
            }

            foreach (MightyMapperRule<F, T> rule in MightyMapperRuleList)
            {
                if (rule.DoNothing)
                    continue;

                PropertyInfo destProperty = rule.GetDestinationPropertyInfo();
                PropertyInfo sourceProperty = rule.GetSourcePropertyInfo();

                bool isMemberAcess = sourceProperty != null;

                if (isMemberAcess)
                {
                    destProperty.SetValue(to, sourceProperty.GetValue(from));
                    continue;
                }
                else
                {
                    var expression  = rule.Source.Compile();
                    destProperty.SetValue(to, expression(from));
                }
            }

            return to;
        }
    }
}

