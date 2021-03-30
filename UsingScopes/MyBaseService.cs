using System;
using System.Threading.Tasks;

namespace UsingScopes
{
    public abstract class MyBaseService
    {
        public Guid Id { get; set; }

        public MyBaseService()
        {
            Id = Guid.NewGuid();
        }

        public abstract Task RunAsync();
    }
}