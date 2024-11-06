using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName ="Map/RoomDataSO")]
public class RoomDataSo : ScriptableObject
{
    public Sprite roomIcon;
    public RoomType roomType;
    public AssetReference senceToLoad;
}
