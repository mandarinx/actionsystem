using UnityEngine;

namespace RL {

    public class DebugMapGrid : MonoBehaviour {

        public Material matGrid;

        private bool show;

        private void Start() {
            show = PlayerPrefs.GetInt(CFG.PPKEY_DRAW_GRID, 0) == 1;
        }
        
        private void Update() {
            if (UnityEngine.Input.GetKeyUp(KeyCode.G)) {
                show = !show;
                PlayerPrefs.SetInt(CFG.PPKEY_DRAW_GRID, show ? 1 : 0);
            }
        }

        private void OnPostRender() {
            if (!matGrid) {
                Debug.LogError("Please Assign a material on the inspector");
                return;
            }

            if (!show) {
                return;
            }
            
            GL.PushMatrix();
            matGrid.SetPass(0);
            GL.LoadOrtho();

            GL.Begin(GL.LINES);
            const float c = 1f;
            GL.Color(new Color(c,c,c));
            for (int i = 0; i < CFG.MAP_WIDTH; ++i) {
                float x = i / (float) CFG.MAP_WIDTH;
                GL.Vertex(new Vector3(x, 0, 0));
                GL.Vertex(new Vector3(x, 1, 0));
            }
            for (int i = 0; i < CFG.MAP_HEIGHT; ++i) {
                float y = i / (float) CFG.MAP_HEIGHT;
                GL.Vertex(new Vector3(0, y, 0));
                GL.Vertex(new Vector3(1, y, 0));
            }
            GL.End();

            GL.PopMatrix();
        }
        
    }
}