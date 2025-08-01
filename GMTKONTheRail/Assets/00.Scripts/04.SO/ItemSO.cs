using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemTypeSO", menuName = "Scriptable Objects/ItemTypeSO")]
public class ItemSO : ScriptableObject,ICloneable
{
    
    public int Ammo = 0;
    public int Durability = 10;
    public ItemObject Prefab;
    public Texture2D ItemIcon;

    public object Clone()
    {
        return Instantiate(this); //히히히하 생성할떄마다 복제밍
    }
}
