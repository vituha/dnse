namespace VS.Library.Pattern.Lifetime
{
    public interface IScope
    {
        /// <summary>
        /// Notifies this object that it goes into scope
        /// </summary>
        void Enter();

        /// <summary>
        /// Notifies this object that it goes out of scope
        /// </summary>
        void Exit();
    }
}
