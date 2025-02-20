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

        private int m_Score = 0;
        public string Name = "";
        public int Score
        {
            get { return m_Score; }
            set
            {
                m_Score = value;
                this.Text = $"{this.Name}: {this.Score}";
            }
        }
    }
}
