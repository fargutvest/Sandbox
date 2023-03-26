//https://smack0007.github.io/blog/2011/getting-started-with-slimdx.html

using System;
using System.Drawing;
using System.Windows.Forms;
using SlimDX;
using SlimDX.Direct3D9;
using SlimDX.Windows;

namespace SlimDXApp1
{
    public partial class SlimDXApp1Form : RenderForm
    {
        struct Vertex
        {
            public Vector4 Position;
            public int Color;
        }

        Device device;
        VertexDeclaration vertexDeclaration;

        public SlimDXApp1Form()
            : base("SlimDXApp1")
        {
            this.ClientSize = new Size(800, 600);

            this.device = new Device(new Direct3D(), 0, DeviceType.Hardware, this.Handle, CreateFlags.HardwareVertexProcessing, new PresentParameters()
            {
                BackBufferWidth = this.ClientSize.Width,
                BackBufferHeight = this.ClientSize.Height
            });

            this.vertexDeclaration = new VertexDeclaration(this.device, new[] {
                new VertexElement(0, 0, DeclarationType.Float4, DeclarationMethod.Default, DeclarationUsage.PositionTransformed, 0),
                new VertexElement(0, 16, DeclarationType.Color, DeclarationMethod.Default, DeclarationUsage.Color, 0),
                VertexElement.VertexDeclarationEnd
            });
        }

        public void Run()
        {
            MessagePump.Run(this, () =>
            {
                this.device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Black, 1.0f, 0);
                this.device.BeginScene();

                this.device.VertexDeclaration = this.vertexDeclaration;

                this.device.DrawUserPrimitives<Vertex>(PrimitiveType.TriangleList, 1, new[] {
                    new Vertex() { Color = Color.Red.ToArgb(), Position = new Vector4(400.0f, 100.0f, 0.5f, 1.0f) },
                    new Vertex() { Color = Color.Blue.ToArgb(), Position = new Vector4(650.0f, 500.0f, 0.5f, 1.0f) },
                    new Vertex() { Color = Color.Green.ToArgb(), Position = new Vector4(150.0f, 500.0f, 0.5f, 1.0f) }
                });

                this.device.EndScene();
                this.device.Present();
            });
        }

        [STAThread]
        static void Main()
        {
            SlimDXApp1Form form = new SlimDXApp1Form();
            form.Run();

            // Cleans up COM handles
            foreach (var item in ObjectTable.Objects)
                item.Dispose();
        }
    }
}