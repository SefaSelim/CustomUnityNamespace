## 1. Using Custom.Timers
![image alt](https://github.com/SefaSelim/CustomUnityNamespace/blob/481d6cb84cb2b83ffcc7effaa3e64b88791755d7/CustomNamespaces/Timers/Pictures/TimerInterface.png)


![image alt](https://github.com/SefaSelim/CustomUnityNamespace/blob/4647818e778f8f994daf95a069aaece4cc908c31/CustomNamespaces/Timers/Pictures/SetT%C4%B0mer.png)

Recommended for single usage, in Start or single entries ect. if it is already in action, it will skip the overriding value, if you want to override use ForceSetTimer() instead.

![image alt](https://github.com/SefaSelim/CustomUnityNamespace/blob/4647818e778f8f994daf95a069aaece4cc908c31/CustomNamespaces/Timers/Pictures/SetTimer.png)

Use it in Update Method, returns a boolean in a single frame after the duration.

![image alt](https://github.com/SefaSelim/CustomUnityNamespace/blob/4647818e778f8f994daf95a069aaece4cc908c31/CustomNamespaces/Timers/Pictures/ForceSetTimer.png)

Whatever the current or before situation, overrides the new values and resets the timer with new values.

![image alt](https://github.com/SefaSelim/CustomUnityNamespace/blob/4647818e778f8f994daf95a069aaece4cc908c31/CustomNamespaces/Timers/Pictures/OverrideTimer.png)

Overrides the timer only at current case, if you want to override all the timers please use the Set functions instead.

![image alt](https://github.com/SefaSelim/CustomUnityNamespace/blob/4647818e778f8f994daf95a069aaece4cc908c31/CustomNamespaces/Timers/Pictures/AddAction.png)

Adds the specified function to the action list. Can be used to register multiple callbacks for when the timer completes.

![image alt](https://github.com/SefaSelim/CustomUnityNamespace/blob/4647818e778f8f994daf95a069aaece4cc908c31/CustomNamespaces/Timers/Pictures/ResetAction.png)

Resets all the functions that has been added before and resets the timer and count.

![image alt](https://github.com/SefaSelim/CustomUnityNamespace/blob/4647818e778f8f994daf95a069aaece4cc908c31/CustomNamespaces/Timers/Pictures/RemoveAction.png)

Removes a specified function into the action list. Inverse of the AddAction function.

![image alt](https://github.com/SefaSelim/CustomUnityNamespace/blob/4647818e778f8f994daf95a069aaece4cc908c31/CustomNamespaces/Timers/Pictures/Stop.png)

Stops the timer, does not reset count or any property.

![image alt](https://github.com/SefaSelim/CustomUnityNamespace/blob/4647818e778f8f994daf95a069aaece4cc908c31/CustomNamespaces/Timers/Pictures/Tick.png)

Most important function, Tick() function must have been in the Update() function at the top, it should tick every frame and you should tick the every timer that you created before.
Be careful about ticking only in one update function, otherwise it could cause some wrong values.


### External Accesses

---


### Usecases:

--
Using Custom.Timers;                       // USE NAMESPACE

Timer timer1 = new Timer();                // DEFINITIONS
Timer timer2 = new Timer();

Private Void Update(){

timer1.Tick();                             // IMPORTANT, TICK EVERY TIMER
timer2.Tick();
}
-- NECESSARY PART


### Usecase 1

Timer timer = new Timer();

Private Void Start(){
timer.SetTimer(() => print( timer1.PlayCount + " Times Triggered") , 2f, true);  // Will trigger the lambda expression every 2 second and it will print the count that player until lifetime.
} 


You can pass a full function too also, simple example down below:

Private Void OnEnterTrigger(){
timer.SetTimer(GetHit,1f,true);
}
Private Void OnExitTrigger(){
timer.ResetTimer();
}

Private Void GetHit(){ ... }

### Usecase 2

You can pass multi functions too, example down below:

Private Void OnEnterTrigger(collider other){
if (other.CompareTag("HealingArea")){

timer.SetTimer(Heal,1f,true);
timer.AddAction(HealParticule);

} }

Private Void Heal() { ... };
Private Void HealParticule() { ... };

### Usecase 3

This case will trigger the if statement every 5 second for only one frame.

Private Void Update(){
if(timer.SetTimer_ForUpdate(5f)){
print("Triggered");
} }

