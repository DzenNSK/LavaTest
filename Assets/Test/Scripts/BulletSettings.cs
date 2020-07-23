using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Bullet settings asset
[CreateAssetMenu(fileName = "Bullet settings", menuName = "Test/Bullet settings")]
public class BulletSettings : ScriptableObject
{
    public float ImpulseMagnitude;
}
