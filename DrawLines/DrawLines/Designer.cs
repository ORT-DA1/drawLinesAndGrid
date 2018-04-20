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

        private NoFlickerPanel drawSurface;
        private Bitmap gridLayer;
        private Bitmap linesLayer;
        private Bitmap currentLineLayer;
        private Point start;
        private List<Point> points;

        private const int gridCellCount = 30;
        private const int cellSizeInPixels = 40;
        private const int windowXBoundriesInPixels = 20;
        private const int windowYBoundriesInPixels = 40;
        private const int drawSurfaceMaringToWindowInPixels = 10;
        private const int linesMarginToLayerInPixels = 1;


        public Designer()
        {
            points = new List<Point>();
            InitializeComponent();

            int drawSurfaceSize = cellSizeInPixels * gridCellCount;
            CreateDrawSurface(drawSurfaceSize);
            AdjustWindowSize(drawSurfaceSize);

            CreateOrRecreateLayer(ref gridLayer);
            PaintGrid();
            CreateOrRecreateLayer(ref linesLayer);
            CreateOrRecreateLayer(ref currentLineLayer);
        }

        private void CreateDrawSurface(int drawSurfaceSize)
        {
            drawSurface = new NoFlickerPanel();
            SuspendLayout();
            drawSurface.Name = "drawSurface";
            drawSurface.Location = new Point(drawSurfaceMaringToWindowInPixels, drawSurfaceMaringToWindowInPixels);
            drawSurface.Size = new Size(drawSurfaceSize, drawSurfaceSize);
            drawSurface.TabIndex = 0;
            drawSurface.Paint += new PaintEventHandler(drawSurface_Paint);
            drawSurface.MouseClick += new MouseEventHandler(drawSurface_MouseClickStart);
            Controls.Add(drawSurface);
            ResumeLayout(false);
        }

        private void AdjustWindowSize(int drawSurfaceSize)
        {
            int windowSize = drawSurfaceSize + drawSurfaceMaringToWindowInPixels * 2;
            MaximumSize = new Size(windowSize + windowXBoundriesInPixels, windowSize + windowYBoundriesInPixels);
            AutoScrollMargin = new Size(drawSurfaceMaringToWindowInPixels, drawSurfaceMaringToWindowInPixels);
        }

        private void PaintGrid()
        {

            using (Graphics graphics = Graphics.FromImage(gridLayer))
            {
                for (int i = 0; i < gridCellCount; i++)
                {
                    DrawGridHorizontalAndVerticalLines(graphics, i);
                }
                DrawGridRightAndBottomLines(graphics);
            }
            drawSurface.Invalidate();
        }

        private void DrawGridHorizontalAndVerticalLines(Graphics graphics, int axis)
        {
            DrawHorizontalAndVerticalLines(graphics, axis, 0);
        }

        private void DrawGridRightAndBottomLines(Graphics graphics)
        {
            DrawHorizontalAndVerticalLines(graphics, gridCellCount, -linesMarginToLayerInPixels);
        }


        private void DrawHorizontalAndVerticalLines(Graphics graphics, int axis, int offset)
        {
            int gridCellWidth = axis * gridLayer.Width / gridCellCount + offset;
            int gridCellHeight = axis * gridLayer.Height / gridCellCount + offset;
            graphics.DrawLine(Pens.Black, gridCellWidth, 0, gridCellWidth, gridLayer.Height);
            graphics.DrawLine(Pens.Black, 0, gridCellHeight, gridLayer.Width, gridCellHeight);
        }


        private void drawSurface_Paint(object sender, PaintEventArgs e)
        {
            Point zeroing = new Point(0, 0);
            e.Graphics.DrawImage(gridLayer, zeroing);
            e.Graphics.DrawImage(currentLineLayer, zeroing);
            e.Graphics.DrawImage(linesLayer, zeroing);
        }

        private void drawSurface_MouseClickStart(object sender, MouseEventArgs e)
        {

            Point point = AdjustPointToGrid(drawSurface.PointToClient(Cursor.Position));
            points.Add(point);
            start = point;

            drawSurface.MouseMove += new MouseEventHandler(drawSurface_MouseMove);
            drawSurface.MouseClick -= new MouseEventHandler(drawSurface_MouseClickStart);
            drawSurface.MouseClick += new MouseEventHandler(drawSurface_MouseClickEnd);
        }

        private void drawSurface_MouseClickEnd(object sender, MouseEventArgs e)
        {

            Point point = AdjustPointToGrid(drawSurface.PointToClient(Cursor.Position));
            points.Add(point);
            PaintLines();

            CreateOrRecreateLayer(ref currentLineLayer);

            drawSurface.MouseMove -= new MouseEventHandler(drawSurface_MouseMove);
            drawSurface.MouseClick += new MouseEventHandler(drawSurface_MouseClickStart);
            drawSurface.MouseClick -= new MouseEventHandler(drawSurface_MouseClickEnd);
        }

        private Point AdjustPointToGrid(Point point)
        {
            int gridCellWidth = gridLayer.Width / gridCellCount;
            int gridCellHeight = gridLayer.Height / gridCellCount;
            int x = point.X * (gridCellCount + 1) / gridLayer.Width;
            int y = point.Y * (gridCellCount + 1) / gridLayer.Height;
            point = new Point(x * gridCellWidth, y * gridCellHeight);
            return point;
        }

        private void PaintLines()
        {
            CreateOrRecreateLayer(ref linesLayer);
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
            CreateOrRecreateLayer(ref currentLineLayer);
            using (Graphics graphics = Graphics.FromImage(currentLineLayer))
            {
                graphics.DrawLine(Pens.Orange, start, drawSurface.PointToClient(Cursor.Position));
            }
            drawSurface.Invalidate();
        }

        private void CreateOrRecreateLayer(ref Bitmap layer)
        {
            try
            {
                layer.Dispose();
            }
            catch (Exception)
            {
            }
            finally
            {
                layer = new Bitmap(drawSurface.Width, drawSurface.Height);
            }
        }
    }
}
