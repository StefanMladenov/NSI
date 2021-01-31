using UnityEngine;
using UnityEngine.UI;
using System;


public class BalloonScript : MonoBehaviour
{
    public Text result;
    public event Action<BalloonScript> onClick;

    public void OnButtonClick()
    {
        LeanTween.cancel(gameObject);
        LTSeq sequence = LeanTween.sequence();
        sequence.append(LeanTween.scale(gameObject, Vector3.one * 0.8f, .2f).setEaseInBounce());
        sequence.append(LeanTween.scale(gameObject, Vector3.one, .2f).setEaseOutBounce());
        onClick?.Invoke(this);
        Debug.Log("Baloon clicked");
    }
}
