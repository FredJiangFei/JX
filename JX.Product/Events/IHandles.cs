namespace JX.Product.Events
{
    public interface IHandles<in T>
    {
        void Handle(T message);
    }
}
