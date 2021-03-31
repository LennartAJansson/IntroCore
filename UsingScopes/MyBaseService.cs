using System;
using System.Threading.Tasks;

namespace UsingScopes
{
    public abstract class MyBaseService
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public abstract Task RunAsync();
    }
}