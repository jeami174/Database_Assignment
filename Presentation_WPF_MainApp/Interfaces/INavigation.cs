namespace Presentation_WPF_MainApp.Interfaces
{
    public interface INavigation
    {
        /// <summary>
        /// Navigerar till listan över projekt.
        /// </summary>
        void ShowProjectList();

        /// <summary>
        /// Navigerar asynkront till sidan för att redigera ett projekt.
        /// </summary>
        /// <param name="projectId">Id för projektet som ska redigeras.</param>
        void ShowEditProject(int projectId);

        /// <summary>
        /// Navigerar till sidan för att skapa ett nytt projekt.
        /// </summary>
        void ShowCreateProject();
    }
}


