using XPence.Models;

namespace XPence.ViewModels
{
    /// <summary>
    /// A data holder class for the aplication.
    /// </summary>
    public static class AppData
    {
        /// <summary>
        /// Gets or sets the <see cref="User"/> instance who is entred in into the application.
        /// </summary>
        public static User ApplicationUser { get; set; }
    }
}
