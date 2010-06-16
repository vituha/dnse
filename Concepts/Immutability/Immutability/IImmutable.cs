namespace Immutability
{
    public interface IImmutable
    {
        IImmutableKey Lock();

        bool IsImmutable { get; }
    }
}
