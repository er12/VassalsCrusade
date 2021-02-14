using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//the way the attack can be used
public enum AttackBehaviourTrigger
{
    Click,
    Hold,
    Charge

}

[CreateAssetMenu(fileName = "AttackSO", menuName = "ScriptableObjects/AttackScriptableObject", order = 1)]
public class AttackScriptableObject : ScriptableObject
{
    public string attackName;

    public GameObject prefab;

    public AttackBehaviourTrigger attackBehaviourTrigger;

}
