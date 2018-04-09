using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawLines
{
    public partial class NoFlickerPanel : Panel
    {
        public NoFlickerPanel() : base()
        {
            DoubleBuffered = true;
        }
    }
}
