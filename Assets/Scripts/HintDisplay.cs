using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _hint;
    Color _textColor;

    // Start is called before the first frame update
    void Start()
    {
        _hint.gameObject.SetActive(false);
        _textColor = _hint.color;
    }

    public void DisplayHint()
    {
        _hint.color = Color.white;
        _hint.gameObject.SetActive(true);
    }

    IEnumerator DelayAndFade()
    {
        yield return new WaitForSeconds(3.0f);
        float textAlpha = _hint.color.a;

        while (textAlpha > 0.01)
        {
            textAlpha -= 0.01f;
            _textColor.a = textAlpha;
            _hint.color = _textColor;
            yield return null;
        }
    }

    public void StartFade()
    {
        StartCoroutine("DelayAndFade");
    }

    public void StopFade()
    {
        StopCoroutine("DelayAndFade");
    }
}
