using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnerStateMachine))]
public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private int NumberOfZombieToSpawn;
    public GameObject[] ZombiePrefabs;
    public SpawnerVolumes[] SpawnVolumes;

    public GameObject TargetObject => FollowGameObject;
    private GameObject FollowGameObject;

    private SpawnerStateMachine StateMachine;

    private void Awake()
    {
        StateMachine = GetComponent<SpawnerStateMachine>();
        FollowGameObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        ZombieWaveSpawnerState beginnerWave = new ZombieWaveSpawnerState(this, StateMachine)
        {
            ZombiesToSpawn = 5,
            NextState = SpawnerStateEnum.Complete
        };
        StateMachine.AddState(SpawnerStateEnum.Beginner, beginnerWave);

        StateMachine.Initialize(SpawnerStateEnum.Beginner);

        //for (int zombieCount = 0; zombieCount < NumberOfZombieToSpawn; zombieCount++)
        //{
        //    SpawnZombie();
        //}
    }

    private void SpawnZombie()
    {
        GameObject zombieToSpawn = ZombiePrefabs[Random.Range(0, ZombiePrefabs.Length)];
        SpawnerVolumes spawnVolume = SpawnVolumes[Random.Range(0, SpawnVolumes.Length)];

        GameObject zombie = Instantiate(zombieToSpawn, spawnVolume.GetPositionInBounds(), spawnVolume.transform.rotation);

        zombie.GetComponent<ZombieComponent>().Initialize(FollowGameObject);
    }
}
