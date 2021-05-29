namespace Essentials.EssentialsSettings
{
    /// <summary>
    /// Interface integrating all the basic elements that an adjustment that can be managed in the "Essentials Settings Window" must have.
    /// </summary>
    public interface IAdjustment
    {
        string title { get; }
        string applyButtonText { get; }
        string revertButtonText { get; }
        void Apply();
        void Revert();
        string applyAdjustmentShortExplanation { get; }
        string revertAdjustmentShortExplanation { get; }

        string infoButtonText { get; }
        public abstract string infoURL { get; }
        public bool showInSettingsWindow { get; }
        void OpenInfoURL();
    }
}