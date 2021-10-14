using System.Windows;

namespace Backuper
{
    public partial class App : Application
    {
        public static Settings Settings { get; } = Settings.InitAndDeserialize();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Settings.Serialize();
            base.OnExit(e);
        }
    }
}
