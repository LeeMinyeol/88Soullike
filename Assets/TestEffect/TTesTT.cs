using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTesTT : MonoBehaviour
{
    public float shakeDuration = 0.5f;      // ����ũ ���� �ð�
    public float shakeMagnitude = 0.1f;     // ����ũ ����
    public float shakeFadeOutTime = 0.5f;   // ����ũ ���̵�ƿ� �ð�

    private Vector3 originalPosition;       // ���� ī�޶� ��ġ
    public float knockbackForce = 5f;         // �˹� ��
    public float knockbackDuration = 0.5f;    // �˹� ���� �ð�

    public Rigidbody2D playerRigidbody;
    public ParticleSystem particleSystem;

    public float flashDuration = 0.1f;       // ��½�� ���� �ð�
    public Color flashColor = Color.white;   // ��½�� ����

    public SpriteRenderer spriteRenderer;

    public float zoomDuration = 0.3f;   // ���� ���� �ð�
    public float targetZoom = 2f;       // ��ǥ �� ����
    public AnimationCurve zoomCurve;    // ���� �

    private Camera mainCamera;

    public float stopDuration = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #region CameraShake
    public void StartShake()
    {
        originalPosition = Camera.main.transform.position;
        StartCoroutine(ShakeCoroutine());
    }
    // ����ũ �ڷ�ƾ
    private IEnumerator ShakeCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            // ������ ����ũ ��ġ ���
            Vector3 shakePosition = originalPosition + Random.insideUnitSphere * shakeMagnitude;

            // ī�޶� ��ġ ����
            Camera.main.transform.position = shakePosition;

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        // ����ũ ���̵�ƿ�
        float fadeElapsedTime = 0f;
        while (fadeElapsedTime < shakeFadeOutTime)
        {
            // ���� ���� ��ġ�� ����
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, originalPosition, fadeElapsedTime / shakeFadeOutTime);

            fadeElapsedTime += Time.deltaTime;

            yield return null;
        }
        // ���� ��ġ ����
        Camera.main.transform.position = originalPosition;
    }
    #endregion
    #region Knockback
    public void Knockback()
    {
        StartCoroutine(KnockbackCoroutine());
    }

    // �˹� �ڷ�ƾ
    private IEnumerator KnockbackCoroutine()
    {
        // �˹� ����� �� ���
        Vector2 knockbackDirection = Vector2.left;
        Vector2 knockbackForceVector = knockbackDirection * knockbackForce;

        // �˹� �� ����
        playerRigidbody.velocity = knockbackForceVector;

        // �˹� ���� �ð���ŭ ���
        yield return new WaitForSeconds(knockbackDuration);

        // �˹� �� �ʱ�ȭ
        playerRigidbody.velocity = Vector2.zero;
    }
    #endregion
    #region Effect
    public void StartEffect()
    {
        StartCoroutine(PlayParticleEffectCoroutine());
    }
    private IEnumerator PlayParticleEffectCoroutine()
    {
        // ��ƼŬ �ý��� ���
        particleSystem.Play();
        // ��ƼŬ ��� �ð���ŭ ���
        yield return new WaitForSeconds(particleSystem.main.duration);

        // ��ƼŬ �ý��� ����
        particleSystem.Stop();
    }
    #endregion
    #region ScreenFlash
    // ȭ�� ��½��
    public void FlashScreen()
    {
        StartCoroutine(FlashCoroutine());
    }

    // ��½�� �ڷ�ƾ
    private IEnumerator FlashCoroutine()
    {
        // ���� ���� ����
        Color originalColor = spriteRenderer.color;

        // ��½�� ���� ����
        spriteRenderer.color = flashColor;

        // ��½�� ���� �ð���ŭ ���
        yield return new WaitForSeconds(flashDuration);

        // ���� ���� ����
        spriteRenderer.color = originalColor;
    }
    #endregion
    #region ZoomIn
    public void ZoomIn()
    {
        StartCoroutine(ZoomInCoroutine());
    }
    private IEnumerator ZoomInCoroutine()
    {
        float startTime = Time.time;
        float initialZoom = mainCamera.orthographicSize;

        while (Time.time - startTime < zoomDuration)
        {
            float t = (Time.time - startTime) / zoomDuration;
            float newZoom = Mathf.Lerp(initialZoom, targetZoom, zoomCurve.Evaluate(t));

            mainCamera.orthographicSize = newZoom;

            yield return null;
        }
        mainCamera.orthographicSize = targetZoom;
    }
    #endregion
    #region TimeStop
    public void StopPlayerTime()
    {
        StartCoroutine(StopPlayerTimeCoroutine());
    }
    private IEnumerator StopPlayerTimeCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        float originalTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(stopDuration);
        Time.timeScale = originalTimeScale;
    }
    #endregion

}
