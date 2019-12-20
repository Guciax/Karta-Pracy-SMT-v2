using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karta_Pracy_SMT_v2
{
    class PanelDoubleBuffered : Panel
    {
        public PanelDoubleBuffered()
        {
            this.DoubleBuffered = true;
        }
    }
}
