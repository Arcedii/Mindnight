// Copyright (c) 2025 Craciun Dan. All rights reserved.
// Unauthorized use or distribution is prohibited.


using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerDamageHandler : MonoBehaviour
{
    public Image damageOverlay; // UI элемент (например, красный Image)
    public float fadeSpeed = 1f;
    public float damageCooldown = 4f; // время до восстановления

    private int damageLevel = 0;
    private float lastHitTime = -10f;
    private bool isDead = false;

    public float hitCooldown = 1.0f;
    private float lastDamageTakenTime = -10f;

    public MonoBehaviour movementScript; // Сюда перетаскиваем PlayerController

    public GameObject PlayerCamera;
    public GameObject DeathCamera1;
    


    private void Update()
    {
        if (isDead)
            return;

        // Постепенное восстановление
        if (Time.time - lastHitTime >= damageCooldown && damageLevel > 0)
        {
            damageLevel--;
            lastHitTime = Time.time;
        }

        UpdateScreenEffect();
    }

    private void UpdateScreenEffect()
    {
        if (damageOverlay == null) return;

        // Прозрачность в зависимости от урона
        float alpha = damageLevel / 3f;
        Color color = damageOverlay.color;
        color.a = Mathf.Lerp(color.a, alpha, Time.deltaTime * fadeSpeed);
        damageOverlay.color = color;
    }

    public void TakeHit()
    {
        if (isDead)
            return;

        // Не принимать урон, если не прошло достаточно времени
        if (Time.time - lastDamageTakenTime < hitCooldown)
            return;

        lastDamageTakenTime = Time.time;
        damageLevel++;
        lastHitTime = Time.time;

        if (damageLevel >= 3)
        {
            Die();
        }
    }


    private void Die()
    {
        isDead = true;
        Debug.Log("Игрок умер");
        CanvasController.playerIsDead = true;
        movementScript.enabled = false;
        PlayerCamera.gameObject.SetActive(false);
        DeathCamera1.gameObject.SetActive(true);

        InfimaGames.LowPolyShooterPack.Character.playerAlive = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        int losses = PlayerPrefs.GetInt("LossCount", 0);
        PlayerPrefs.SetInt("LossCount", losses + 1);

    }
}
