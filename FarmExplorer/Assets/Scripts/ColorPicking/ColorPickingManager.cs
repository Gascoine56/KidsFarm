using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class ColorPickingManager : MonoBehaviour
{
    public static ColorPickingManager Instance { get; private set; }

    private Dictionary<string, Color> colorsToPick = new Dictionary<string, Color>()
    {
        { "RED", new Color(1, 0, 0) },
        { "BLUE", new Color(0.4f, 0.4f, 1) },
        { "GREEN", new Color(0.4f, 0.8f, 0.4f) },
        { "YELLOW", new Color(1, 1, 0) }
    };

    [SerializeField] private List<Transform> spawnPoints;

    [SerializeField] private PickableObjectStorage pickableObjectStorage;
    [SerializeField] private PickableObject prefab;
    [SerializeField] private Sprite pickableObjectSprite;

    private string colorToPick;

    private void Awake()
    {
        if (Instance != null) print("There is more than one Player instance");
        Instance = this;

        SpawnPickableObjects();
        SetColorToPick();
        CheckIfColorToPickItemsOnScreen();
    }

    private void SetColorToPick()
    {
        colorToPick = GetRandomColor();
        pickableObjectStorage.SetColor(colorsToPick[colorToPick]);
        colorsToPick.Remove(colorToPick);
    }

    private void SpawnPickableObjects()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            PickableObject pickableObject =  Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            string color = GetRandomColor();
            pickableObject.SetSpriteAndColor(pickableObjectSprite, colorsToPick[color], color);
        }
    }

    public void CheckIfColorToPickItemsOnScreen()
    {
        int remainingObjectsOfColorToPick = 0;
        PickableObject[] obj = (PickableObject[])FindObjectsOfType(typeof(PickableObject));
        foreach (PickableObject o in obj)
        {
            if (o.GetColorName().Equals(colorToPick))
            {
                remainingObjectsOfColorToPick++;
            }
        }
        Debug.Log(remainingObjectsOfColorToPick);
        if(remainingObjectsOfColorToPick <= 0)
        {
            if (colorsToPick.Count > 0)
            {
                SetColorToPick();
                CheckIfColorToPickItemsOnScreen();
                //some animation of changing the color and yay hura
            }
            else
            {
                //VictoryScreen, pause game and afterwards:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private string GetRandomColor()
    {
        int colorToPickIndex = Random.Range(0, colorsToPick.Count);
        return colorsToPick.ElementAt(colorToPickIndex).Key;
    }

    public string GetColorToPick()
    {
        return colorToPick;
    }

    public Vector3 GetPickableObjectStoragePosition()
    {
        return pickableObjectStorage.transform.position;
    }
}
