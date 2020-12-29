using PvPModifier.Utilities;

namespace PvPModifier.DataStorage {

    /// <summary>
    /// Base class for all database items
    /// </summary>
    public abstract class DbObject {
        public abstract string Section { get; }
        public int ID;

        /// <summary>
        /// Sets a value to any property in the class that inherits <see cref="DbObject"/>
        /// </summary>
        /// <param name="param">Name of property/variable in the class</param>
        /// <param name="value">The value of the property</param>
        /// <returns>A boolean whether the value can be set to the property</returns>
        public bool TrySetValue(string param, string value) {
            if (MiscUtils.SetValueWithString(this, param, value)) {
                Database.Update(Section, ID, param, value);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets a string of any property in the class that inherits <see cref="DbObject"/>
        /// </summary>
        /// <param name="propertyName">The name of the property</param>
        /// <returns>A string of the value of the property</returns>
        public string GetValueWithString(string propertyName) {
            try {
                var property = GetType().GetProperty(propertyName);
                if (property == null) return string.Empty;
                return property.GetValue(this).ToString();
            } catch {
                return string.Empty;
            }
        }
    }
}