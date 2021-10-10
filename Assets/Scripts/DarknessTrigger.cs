using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessTrigger : MonoBehaviour
{
    public AudioSource backgroundSource;
    public Animator playerAnim;
    public bool darknessRight;

    private static float originalVolume;
    private bool fadeOut;
    private static Coroutine routine;

    private void Start()
    {
        originalVolume = backgroundSource.volume;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (playerAnim.GetBool("halo"))
            return;

        if (darknessRight)
            fadeOut = collision.transform.position.x > transform.position.x;
        else
            fadeOut = collision.transform.position.x < transform.position.x;

        if (routine != null)
            StopCoroutine(routine);
        routine = StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        int fadeInt = 1;

        if (fadeOut)
            fadeInt = -1;

        do
        {
            backgroundSource.volume += fadeInt * 0.0015f;
            yield return new WaitForSecondsRealtime(0.06f);
        } while (backgroundSource.volume > 0 & backgroundSource.volume < originalVolume);

        if (backgroundSource.volume != 0)
            backgroundSource.volume = originalVolume;
    }
}
