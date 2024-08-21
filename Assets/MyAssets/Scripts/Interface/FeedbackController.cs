using UnityEngine;
using UnityEngine.InputSystem;

public enum Power { Min, Mid, Max}
public enum Duration { Min, Mid, Max }

public class FeedbackController : MonoBehaviour{
    private Gamepad gamepad;
    private InputsMovement move;

    void Start(){
        move = FindObjectOfType<InputsMovement>();
    }

    public void Vibrate(Power power, Duration duration){
        gamepad = Gamepad.current;
        if (gamepad != null){
            float value = getPower(power);
            gamepad.SetMotorSpeeds(value, value);
            CancelInvoke("StopVibration");
            Invoke("StopVibration", getTime(duration));
        }
    }

    public void VibrateRoomba(){
        gamepad = Gamepad.current;
        if (gamepad != null){
            float value = Mathf.Lerp(0, 1, Mathf.InverseLerp(0, 255, move.acceleration));
            gamepad.SetMotorSpeeds(value, value);
            CancelInvoke("StopVibration");
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
