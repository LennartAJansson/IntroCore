
using System;

namespace UsingScopes
{
    internal class InjectedService
    {
        public Guid Id { get; set; }

        public InjectedService()
        {
            Id = Guid.NewGuid();
        }
    }
}