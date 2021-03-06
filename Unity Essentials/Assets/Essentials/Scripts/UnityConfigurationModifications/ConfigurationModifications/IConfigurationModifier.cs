using UnityEngine;

public interface IConfigurationModifier
{
    string title { get; }
    string applyButtonText { get; }
    string revertButtonText { get; }
    void Apply();
    void Revert();
}