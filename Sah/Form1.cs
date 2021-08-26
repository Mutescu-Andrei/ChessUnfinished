using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sah
{
    public partial class Form1 : Form
    {
        Image img;
        Graphics g;
        JocSah joc;
        Piesa piesa;
        int dx, dy;
        bool apasat;
        public Form1()
        {
            InitializeComponent();
        }
        private void iesireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            img = new Bitmap(p.Width, p.Height);
            g = Graphics.FromImage(img);
            joc = new JocSah(p.Width, p.Height, g);
            apasat = false;
            joc.jocNou();
            p.Refresh();
        }
        private void p_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(img, 0, 0);
        }
        private void nouToolStripMenuItem_Click(object sender, EventArgs e)
        {
            joc.jocNou();
            p.Refresh();
        }
        private void p_MouseDown(object sender, MouseEventArgs e)
        {
            piesa = joc.da_piesa(e.X, e.Y);
            if (piesa != null)
            {
                apasat = true;
                dx = e.X - piesa.X;
                dy = e.Y - piesa.Y;
                Cursor = Cursors.Hand;
            }
        }
        private void p_MouseUp(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Default;
            if (apasat)
            {
                apasat = false;
                joc.aseaza_piesa(piesa, e.X, e.Y);
                joc.deseneaza();
                p.Refresh();
            }
        }

        private void p_MouseMove(object sender, MouseEventArgs e)
        {
            if (apasat)
            {
                piesa.X = e.X - dx;
                piesa.Y = e.Y - dy;
                joc.deseneaza();
                p.Refresh();
            }
        }
    }
}
