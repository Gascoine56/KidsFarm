using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackToMapButton : MonoBehaviour
{
    public void BackToMap()
    {
        SceneTransferManager.ReturnToMapScene();
    }
}
