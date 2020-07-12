using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
  public int objectCount = 10;
  private Dictionary<GameObject, ItemSpawnSetting> itemSpawnSettings;
  public List<SurfaceTypeComponent> objectsWithSurfaces;

  // Start is called before the first frame update
  void Start()
  {
    itemSpawnSettings = new Dictionary<GameObject, ItemSpawnSetting>();
    ItemSpawnSetting[] settings = GetComponentsInChildren<ItemSpawnSetting>();
    foreach (ItemSpawnSetting itemSetting in settings)
    {
      itemSpawnSettings.Add(itemSetting.itemPrefab, itemSetting);
    }

    objectsWithSurfaces = FindObjectsOfType<SurfaceTypeComponent>().ToList();

    for (int i = 0; i < objectCount; i++)
    {
      SpawnRandomObject();
    }
  }


  // Update is called once per frame
  void Update()
  {
    // This is for testing only
    if (Input.GetKeyDown("g"))
    {
      SpawnRandomObject();
    }
  }

  public void SpawnRandomObject()
  {
    int randSpawnNumber = Random.Range(0, itemSpawnSettings.Count);
    var spawnChoice = itemSpawnSettings.ElementAt(randSpawnNumber);
    var legalSpawnSurfaces = spawnChoice.Value.legalSpawnSurfaces;
    List<SurfaceTypeComponent> legalSurfaces = objectsWithSurfaces.
        Where(surface => legalSpawnSurfaces.Intersect(surface.types).Any()).ToList();
    if (legalSurfaces.Count <= 0)
    {
      Debug.LogWarningFormat("Tried to spawn a {0} but could find no legal surfaces.");
      return;
    }

    int randSurfaceNumber = Random.Range(0, legalSurfaces.Count);
    GameObject surfaceForSpawning = legalSurfaces[randSurfaceNumber].gameObject;
    Bounds worldBounds = surfaceForSpawning.GetComponent<Renderer>().bounds;

    float randXLoc = Random.Range(worldBounds.min.x + .1f, worldBounds.max.x - .1f);
    float randZLoc = Random.Range(worldBounds.min.z + .1f, worldBounds.max.z - .1f);
    float yVal = worldBounds.max.y + .1f;
    Vector3 generationPoint = new Vector3(randXLoc, yVal, randZLoc);
    Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

    Instantiate(spawnChoice.Key, generationPoint, rotation);
  }
}
