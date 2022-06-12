using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinController : MonoBehaviour
{
    [SerializeField]
    private float scaleDuration;
    [SerializeField]
    private float scaleMultipier;

    private Animator animator;
    private Vector3 selectedScale;
    private Vector3 normalSize;

    private void Start()
    {
        //if (TryGetComponent<Animator>(out animator)) animator.SetBool("All_b", true);
    }

    public void Scale(bool increase)
    {
        if(normalSize == Vector3.zero) normalSize = transform.localScale; //not cleancode, it must be in start but it work incorrect
        selectedScale = normalSize * scaleMultipier;
        StopAllCoroutines(); //not cleancode
        if (increase) { StartCoroutine(Scale(selectedScale)); PlayAnimation(true); }
        else {StartCoroutine(Scale(normalSize)); PlayAnimation(false); }
    }

    IEnumerator Scale(Vector3 _targetScale)
    {
        float timeElapsed = 0;
        while (transform.localScale != _targetScale)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > scaleDuration) timeElapsed = scaleDuration;
            transform.localScale = Vector3.Lerp(transform.localScale, _targetScale, timeElapsed / scaleDuration);
            yield return null;
        }
    }

    void PlayAnimation(bool play)
    {
        if (TryGetComponent<Animator>(out animator))
        {
            if(play) animator.SetBool("All_b", true);
            else animator.SetBool("All_b", false);
        }
    }
}
