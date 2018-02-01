using System;
using System.Runtime.Serialization;

namespace JX.Customer
{
    [DataContract]
    public class CustomerDomain
    {
        public CustomerDomain(Guid id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }

        [DataMember]
        public readonly Guid Id;

        [DataMember]
        public readonly string Name;

        [DataMember]
        public readonly int Age;
    }
}
