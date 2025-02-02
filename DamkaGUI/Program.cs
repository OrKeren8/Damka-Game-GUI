namespace DamkaGUI
{
    internal class Program
    {
        public static void Main()
        {
            FormSettings m_SettingsForm = new FormSettings();
            FormDamka m_DamkaForm = new FormDamka();

            m_SettingsForm.ShowDialog();
            m_DamkaForm.ShowDialog(m_SettingsForm);

        }
    }
}
