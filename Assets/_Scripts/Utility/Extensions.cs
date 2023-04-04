using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class Extensions
{
    public static void AddExplosionForce(this Rigidbody2D rb, float explosionForce, Vector3 explosionPosition)
    {
        Vector3 direction = (rb.position - (Vector2)explosionPosition).normalized;

        rb.AddForce(direction * explosionForce, ForceMode2D.Impulse);
    }
    public static IEnumerator Shake(this Transform transform, float amplitude, float period)
    {
        Vector3 startPos = transform.position;
        while (period > 0)
        {
            transform.position = startPos + UnityEngine.Random.insideUnitSphere * amplitude;
            period -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        transform.position = startPos;
        yield return null;
    }
    public static IEnumerator FreeShake(this Transform transform, float amplitude, float period)
    {
        while (period > 0)
        {
            transform.position = transform.position + UnityEngine.Random.insideUnitSphere * amplitude;
            period -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }
    public static IEnumerator Blink(this SpriteRenderer spriteRenderer, float period, float delta)
    {
        while (period > 0)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            period -= delta;
            yield return new WaitForSeconds(delta);
        }
        spriteRenderer.enabled = true;
        yield return null;
    }
    public static IEnumerator TransitionToSceneAsync(this Scene scene, string sceneName, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (operation.isDone == false)
        {
            yield return null;
        }
    }
    public static IEnumerator TransitionToScene(this Scene scene, string sceneName, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene(sceneName);
    }
    public static IEnumerator TransitionToScene(this Scene scene, string sceneName, float delay, Action<AsyncOperation> action)
    {
        yield return new WaitForSecondsRealtime(delay);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (operation.isDone == false)
        {
            action.Invoke(operation);
            yield return null;
        }
    }
    public static IEnumerator Flash(this Graphic graphic, Color color, float amplitude, float riseTime, float fallTime, int repetition)
    {
        graphic.color = color;
        while (repetition > 0)
        {
            graphic.canvasRenderer.SetAlpha(0);
            graphic.CrossFadeAlpha(amplitude, riseTime, true);
            yield return new WaitForSecondsRealtime(riseTime);
            graphic.CrossFadeAlpha(0, fallTime, true);
            yield return new WaitForSecondsRealtime(fallTime);
            repetition--;
        }
    }
    public static IEnumerator FlashInverse(this Graphic graphic, Color color, float amplitude, float riseTime, float fallTime, int repetition)
    {
        graphic.color = color;
        while (repetition > 0)
        {
            graphic.CrossFadeAlpha(1, 0, true);
            graphic.CrossFadeAlpha(amplitude, riseTime, true);
            yield return new WaitForSecondsRealtime(riseTime);
            graphic.CrossFadeAlpha(1, fallTime, true);
            yield return new WaitForSecondsRealtime(fallTime);
            repetition--;
        }
    }
    public static IEnumerator SpriteFlash(this SpriteRenderer spriteRenderer, float value, float riseTime, float fallTime, int repetition)
    {
        Color tempColor = spriteRenderer.color;
        Color startColor = spriteRenderer.color;
        startColor.a = 1;

        float increment;
        float delta;
        while (repetition > 0)
        {
            delta = 0;
            increment = Time.fixedDeltaTime / riseTime;
            while (delta <= 1)
            {
                tempColor.a = Mathf.Lerp(startColor.a, value, delta);
                spriteRenderer.color = tempColor;

                delta += increment;
                yield return new WaitForFixedUpdate();
            }

            delta = 0;
            increment = Time.fixedDeltaTime / fallTime;
            while (delta <= 1)
            {
                tempColor.a = Mathf.Lerp(value, startColor.a, delta);
                spriteRenderer.color = tempColor;

                delta += increment;
                yield return new WaitForFixedUpdate();
            }
            tempColor.a = 1;
            spriteRenderer.color = tempColor;

            repetition--;
        }
    }
    public static IEnumerator FadeIn(this Graphic graphic, Color color, float time, Action action = null)
    {
        graphic.color = color;
        graphic.CrossFadeAlpha(0, 0, true);
        graphic.CrossFadeAlpha(1, time, true);
        yield return new WaitForSecondsRealtime(time);
        action?.Invoke();
    }
    public static IEnumerator FadeOut(this Graphic graphic, Color color, float time, Action action = null)
    {
        graphic.color = color;
        graphic.CrossFadeAlpha(1, 0, true);
        graphic.CrossFadeAlpha(0, time, true);
        yield return new WaitForSecondsRealtime(time);
        action?.Invoke();
    }
    public static IEnumerator FadeOut(this CanvasGroup cg, float time, Action action = null)
    {
        float elapsedTime = 0f;

        while(elapsedTime <= time)
        {
            cg.alpha = Mathf.Lerp(1, 0, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        cg.alpha = 0;
        action?.Invoke();
        yield return null;
    }
    public static IEnumerator FadeOIn(this CanvasGroup cg, float time, Action action = null)
    {
        float elapsedTime = 0f;

        while (elapsedTime <= time)
        {
            cg.alpha = Mathf.Lerp(0, 1, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        cg.alpha = 1;
        action?.Invoke();
        yield return null;
    }

}


