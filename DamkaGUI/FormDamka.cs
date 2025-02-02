using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace DamkaGUI
{
    internal class FormDamka : Form
    {
        private FormSettings.Settings Settings { get; set; }
        public void AddSettings(FormSettings.Settings i_Settings)
        {
            this.Settings = i_Settings;
        }
    }
}
