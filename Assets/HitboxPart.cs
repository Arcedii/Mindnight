using UnityEngine;

public class HitboxPart : MonoBehaviour
{
    [Tooltip("Тип этой части тела. Пример: Head, Leg, Body")]
    public string bodyPartType = "Body";

    [Tooltip("Ссылка на врага, которому принадлежит эта часть.")]
    public Enemy healthManager;

    public ParticleSystem bloodEffect;

    private AudioSource audioSource;

    public AudioClip hitSound; // Присвоим через инспектор


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnHit()
    {
        int damage = 0;

        switch (bodyPartType)
        {
            case "Head":
                damage = 100;
                break;
            case "Leg":
                damage = 25;
                break;
            case "Arm":
                damage = 20;
                break;
            case "Body":
                damage = 10;
                break;

        }

        healthManager.TakeDamage(damage, bodyPartType);

        // Эффект крови
        bloodEffect?.Play();

        // Воспроизвести эффект крови
        if (bloodEffect != null)
            bloodEffect.Play();

        // Воспроизвести звук
        if (audioSource != null && hitSound != null)
            audioSource.PlayOneShot(hitSound);
    }
}


