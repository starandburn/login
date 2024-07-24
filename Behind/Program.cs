using Nkk.IT.Trial.Programing.Login.Services;
using Nkk.IT.Trial.Programing.Login.Views;

namespace Nkk.IT.Trial.Programing.Login.Behind
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            try
            {
                Application.Run(new LoginWindow());
            }
            catch (Exception ex)
            {
                MessageService.ShowException(ex);
            }
        }
    }
}