using UnityEngine;

[CreateAssetMenu(fileName = "Info", menuName = "Infos/EnemyInfo")]
public class EnemyInfo : ScriptableObject
{
    public float StartSpeed;
    public Sprite[] Sprites;
    public float IncreaseSpeedRandomValue;
}
