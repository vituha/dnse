using System;

namespace VS.Library.UT.Automapper
{
    public interface IEntityWithKey<out T>
    {
        T Id { get; }
    }
}