using Cinemachine;
using UnityEngine;
using RL.Core;

namespace RL.Systems.Game {

    public class CameraSystem : IGameSystem {

        private Camera mainCamera;
        private CinemachineVirtualCamera virtualCamera;
        
        public void Init(IGameSystems gameSystems, IConfig config, IAssets assets) {
            mainCamera = Object
                        .Instantiate(((Assets) assets).GetPrefab("Main Camera"))
                        .GetComponent<Camera>();
            virtualCamera = Object
                           .Instantiate(((Assets) assets).GetPrefab("CM vcam"))
                           .GetComponent<CinemachineVirtualCamera>();
        }

        public void SetTarget(Transform cameraTarget) {
            virtualCamera.Follow = cameraTarget;
        }
    }
}