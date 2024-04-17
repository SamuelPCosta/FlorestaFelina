using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("Colors")]
    public Color color1;
    public Color color2;

    protected IEnumerator animateOpacity(GameObject text)
    {
        float duration = 2.5f;
        Animator textAnimator = text.GetComponent<Animator>();

        textAnimator.Play("FadeInText", 0, 0f);
        yield return new WaitForSeconds(duration);

        textAnimator.Play("FadeOutText");

        yield return new WaitForSeconds(.5f);
        text.gameObject.SetActive(false);
    }
}
