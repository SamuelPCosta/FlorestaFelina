using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCam;
    [SerializeField] private InputsMovement inputsMovement;
    [SerializeField] private float dutchLimit = 20f;
    private bool reset = false;
    public float timeAboveThreshold = 0f;
    public float threshold = 0.1f;
    private float FOVDefault = 40f;
    private float distanceDefault = 5f;

    void Start()
    {
        virtualCam = gameObject.GetComponent<CinemachineVirtualCamera>();
    }

    void Update(){
        if (gameObject.activeSelf && inputsMovement != null){

            if ((inputsMovement.move != Vector2.zero) && inputsMovement.acceleration > threshold){
                timeAboveThreshold += Time.deltaTime;
                timeAboveThreshold = Mathf.Clamp(timeAboveThreshold, 0f, 1f);
            }
            else{
                timeAboveThreshold = 0f;
                if (reset) { 
                    reset = false;
                    StopAllCoroutines();

                    float currentCameraDistance = 0;
                    var componentBase = virtualCam.GetCinemachineComponent(CinemachineCore.Stage.Body);
                    if (componentBase is Cinemachine3rdPersonFollow)
                    {
                        currentCameraDistance = (componentBase as Cinemachine3rdPersonFollow).CameraDistance;
                    }
                    StartCoroutine(ResetInclination(virtualCam.m_Lens.Dutch, virtualCam.m_Lens.FieldOfView, currentCameraDistance));
                }
            }

            if(timeAboveThreshold != 0) {
                StopAllCoroutines();
                virtualCam.m_Lens.Dutch = (dutchLimit * inputsMovement.move.x * timeAboveThreshold);

                float lensDistortion = 1+(inputsMovement.acceleration * timeAboveThreshold);
                lensDistortion = lensDistortion / 1.2f;

                virtualCam.m_Lens.FieldOfView = Mathf.Clamp(FOVDefault * lensDistortion, FOVDefault, 90f);

                var componentBase = virtualCam.GetCinemachineComponent(CinemachineCore.Stage.Body);
                if (componentBase is Cinemachine3rdPersonFollow)
                {
                    (componentBase as Cinemachine3rdPersonFollow).CameraDistance = distanceDefault / (lensDistortion * 1.15f);
                }

                reset = true;
            }
        }
    }

    private IEnumerator ResetInclination(float start, float fovStart, float distanceStart){
        float time = Mathf.Clamp(Mathf.Ceil(start/15), 0.25f, 1f);
        float elapsed = 0f;
        var componentBase = virtualCam.GetCinemachineComponent(CinemachineCore.Stage.Body);
        while (elapsed < time)
        {
            virtualCam.m_Lens.Dutch = Mathf.Lerp(start, 0f, elapsed / time);

            virtualCam.m_Lens.FieldOfView = Mathf.Lerp(fovStart, FOVDefault, elapsed / time);

            float distance = Mathf.Lerp(distanceStart, distanceDefault, elapsed / time);
            if (componentBase is Cinemachine3rdPersonFollow)
                (componentBase as Cinemachine3rdPersonFollow).CameraDistance = distance;

            elapsed += Time.deltaTime;
            yield return null;
        }
        virtualCam.m_Lens.Dutch = 0f;
        virtualCam.m_Lens.FieldOfView = FOVDefault;
        if (componentBase is Cinemachine3rdPersonFollow)
            (componentBase as Cinemachine3rdPersonFollow).CameraDistance = distanceDefault;
        yield return null;
    }
}
