
using System;

namespace UsingScopes
{
    internal class MyBaseService
    {
        public Guid Id { get; set; }

        public MyBaseService()
        {
            Id = Guid.NewGuid();
        }
    }
}