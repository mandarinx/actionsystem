using System.Text;
using RL.Systems.Map;
using TMPro;
using UnityEngine;

namespace RL {

    public class DebugMapInfo : MonoBehaviour {

        public Bootstrapper bootstrapper;
        public TextMeshProUGUI txtMapInfo;

        private StringBuilder log;

        private void Awake() {
            log = new StringBuilder();
        }

        private void Update() {
            if (bootstrapper.Game == null) {
                return;
            }
            ClearLog();
            // Map map = bootstrapper.Game.systems.Get<MapSystem>().Map;
            // Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Vector2Int coord = map.WorldPosToCoord(mouse);
            // int index = map.WorldPosToIndex(mouse);
            // bool spawnpoint = map.HasSpawnpoint(index);
            //
            // Log($"Mouse [x: {mouse.x:00.000}, y: {mouse.y:00.000}]");
            // Log($"Map coord [x: {coord.x}, y: {coord.y}]");
            // Log($"Map index: {index}");
            // if (spawnpoint) {
            //     Log("Spawnpoint");
            // }

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