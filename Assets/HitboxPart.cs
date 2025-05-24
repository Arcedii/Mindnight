using UnityEngine;

public class HitboxPart : MonoBehaviour
{
    [Tooltip("��� ���� ����� ����. ������: Head, Leg, Body")]
    public string bodyPartType = "Body";

    [Tooltip("������ �� �����, �������� ����������� ��� �����.")]
    public Enemy healthManager;

    public ParticleSystem bloodEffect;

    private AudioSource audioSource;

    public AudioClip hitSound; // �������� ����� ���������


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

        // ������ �����
        bloodEffect?.Play();

        // ������������� ������ �����
        if (bloodEffect != null)
            bloodEffect.Play();

        // ������������� ����
        if (audioSource != null && hitSound != null)
            audioSource.PlayOneShot(hitSound);
    }
}


