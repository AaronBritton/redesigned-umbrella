namespace Britton.Stock
{
    /// <summary>
    /// The base attached property to replace the vanilla WPF attached property
    /// </summary>
    /// <typeparam name="Parent">The parent class to be the attached property</typeparam>
    /// <typeparam name="Property">The type of this attached property</typeparam>
    public abstract class BaseAttachedProperty<Parent, Property>
        where Parent : new()
    {
        #region Public Properties

        public static Parent Instance { get; private set; } = new Parent();

        #endregion
    }
}
