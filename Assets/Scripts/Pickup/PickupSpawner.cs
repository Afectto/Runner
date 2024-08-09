using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ButchersGames
{
    public class PickupSpawner : MonoBehaviour
    {
        [SerializeField] private List<Pickup> pickupsPrefab;
        [SerializeField] private float spawnRange = 2.5f;
        private Level _level;
        private List<Transform> _waypoints;
        private readonly float _minDistance = 0.5f;

        void Start()
        {
            _level = GetComponent<Level>();
            _waypoints = _level.WayPoints;
            SpawnPickups();
        }
        
        private void SpawnPickups()
        {
            for (int i = 0; i < _waypoints.Count - 1; i++)
            {
                Vector3 start = _waypoints[i].position;
                Vector3 end = _waypoints[i + 1].position;
                float segmentLength = Vector3.Distance(start, end);
        
                int pickupsCount = Mathf.FloorToInt(segmentLength / _minDistance);
        
                Vector3 direction = (end - start).normalized;
                Vector3 perpendicularDirection = new Vector3(-direction.z, 0, direction.x);

                for (int j = 0; j < pickupsCount; j++)
                {
                    float t = (j + 1) / (float)(pickupsCount + 1);
                    Vector3 spawnPosition = Vector3.Lerp(start, end, t);

                    float randomOffset = Random.Range(-spawnRange / 2, spawnRange / 2);
                    spawnPosition += perpendicularDirection * randomOffset;

                    if (Vector3.Distance(spawnPosition, _waypoints[i].position) >= 3f &&
                        (i + 1 >= _waypoints.Count || Vector3.Distance(spawnPosition, _waypoints[i + 1].position) >= 3f))
                    {
                        int randomIndex = Random.Range(0, pickupsPrefab.Count);
                        Instantiate(pickupsPrefab[randomIndex], spawnPosition, Quaternion.identity);
                    }
                }
            }
        }
    }
}