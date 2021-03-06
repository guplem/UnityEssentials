using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConfigurationMofification : IConfigurationModifier
{
    /// <summary>
    /// Title or short definition of the configuration modification
    /// </summary>
    public abstract string title { get; }
    
    /// <summary>
    /// Applies the desired modification.
    /// </summary>
    public abstract void Apply();

    /// <summary>
    /// Reverts the modification leaving the state of the platform as it was before applying it.
    /// </summary>
    public abstract void Revert();

    /// <summary>
    /// Text displayed on the button to apply the modification
    /// </summary>
    public abstract string applyButtonText { get; }
    /// <summary>
    /// Text displayed on the button to revert the modification
    /// </summary>
    public abstract string revertButtonText { get; }
    
}
