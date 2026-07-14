using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public static RoomGenerator Instance { get; private set; }

    [SerializeField] private GameObject[] roomPrefabs;
    [SerializeField] private GameObject[] corridorPrefabs;
    [SerializeField] private GameObject[] endingRoomPrefabs;
    [SerializeField, Range(0f, 1f)] private float corridorChance = 0.35f;
    [SerializeField, Range(0f, 1f)] private float earlyEndChance = 0.1f;
    [SerializeField] private int maxRoomDepth = 8;
    [SerializeField] private LayerMask overlapMask = ~0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void CreateRoom(RoomExit sourceExit, int depth)
    {
        if (sourceExit == null) { return; }
        if (depth > maxRoomDepth) { return; }

        GameObject[] prefabsToTry = ShufflePrefabs(PickPrefabSet(depth));

        for (int prefabIndex = 0; prefabIndex < prefabsToTry.Length; prefabIndex++)
        {
            GameObject prefab = prefabsToTry[prefabIndex];
            if (prefab == null)
            {
                continue;
            }

            GameObject roomInstance = Instantiate(prefab);

            if (TryAlignToExit(roomInstance, sourceExit, depth))
            {
                return;
            }

            Destroy(roomInstance);
        }
    }

    private static GameObject[] ShufflePrefabs(GameObject[] prefabs)
    {
        if (prefabs == null || prefabs.Length <= 1)
        {
            return prefabs;
        }

        GameObject[] shuffledPrefabs = (GameObject[])prefabs.Clone();

        for (int i = shuffledPrefabs.Length - 1; i > 0; i--)
        {
            int swapIndex = Random.Range(0, i + 1);
            GameObject temp = shuffledPrefabs[i];
            shuffledPrefabs[i] = shuffledPrefabs[swapIndex];
            shuffledPrefabs[swapIndex] = temp;
        }

        return shuffledPrefabs;
    }

    private GameObject[] PickPrefabSet(int depth)
    {
        if (depth >= maxRoomDepth) // if room is at the final depth, spawn an ending room without an exit
        {
            return endingRoomPrefabs;
        }

        if (Random.value < earlyEndChance && depth > 6) // if we're past the 4th room, have a chance to end the generation early
        {
            return endingRoomPrefabs;
        }

        if (Random.value < corridorChance) // have a chance to spawn a corridor room
        {
            return corridorPrefabs;
        }

        return roomPrefabs; // if nothing above triggers, selects a normal room
    }

    private bool TryAlignToExit(GameObject roomInstance, RoomExit sourceExit, int depth)
    {
        RoomEntrance[] entrances = roomInstance.GetComponentsInChildren<RoomEntrance>(true);
        if (entrances == null || entrances.Length == 0)
        {
            Debug.LogWarning($"[RoomGenerator] '{roomInstance.name}' has no RoomEntrance sockets.");
            return false;
        }

        RoomEntrance entrance = FindBestEntrance(entrances, sourceExit.ExitType);
        if (entrance == null)
        {
            Debug.LogWarning($"[RoomGenerator] '{roomInstance.name}' could not find a matching entrance for source exit {sourceExit.ExitType}. Available: {GetEntranceList(entrances)}");
            return false;
        }

        Debug.Log($"[RoomGenerator] Spawning '{roomInstance.name}' from exit {sourceExit.ExitType} using entrance {entrance.EntranceType}.");

        Vector3 positionOffset = sourceExit.transform.position - entrance.transform.position;
        roomInstance.transform.position += positionOffset;

        if (DoesOverlapExistingGeometry(roomInstance))
        {
            Debug.LogWarning($"[RoomGenerator] '{roomInstance.name}' overlaps existing geometry and was rejected.");
            return false;
        }

        foreach (RoomExit exit in roomInstance.GetComponentsInChildren<RoomExit>(true))
        {
            exit.Initialize(depth);
        }

        return true;
    }

    private static RoomEntrance FindBestEntrance(RoomEntrance[] entrances, RoomSocketDirection sourceDirection)
    {
        RoomSocketDirection targetDirection = GetOppositeDirection(sourceDirection);

        for (int i = 0; i < entrances.Length; i++)
        {
            if (entrances[i] != null && entrances[i].EntranceType == targetDirection)
            {
                return entrances[i];
            }
        }

        return null;
    }

    private static string GetEntranceList(RoomEntrance[] entrances)
    {
        if (entrances == null || entrances.Length == 0)
        {
            return "none";
        }

        string result = string.Empty;

        for (int i = 0; i < entrances.Length; i++)
        {
            if (entrances[i] == null)
            {
                continue;
            }

            if (result.Length > 0)
            {
                result += ", ";
            }

            result += entrances[i].EntranceType;
        }

        return result.Length > 0 ? result : "none";
    }

    private bool DoesOverlapExistingGeometry(GameObject roomInstance)
    {
        Collider[] colliders = roomInstance.GetComponentsInChildren<Collider>(true);
        if (colliders == null || colliders.Length == 0)
        {
            return false;
        }

        for (int colliderIndex = 0; colliderIndex < colliders.Length; colliderIndex++)
        {
            Collider roomCollider = colliders[colliderIndex];
            if (roomCollider == null || roomCollider.isTrigger)
            {
                continue;
            }

            Bounds bounds = roomCollider.bounds;
            Collider[] hits = Physics.OverlapBox(bounds.center, bounds.extents, Quaternion.identity, overlapMask, QueryTriggerInteraction.Ignore);

            for (int hitIndex = 0; hitIndex < hits.Length; hitIndex++)
            {
                Collider hit = hits[hitIndex];
                if (hit == null || hit.transform.IsChildOf(roomInstance.transform))
                {
                    continue;
                }

                if (!hit.isTrigger)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static RoomSocketDirection GetOppositeDirection(RoomSocketDirection direction)
    {
        switch (direction)
        {
            case RoomSocketDirection.North:
                return RoomSocketDirection.South;
            case RoomSocketDirection.South:
                return RoomSocketDirection.North;
            case RoomSocketDirection.East:
                return RoomSocketDirection.West;
            case RoomSocketDirection.West:
                return RoomSocketDirection.East;
            default:
                return direction;
        }
    }

}