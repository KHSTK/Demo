using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour, IInteractable
{
    public SceneLoadEventSO loadEventSO;
    public GameSceneSO sceneToGo;
    public Vector3 positionToGo;
    public void TriggerAction()
    {
        Debug.Log("����");
        //�����¼��������������
        loadEventSO.RaisedLoadRequestEvent(sceneToGo, positionToGo, true);
    }
}
