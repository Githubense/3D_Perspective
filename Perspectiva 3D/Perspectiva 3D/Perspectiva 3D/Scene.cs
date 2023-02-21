using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Perspectiva_3D
{
    public class Scene
    {
        private Figure figure;
        public Pen pen = new Pen(Color.Green, 5);
        public int angle;

        public Scene(Figure figures)
        {
            figure = figures;
        }

        public void Draw(Graphics graphics, int viewWidth, int viewHeight)
        {
            graphics.Clear(Color.Black);

            // Draw x-axis
            graphics.DrawLine(new Pen(Color.Gray), 0, viewHeight / 2, viewWidth, viewHeight / 2);

            // Draw y-axis
            graphics.DrawLine(new Pen(Color.Gray), viewWidth / 2, 0, viewWidth / 2, viewHeight);

            var projected = new Vertex[figure.Vertices.Length];
            for (var i = 0; i < figure.Vertices.Length; i++)
            {
                var vertex = figure.Vertices[i];

                var transformed = vertex.RotateX(0);
                
                
                if (angle > 0 && angle < 90)
                {
                    transformed = vertex.RotateX(angle);


                }
                else if (angle > 90 && angle < 180)
                {
                    transformed = vertex.RotateY(angle);
                }
                else if (angle > 180 && angle < 270)
                {
                    transformed = vertex.RotateZ(angle);
                }
                else
                {
                    transformed = vertex.RotateX(angle).RotateY(angle).RotateZ(angle);
                }
                
                
                projected[i] = transformed.Project(viewWidth, viewHeight, 709, 6);




            }

            for (var j = 0; j < 6; j++)
            {
                graphics.DrawLine(pen,
                    (int)projected[figure.Faces[j, 0]].X,
                    (int)projected[figure.Faces[j, 0]].Y,
                    (int)projected[figure.Faces[j, 1]].X,
                    (int)projected[figure.Faces[j, 1]].Y);

                graphics.DrawLine(pen,
                    (int)projected[figure.Faces[j, 1]].X,
                    (int)projected[figure.Faces[j, 1]].Y,
                    (int)projected[figure.Faces[j, 2]].X,
                    (int)projected[figure.Faces[j, 2]].Y);

                graphics.DrawLine(pen,
                    (int)projected[figure.Faces[j, 2]].X,
                    (int)projected[figure.Faces[j, 2]].Y, 
                    (int)projected[figure.Faces[j, 3]].X, 
                    (int)projected[figure.Faces[j, 3]].Y);

                graphics.DrawLine(pen,
                    (int)projected[figure.Faces[j, 3]].X, 
                    (int)projected[figure.Faces[j, 3]].Y,
                    (int)projected[figure.Faces[j, 0]].X, 
                    (int)projected[figure.Faces[j, 0]].Y);
            }
            angle++;
        }
    }
}
