using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Level : ScriptableObject
{
    public int startKnifeAmount;
    public int stickedKnivesAmount;
    [Range(0f, 1f)]
    public float appleChance;
    public float logSpeed;
    public AnimationCurve logRollingCurve;
    public GameObject logModel;
}
