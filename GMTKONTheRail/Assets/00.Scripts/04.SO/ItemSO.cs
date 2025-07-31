using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemTypeSO", menuName = "Scriptable Objects/ItemTypeSO")]
public class ItemSO : ScriptableObject,ICloneable
{
    
    public int Ammo = 0;
    public int Durability = 10;
    public ItemUseableObject _prefab;

    public object Clone()
    {
        return Instantiate(this); //히히히하 생성할떄마다 복제밍
    }
}
