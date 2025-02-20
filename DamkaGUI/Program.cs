using System.Runtime.CompilerServices;

namespace DamkaGUI
{
    internal class Program
    {
        public static void Main()
        {
            FormSettings settingsForm = new FormSettings();
            FormDamka damkaForm = new FormDamka();

            settingsForm.ShowDialog();
            FormSettings.Settings gameSettings = settingsForm.GetSettings();
            if (settingsForm.m_IsInistializedProperly)
            {
                damkaForm.AddSettings(gameSettings);
                damkaForm.ShowDialog();
            }
        }
    }
}
