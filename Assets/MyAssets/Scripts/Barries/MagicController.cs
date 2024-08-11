using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicController : MonoBehaviour
{
    public TrailRenderer trail;

    private Vector3 localPosition;
    void Start()
    {
        localPosition = transform.localPosition;
        gameObject.SetActive(false);
    }

    public void castMagic(GameObject barrier)
    {
        transform.localPosition = localPosition;
        gameObject.SetActive(true);
        StartCoroutine(MoveToObject(barrier, 3f));
    }

    IEnumerator MoveToObject(GameObject targetObject, float time)
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = targetObject.transform.position;
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //transform.position = targetPosition;

        disable();
    }

    private void disable()
    {
        trail.GetComponent<TrailRenderer>().Clear();
        gameObject.SetActive(false);
    }
}
