namespace Essentials.EssentialsSettings
{
    public interface IModification
    {
        string title { get; }
        string applyButtonText { get; }
        string revertButtonText { get; }
        void Apply();
        void Revert();
        string applyModificationShortEplanation { get; }
        string revertModificationShortEplanation { get; }

        string infoButtonText { get; }
        public abstract string infoURL { get; }
        public bool showInSettingsWindow { get; }
        void OpenInfoURL();
    }
}