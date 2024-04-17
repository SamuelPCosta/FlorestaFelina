using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPalette : MonoBehaviour
{
    [Header("Colors")]
    public static Color enableText;
    public static Color disableText;

    public Color _enableText;
    public Color _disableText;

    private void Awake()
    {
        enableText = _enableText;
        disableText = _disableText;
    }
}