using UnityEngine;
using UnityEngine.InputSystem;

public enum Power { Min, Mid, Max}
public enum Duration { Min, Mid, Max }

public class FeedbackController : MonoBehaviour{
    private Gamepad gamepad;
    private InputsMovement move;
    private GameController controller;
    private bool vibration = true;
    private bool guidedCam = false;

    void Start(){
        move = FindObjectOfType<InputsMovement>();
        controller = FindObjectOfType<GameController>();
    }

    void Update()
    {
        guidedCam = controller.isGuidedCamera;
        gamepad = Gamepad.current;
        if (Keyboard.current != null && Keyboard.current.anyKey.isPressed && gamepad != null)
            vibration = false;
        else
            vibration = true;
    }

    public void Vibrate(Power power, Duration duration){
        if (!vibration || guidedCam)
            return;
        gamepad = Gamepad.current;
        if (gamepad != null){
            float value = getPower(power);
            gamepad.SetMotorSpeeds(value, value);
            CancelInvoke("StopVibration");
            Invoke("StopVibration", getTime(duration));
        }
    }

    public void VibrateRoomba(){
        if (!vibration || guidedCam)
            return;
        gamepad = Gamepad.current;
        if (gamepad != null){
            float value = move.acceleration * move.acceleration;
            gamepad.SetMotorSpeeds(Mathf.Clamp(value, 0f, .5f), value);
            //CancelInvoke("StopVibration");
        }
    }

    private float getPower(Power power){
        switch (power){
            case Power.Min: return 0.2f;
            case Power.Mid: return 0.7f;
            case Power.Max: return   1f;
                default: return 0f;
        }
    }

    private float getTime(Duration time){
        switch (time){
            case Duration.Min: return 0.2f;
            case Duration.Mid: return 0.5f;
            case Duration.Max: return   1f;
                default: return 0f;
        }
    }

    public void StopVibration(){
        gamepad = Gamepad.current;
        if (gamepad != null)
            gamepad.SetMotorSpeeds(0, 0);
    }
}
