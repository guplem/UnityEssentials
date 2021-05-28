namespace Essentials.EssentialsSettings
{
    public interface IAdjustment
    {
        string title { get; }
        string applyButtonText { get; }
        string revertButtonText { get; }
        void Apply();
        void Revert();
        string applyAdjustmentShortEplanation { get; }
        string revertAdjustmentShortEplanation { get; }

        string infoButtonText { get; }
        public abstract string infoURL { get; }
        public bool showInSettingsWindow { get; }
        void OpenInfoURL();
    }
}