using ABB.AC800PEC.DbConfigTool.Infrastructure.BaseClasses;
using ABB.AC800PEC.DbConfigTool.Shared;

namespace ABB.AC800PEC.DbConfigTool.Models
{
    /// <summary>
    /// A model class for user.
    /// </summary>
    public class User : ModelBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the name of a user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the selected theme by the user.
        /// </summary>
        public string SelectedTheme { get; set; }

        /// <summary>
        /// Gets or sets the selected accent by the user.
        /// </summary>
        public string SelectedAccent { get; set; }

        #endregion

        #region Overriden members

        /// <summary>
        /// Validates the property values against their names.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected override string GetErrorForProperty(string propertyName)
        {
            string error = null;
            switch (propertyName)
            {
                case "Name":
                    if (string.IsNullOrEmpty(Name))
                        error = ErrorMessages.ERR_NAME_CANNOT_BE_EMPTY;
                    else if (Name.Length > 30)
                        error = ErrorMessages.ERR_NAME_CANNOT_BE_LONG;
                    break;
            }

            return error;
        }

        #endregion
    }
}
