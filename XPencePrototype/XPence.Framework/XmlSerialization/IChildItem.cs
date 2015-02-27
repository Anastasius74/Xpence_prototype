
namespace XPence.Framework.XmlSerialization
{
    /// <summary>
    /// Defines the contract for an object that has a parent object
    /// </summary>
    /// <typeparam name="TP">Type of the parent object</typeparam>
    public interface IChildItem<TP> where TP : class
    {
        TP Parent { get; set; }
    }

}
