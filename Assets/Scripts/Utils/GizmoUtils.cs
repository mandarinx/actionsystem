using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AptGames {

    public class GizmoUtils : MonoBehaviour {

        private struct Sphere {
            public Vector3 position;
            public float   radius;
            public Color   color;
        }

        private struct Line {
            public Vector3 from;
            public Vector3 to;
            public Color   color;
        }

        private struct Cube {
            public Vector3 position;
            public Vector3 size;
            public Color   color;
        }

        private struct Ring {
            public Matrix4x4 matrix;
            public float     radius;
            public float     lineThickness;
            public Color     color;
            public int       segments;
        }

        private readonly List<Sphere> spheres     = new List<Sphere>();
        private readonly List<Sphere> spheresWire = new List<Sphere>();
        private readonly List<Line>   lines       = new List<Line>();
        private readonly List<Cube>   cubesWire   = new List<Cube>();
        private readonly List<Ring>   rings       = new List<Ring>();
        
        public static GizmoUtils Create(string name) {
            return new GameObject(name, typeof(GizmoUtils)).GetComponent<GizmoUtils>();
        }

        public static void DrawSphere(GizmoUtils gu, Vector3 position, float radius, Color color) {
            gu.spheres.Add(new Sphere { position = position, radius = radius, color = color });
        }

        public static void DrawWireSphere(GizmoUtils gu, Vector3 position, float radius, Color color) {
            gu.spheresWire.Add(new Sphere { position = position, radius = radius, color = color });
        }

        public static void DrawSpheres(GizmoUtils gu, Vector3[] positions, float radius, Color color) {
            for (int i = 0; i < positions.Length; ++i) {
                gu.spheres.Add(new Sphere { position = positions[i], radius = radius, color = color });
            }
        }

        public static void DrawLine(GizmoUtils gu, Vector3 from, Vector3 to, Color color) {
            gu.lines.Add(new Line { from = from, to = to, color = color });
        }

        public static void DrawWireCube(GizmoUtils gu, Vector3 position, Vector3 size, Color color) {
            gu.cubesWire.Add(new Cube { position = position, size = size, color = color });
        }

        public static void DrawRing(GizmoUtils gu, Matrix4x4 matrix, float radius, Color color, float lineThickness = 0.01f, int segments = 16) {
            gu.rings.Add(new Ring { matrix = matrix, radius = radius, color = color, lineThickness = lineThickness, segments = segments });
        }
        
        public static void DrawRing(Matrix4x4 matrix, float radius, float lineThickness, Color color, int segments = 16) {
            GL.PushMatrix();
            GL.MultMatrix(matrix);
            new Material(Shader.Find("Sprites/Default")).SetPass(0);
            GL.Begin(GL.QUADS);
            {
                GL.Color(color);

                //warm up:
                {
                    Vector3 A0 = CalcPoint(0, segments, radius);
                    Vector3 B0 = A0 + A0.normalized * lineThickness;
                    GL.Vertex(A0);
                    GL.Vertex(B0);
                }

                //segments:
                for (int i = 1; i <= segments; i++) {
                    Vector3 A = CalcPoint(i, segments, radius);
                    Vector3 B = A + A.normalized * lineThickness;

                    //even
                    if (i % 2 == 0) {
                        //end started quad:
                        GL.Vertex(A);
                        GL.Vertex(B);

                        //start new quad
                        GL.Vertex(A);
                        GL.Vertex(B);
                    }
                    //odd
                    else {
                        //end started quad:
                        GL.Vertex(B);
                        GL.Vertex(A);

                        //start new quad
                        GL.Vertex(B);
                        GL.Vertex(A);
                    }
                }
            }
            GL.End();
            GL.PopMatrix();
        }

        private static Vector3 CalcPoint(int i, int max, float radius) {
            float f     = i / (float) max;
            float angle = f * Mathf.PI * 2;
            return new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);
        }

        // Calculate a constant gizmo size, independent on distance from camera
        private static float GetGizmoSize(Vector3 position) {
            var cam      = Camera.current;
            var camtr    = cam.transform;
            var distance = Vector3.Dot(position - camtr.position, camtr.forward);
            var width    = cam.pixelWidth;
            var height   = cam.pixelHeight;
            var denom = Mathf.Sqrt(width * width + height * height) *
                        Mathf.Tan(cam.fieldOfView * Mathf.Deg2Rad);
            return Mathf.Max(0.0001f, distance / denom * 100.0f);
        }
        
        private void OnDrawGizmos() {
            for (int i = 0; i < spheres.Count; ++i) {
                Gizmos.color = spheres[i].color;
                Gizmos.DrawSphere(spheres[i].position, spheres[i].radius);
            }

            for (int i = 0; i < spheresWire.Count; ++i) {
                Gizmos.color = spheresWire[i].color;
                Gizmos.DrawWireSphere(spheresWire[i].position, spheresWire[i].radius);
            }

            for (int i = 0; i < lines.Count; ++i) {
                Gizmos.color = lines[i].color;
                Gizmos.DrawLine(lines[i].from, lines[i].to);
            }

            for (int i = 0; i < cubesWire.Count; ++i) {
                Gizmos.color = cubesWire[i].color;
                Gizmos.DrawWireCube(cubesWire[i].position, cubesWire[i].size);
            }

            for (int i = 0; i < rings.Count; ++i) {
                Ring r = rings[i];
                DrawRing(r.matrix, r.radius, r.lineThickness, r.color, r.segments);
            }
            
            #if UNITY_EDITOR
            // Keep drawing gizmos when pausing the editor
            if (EditorApplication.isPaused) {
                return;
            }
            #endif
            
            spheres.Clear();
            lines.Clear();
            cubesWire.Clear();
            rings.Clear();
        }
    }
}