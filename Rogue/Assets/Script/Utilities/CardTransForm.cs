using UnityEngine;

public struct CardTransForm
{
    public Vector3 pos;
    public Quaternion rot;
    public CardTransForm(Vector3 vector3, Quaternion quaternion)
    {
        pos = vector3;
        rot = quaternion;
    }
}
