using Cinemachine;
using UnityEngine;
using RL.Core;

namespace RL.Systems.Game {

    public class CameraSystem : IGameSystem {

        private Camera mainCamera;
        private CinemachineVirtualCamera virtualCamera;
        
        public void Init(IGameSystems gameSystems, Context ctx) {
            mainCamera = Object
                        .Instantiate(ctx.assets.Get<GameObject>("prefabs", "system/main camera"))
                        .GetComponent<Camera>();
            virtualCamera = Object
                           .Instantiate(ctx.assets.Get<GameObject>("prefabs", "system/cm vcam"))
                           .GetComponent<CinemachineVirtualCamera>();
        }

        public void SetTarget(Transform cameraTarget) {
            virtualCamera.Follow = cameraTarget;
        }
    }
}