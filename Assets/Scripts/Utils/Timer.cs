using System;
using AptGames;

namespace Altruist {

    public class Timer : IUpdate {

        private          float  time;
        private readonly float  maxTime;
        private readonly Action onTimeout = () => {};

        public Timer(float startTime, Action timeoutCallback) {
            time = startTime;
            maxTime = startTime;
            onTimeout += timeoutCallback;
            UnityUpdate.Add(this);
        }

        public void Reset() {
            time = maxTime;
        }

        public void Stop() {
            UnityUpdate.Remove(this);
        }

        public void OnUpdate(float dt) {
            time -= dt;
            if (time <= 0f) {
                onTimeout();
                Stop();
            }
        }
    }
}