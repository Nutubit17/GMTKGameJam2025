using UnityEngine;

public class StreetLamp : LightObject, IInteractiveable
{
    public void Intreractive()
    {
        AddFuel(5);
    }
}
