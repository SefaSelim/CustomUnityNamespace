using UnityEngine;
using System;

namespace Custom.Timers
{
    public interface ITimer
    {
        public bool SetTimer_ForUpdate(float timer);
        public void SetTimer(Action OnComplete, float timer, bool isLooping);
        public void ForceSetTimer(Action OnComplete, float timer, bool isLooping);
        public void OverrideTimer(float newTime);
        public void AddAction(Action actionToAdd);
        public void ResetActions();
        public void RemoveAction(Action actionToRemove);
        public void Stop();
        public void Tick();
    }
    
}