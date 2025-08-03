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
    //    var c = Instantiate(this); //�������� �����ҋ����� ������
    //    //c.Ammo = 0;
    //    c.name = name + " (Runtime)";
    //    c.hideFlags = HideFlags.DontSave; // ��/������Ʈ�� ���� ����
    //    return c;
    //}
    public object Clone()
    {
        var c = Instantiate(this); //�������� �����ҋ����� ������

        return c;
    }
}
