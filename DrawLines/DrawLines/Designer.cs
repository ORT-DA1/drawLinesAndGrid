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
    public partial class Designer : Form
    {

        private Bitmap gridLayer;
        private Bitmap currentLineLayer;
        private Bitmap linesLayer;
        private Point? start;
        private List<Point> points;
        private int gridCellCount;

        public Designer()
        {
            InitializeComponent();
            gridLayer = new Bitmap(drawSurface.Width, drawSurface.Height);
            currentLineLayer = new Bitmap(drawSurface.Width, drawSurface.Height);
            linesLayer = new Bitmap(drawSurface.Width, drawSurface.Height);
            points = new List<Point>();
            gridCellCount = 20;
            PaintGrid();
        }

        private void PaintGrid()
        {
          
            using (Graphics graphics = Graphics.FromImage(gridLayer))
            {
                for (int i = 0; i < gridCellCount; i++)
                {
                    DrawHorizontalAndVerticalLines(graphics, i);
                }
            }
            drawSurface.Invalidate();
        }

        private void DrawHorizontalAndVerticalLines(Graphics graphics, int axis)
        {
            int gridCellWidth = axis * drawSurface.Width / gridCellCount;
            int gridCellHeight = axis * drawSurface.Height / gridCellCount;
            graphics.DrawLine(Pens.Gray, gridCellWidth, 0, gridCellWidth, drawSurface.Height);
            graphics.DrawLine(Pens.Gray, 0, gridCellHeight, drawSurface.Width, gridCellHeight);
        }

      

        private void drawSurface_Paint(object sender, PaintEventArgs e)
        {
            Point zeroing = new Point(0, 0);
            e.Graphics.DrawImage(gridLayer, zeroing);
            e.Graphics.DrawImage(currentLineLayer, zeroing);
            e.Graphics.DrawImage(linesLayer, zeroing);
        }

        private void drawSurface_MouseClick(object sender, MouseEventArgs e)
        {

            Point end = drawSurface.PointToClient(Cursor.Position);
            points.Add(end);
            start = end;
            PaintLines();
        }

        private void PaintLines()
        {
            using (Graphics graphics = Graphics.FromImage(linesLayer))
            {
                for (int i = 1; i < points.Count; i++)
                {
                    graphics.DrawLine(Pens.Red, points[i - 1], points[i]);
                }
            }
            drawSurface.Invalidate();
        }

        private void drawSurface_MouseMove(object sender, MouseEventArgs e)
        {
            PaintCurrentLine();
        }

        private void PaintCurrentLine()
        {
            if (start != null)
            {
                currentLineLayer.Dispose();
                currentLineLayer = new Bitmap(drawSurface.Width, drawSurface.Height);
                using (Graphics graphics = Graphics.FromImage(currentLineLayer))
                {
                    graphics.DrawLine(Pens.Orange, start.Value, drawSurface.PointToClient(Cursor.Position));
                }
                drawSurface.Invalidate();
            }
        }
    }
}
