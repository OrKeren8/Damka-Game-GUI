using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DamkaGUI
{
    internal class ScoreLabel : Label
    {
        public int Score { get; private set; } = 0;

    }
}
