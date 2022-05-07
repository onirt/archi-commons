using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] SpawnData groundHumanoid;
    [SerializeField] SpawnData groundRobot;
    [SerializeField] SpawnData flyRobot;
    [SerializeField] SpawnData groundVehicle;
    [SerializeField] SpawnData flyVehicle;

    [SerializeField] Transform[] groundHumanoidSpawnPoints;
    [SerializeField] Transform[] groundRobotsSpawnPoints;
    [SerializeField] Transform[] flyRobotsSpawnPoints;
    [SerializeField] Transform[] groundVehiclesSpawnPoints;
    [SerializeField] Transform[] flyVehiclesSpawnPoints;

    public void SpawnGroundVehicle(int level)
    {
        Transform point = groundVehiclesSpawnPoints[Random.Range(0, groundVehiclesSpawnPoints.Length)];
        StartCoroutine(groundVehicle.Instantiate(point, level, null));
    }
    public void SpawnFlyVehicle(int level)
    {
        Transform point = flyVehiclesSpawnPoints[Random.Range(0, flyVehiclesSpawnPoints.Length)];
        StartCoroutine(flyVehicle.Instantiate(point, level, null));
    }
    public void SpawnFlyRobot(int level)
    {
        Transform point = flyRobotsSpawnPoints[Random.Range(0, flyRobotsSpawnPoints.Length)];
        StartCoroutine(flyRobot.Instantiate(point, level, null));
    }
    public void SpawnGroundRobot(int level)
    {
        Transform point = groundRobotsSpawnPoints[Random.Range(0, groundRobotsSpawnPoints.Length)];
        StartCoroutine(groundRobot.Instantiate(point, level, null));
    }
    public void SpawnGroundHumanoid(int level)
    {
        Transform point = groundHumanoidSpawnPoints[Random.Range(0, groundHumanoidSpawnPoints.Length)];
        StartCoroutine(groundHumanoid.Instantiate(point, level, null));
    }
}