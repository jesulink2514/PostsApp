using System;
using System.Collections.Generic;
using System.Text;

namespace PostsApp.Services
{
    public interface IApi<T>
    {
        T UserInitiated { get; }
        T Background { get; }
        T Speculative { get; }
        T Offline { get; }
    }
}
