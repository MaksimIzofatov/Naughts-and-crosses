using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using X_O.XOBLL.Enums;

namespace X_O
{
    public partial class Load : Form
    {
        public Load()
        {
            InitializeComponent();
        }

        public void ShowResult(string result, GameMode mode)
        {
            Text = mode.ToString();
            label1.Text = result;
        }
    }
}
