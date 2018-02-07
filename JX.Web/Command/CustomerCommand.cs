using System;
using System.Runtime.Serialization;

namespace JX.Web.Command
{
    public class CustomerCommand
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }
}
