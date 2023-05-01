using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Object : MonoBehaviour
{
    public abstract void SetStartPosition(Transform newSpawnedBuildingTransform, SpawnBuilding newSpawnedBuildingData, BoxCollider2D newSpawnedBuildingCollider);
}
