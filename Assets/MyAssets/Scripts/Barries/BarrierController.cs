using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    public GameObject orb;
    public Material defaultMaterial;
    public float fadeDuration = .8f;

    private void Start(){
        orb.SetActive(false);
    }

    public void makeWay()
    {
        //TODO pegar animator e ativar animacao de abrir caminho
        orb.SetActive(true);
        AudioController.playAction(INTERACTIONS.Spell);
        Material material = new Material(defaultMaterial);

        Renderer[] treesRenderes = transform.GetChild(0).GetComponentsInChildren<Renderer>();
        foreach (var tree in treesRenderes){
            tree.material = material;
        }
        StartCoroutine(destroyTress(treesRenderes, fadeDuration));
        Invoke("destroy", fadeDuration);
    }

    private IEnumerator destroyTress(Renderer[] treesRenderes, float fadeDuration)
    {
        float fade = 0f;
        float targetFade = 1.2f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            fade = Mathf.Lerp(0f, targetFade, elapsedTime / fadeDuration);
            foreach (var tree in treesRenderes)
                tree.material.SetFloat("_fade", fade);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void destroy()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
