using UnityEngine;
using System;

namespace Custom.Timers
{
    

    public class Timer
    {

        #region Private Variables
        
        private int playCount = 0;
        private float totalTime;
        private float timer;
        private Action OnComplete;
        private bool isLooping;
        private bool onUpdateMethodDontAction = false;
        
        #endregion
        
        private bool completeFeedback = false;
        private bool onAction = false;
        
        #region Readonly Properties

        // You can access from outside the class
        public int PlayCount => playCount;
        public bool IsRunning => onAction;
        public float TimeRemaining => timer;
        public float TimePassed => totalTime - timer;
        public float TotalDuration => totalTime;

        #endregion

        
        /// <summary>
        /// Use it Only in Update Method
        /// </summary>
        /// <param name="timer">Duration Of The Timer</param>
        /// <returns>Return True When Executed End Of The Timer</returns>
        public bool SetTimer_ForUpdate(float timer)
        {
            if (!onAction)
            {
                onUpdateMethodDontAction = true;
                totalTime = timer;
                this.timer = timer;
                isLooping = true;

                onAction = true;
            }

            if (completeFeedback)
            {
                completeFeedback = false;
                return true;
            }

            return false;
        } // Use it in Only Update Method
        
        /// <summary>
        /// Use it for One Time Call is Preferred. If the timer currently working on,
        /// it won't override the values and won't break the current timer use ForceSetTimer() Insted.
        /// Set the values, and it will execute the function at the end of the timer.
        /// <param name="OnComplete">Function To Be Executed At The End Of The Timer</param>
        /// <param name="timer">Duration Of The Timer</param>
        /// <param name="isLooping">If It Is True, Loops Until You Reset Or Stop</param>
        /// </summary>

        public void SetTimer(Action OnComplete, float timer, bool isLooping)
        {
            if (!onAction)
            {
                onUpdateMethodDontAction = false;
                totalTime = timer;
                this.timer = timer;
                this.OnComplete = OnComplete;
                this.isLooping = isLooping;

                onAction = true;
                playCount = 0;
            }

        } // Preferred One Time Call Method
        
        /// <summary>
        /// Normally you cant set as an override when it is running, with this method whatever the current timer is, it will be overridden with new values.
        /// <param name="OnComplete">Function To Be Executed At The End Of The Timer</param>
        /// <param name="timer">Duration Of The Timer</param>
        /// <param name="isLooping">If It Is True, Loops Until You Reset Or Stop</param>
        /// </summary>
        public void ForceSetTimer(Action OnComplete, float timer, bool isLooping)
        {
            onUpdateMethodDontAction = false;
            totalTime = timer;
            this.timer = timer;
            this.OnComplete = OnComplete;
            this.isLooping = isLooping;

            onAction = true;
            playCount = 0;

            completeFeedback = false;
        } // Override The Current Timer
        
        #region BASIC OPERATIONS
        
        // OverrideTimer(float newTime) sets a new time and overrides it for current situation, not for future calls and loops.
        // AddAction(Action actionToAdd) adds an external function could execute in the same time with current function.
        // ResetActions() resets all functions and stops the timer.
        // RemoveAction(Action actionToRemove) if you want to remove a specific function from the timer. Not works with lambda expressions. Use ResetActions() instead.
        // Stop() stops the timer but does not reset the actions. If you want to reset the actions, use ResetActions() instead.

        public void OverrideTimer(float newTime)
        {
            timer = Mathf.Max(0f, newTime);
        }

        public void AddAction(Action actionToAdd)
        {
            OnComplete += actionToAdd;
        }

        public void ResetActions()
        {
            OnComplete = null;
            onUpdateMethodDontAction = false;
        }

        public void RemoveAction(Action actionToRemove)
        {
            OnComplete -= actionToRemove;
        }

        public void Stop()
        {
            onAction = false;
            timer = 0f;
            onUpdateMethodDontAction = false;
        }

        #endregion

        /// <summary>
        /// Use it in the Update Method for every timer you have.
        /// <para></para>
        /// <para>
        /// public void Update()
        /// { <para>
        /// timer1.Tick();<para>
        /// timer2.Tick();<para>
        /// timer...<para>
        /// 
        /// ... your code ...<para>
        /// }
        /// </para> </para></para></para></para></para>
        /// </summary>
        public void Tick()
        {
            if (timer > 0 && onAction)
            {
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    completeFeedback = true;
                    
                    if (!onUpdateMethodDontAction)
                    {
                        ExecuteTheFunctions();
                    }
                    playCount++;


                    if (isLooping)
                    {
                        timer = totalTime;
                    }
                    else
                    {
                        playCount = 0;
                        onAction = false;
                    }
                    
                }
            }

        }
        
        private void ExecuteTheFunctions() // ON COMPLETE THE FUNCTIONS
        {
            OnComplete?.Invoke();
        }

    }
}
