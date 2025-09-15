using UnityEngine;

public class StreetLamp : LightObject, IInteractiveable
{
    //public void Intreractive()
    //{

    //}

    ItemDataAndSO IInteractiveable.Intreractive()
    {
        AddFuel(5);
        return null;
    }
}
