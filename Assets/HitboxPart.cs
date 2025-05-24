using UnityEngine;

public class HitboxPart : MonoBehaviour
{
    [Tooltip("��� ���� ����� ����. ������: Head, Leg, Body")]
    public string bodyPartType = "Body";

    [Tooltip("������ �� �����, �������� ����������� ��� �����.")]
    public Enemy healthManager;

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
            default:
                damage = 10;
                break;
        }

        healthManager.TakeDamage(damage, bodyPartType);
    }
}
