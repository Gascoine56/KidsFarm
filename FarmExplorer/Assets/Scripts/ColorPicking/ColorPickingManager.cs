using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ColorPickingManager : MonoBehaviour
{
    private Dictionary<string, Color> colorsToPick = new Dictionary<string, Color>()
    {
        { "RED", new Color(1, 0, 0) },
        { "BLUE", new Color(0.4f, 0.4f, 1) },
        { "GREEN", new Color(0.4f, 0.8f, 0.4f) },
        { "YELLOW", new Color(1, 1, 0) }
    };

    [SerializeField] private SpriteRenderer box;
    [SerializeField] private int numberOfObjectsToSpawn;
    [SerializeField] private PickableObject prefab;

    private string colorToPick;

    private void Awake()
    {
        colorToPick = GetRandomColor();
        box.GetComponent<SpriteRenderer>().color = colorsToPick[colorToPick];
        SpawnPickableObjects();
    }

    private void SpawnPickableObjects()
    {
        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            Vector2 position = GetRandomPosition();
            Debug.Log(position);
            Instantiate(prefab, position, Quaternion.identity);
            //check if object overlap with existing object, change position if it does
            //set color and image of prefab
        }
    }

    private Vector2 GetRandomPosition()
    {
        float positionX = Random.Range(-10f, 10.01f);
        float positionY = Random.Range(-2.5f, 5.01f);

        return new Vector2(positionX, positionY);
    }

    private string GetRandomColor()
    {
        int colorToPickIndex = Random.Range(0, colorsToPick.Count);
        return colorsToPick.ElementAt(colorToPickIndex).Key;
    }
}
