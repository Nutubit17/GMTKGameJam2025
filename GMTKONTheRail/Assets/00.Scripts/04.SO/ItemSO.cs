using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemTypeSO", menuName = "Scriptable Objects/ItemTypeSO")]

public class ItemSO : ScriptableObject,ICloneable
{
    
    //public int Ammo = 0;
    //public int Durability = 10;
    public ItemObject Prefab;
    public Texture2D ItemIcon;

    //public object RuntimeClone()
    //{
    //    var c = Instantiate(this); //히히히하 생성할떄마다 복제밍
    //    //c.Ammo = 0;
    //    c.name = name + " (Runtime)";
    //    c.hideFlags = HideFlags.DontSave; // 씬/프로젝트에 저장 방지
    //    return c;
    //}
    public object Clone()
    {
        var c = Instantiate(this); //히히히하 생성할떄마다 복제밍

        return c;
    }
}
