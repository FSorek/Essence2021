using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_Health : MonoBehaviour
{
    [SerializeField] private Image foregroundImg;
    [SerializeField] private float positionOffset;
    [SerializeField] private float updateSpeed;
    private IEntity currentTarget;
    private Health healthTarget;
    private Camera mainCamera;
    private Coroutine updateHealth;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void SetTarget(IEntity entity)
    {
        if(!(entity is ITakeDamage))
            return;
        currentTarget = entity;
        healthTarget = ((ITakeDamage) entity).Health;
        healthTarget.OnTakeDamage += HealthOnTakeDamage;
    }

    private void HealthOnTakeDamage()
    {
        float pct = healthTarget.CurrentHealth / healthTarget.MaxHealth;
        if(updateHealth != null)
            StopCoroutine(updateHealth);
        updateHealth = StartCoroutine(ResizeToPercentage(pct));
    }
    private IEnumerator ResizeToPercentage(float pct)
    {
        float preChangePercentage = foregroundImg.fillAmount;
        float elapsed = 0f;

        while (elapsed < updateSpeed)
        {
            elapsed += Time.deltaTime;
            foregroundImg.fillAmount = Mathf.Lerp(preChangePercentage, pct, elapsed / updateSpeed);
            yield return null;
        }

        foregroundImg.fillAmount = pct;
    }
    private void LateUpdate()
    {
        if (!healthTarget.IsAlive)
        {
            Invoke(nameof(Disable),updateSpeed);
            return;
        }
        transform.position = mainCamera.WorldToScreenPoint(currentTarget.Position.TruePosition - Vector3.forward * positionOffset);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}