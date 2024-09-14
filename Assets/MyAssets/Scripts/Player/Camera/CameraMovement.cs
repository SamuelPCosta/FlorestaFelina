using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCam;
    [SerializeField] private InputsMovement inputsMovement;
    [SerializeField] private float dutchLimit = 20f;
    private bool defaultInclination = true;
    private bool oldInclination = true;
    private bool reset = false;
    public float timeAboveThreshold = 0f;
    public float threshold = 0.3f;

    void Start()
    {
        virtualCam = gameObject.GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (gameObject.activeSelf && inputsMovement != null){
    //        if(inputsMovement.acceleration < 0.5 || Mathf.Abs(inputsMovement.move.x) < 0.3){
    //            StopAllCoroutines();
    //            virtualCam.m_Lens.Dutch = 0f;
    //            StartCoroutine(ResetInclination(virtualCam.m_Lens.Dutch, .8f));
    //        }
    //        else {
    //            StopAllCoroutines();
    //            defaultInclination = false;
    //            float limit = dutchLimit * inputsMovement.move.x;
    //            StartCoroutine(StartInclination(virtualCam.m_Lens.Dutch, limit, .5f));
    //        }
    //    }
    //}

    void Update(){
        if (gameObject.activeSelf && inputsMovement != null){

            if (Mathf.Abs(inputsMovement.move.x) > threshold && inputsMovement.acceleration > threshold){
                timeAboveThreshold += Time.deltaTime;
                timeAboveThreshold = Mathf.Clamp(timeAboveThreshold, 0f, 1f);
            }
            else{
                timeAboveThreshold = 0f;
                if (reset) { 
                    reset = false;
                    StopAllCoroutines();
                    StartCoroutine(ResetInclination(virtualCam.m_Lens.Dutch));
                }
            }

            if(timeAboveThreshold != 0) {
                StopAllCoroutines();
                virtualCam.m_Lens.Dutch = (dutchLimit * inputsMovement.move.x * timeAboveThreshold);
                reset = true;
            }
        }
    }

    private IEnumerator ResetInclination(float start){
        float time = Mathf.Ceil(start / 10);
        float elapsed = 0f;
        while (elapsed < time)
        {
            virtualCam.m_Lens.Dutch = Mathf.Lerp(start, 0f, elapsed / time);
            elapsed += Time.deltaTime;
            yield return null;
        }
        virtualCam.m_Lens.Dutch = 0f;
        yield return null;
    }

    private IEnumerator StartInclination(float time)
    {
        float elapsed = 0f;
        while (elapsed < time)
        {
            //percent = Mathf.Lerp(0, 1f, elapsed / time);
            elapsed += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
}
