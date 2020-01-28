using System.Text;
using TMPro;
using UnityEngine;

namespace RL {

    public class DebugMapInfo : MonoBehaviour {

        public GameManager gameManager;
        public TextMeshProUGUI txtMapInfo;

        private Camera        cam;
        private StringBuilder log;

        private void Awake() {
            cam = Camera.main;
            log = new StringBuilder();
        }

        private void Update() {
            if (gameManager.Game == null) {
                return;
            }
            ClearLog();
            Vector3 mouse = cam.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            Vector2Int coord = Map.GetCoord(mouse);
            int index = Map.GetIndex(coord);
            int tile = Map.GetTile(gameManager.Game.map, index);
            
            Log($"Mouse [x: {mouse.x:00.000}, y: {mouse.y:00.000}]");
            Log($"Map coord [x: {coord.x}, y: {coord.y}]");
            Log($"Map index: {index}");
            Log($"Tile: {tile}");

            PrintLog();
        }

        private void ClearLog() {
            log.Clear();
        }

        private void Log(string msg) {
            log.AppendLine(msg);
        }

        private void PrintLog() {
            txtMapInfo.text = log.ToString();
        }
    }
}