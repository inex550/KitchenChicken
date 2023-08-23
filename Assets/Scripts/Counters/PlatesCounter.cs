using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter {

    [SerializeField] private float spawnPlatesTimerMax = 4.0f;
    [SerializeField] private int platesSpawnedCountMax = 4;
    [SerializeField] private float plateOffsetY = 0.1f;

    [Header(Strings.presetFields)]
    [SerializeField] private Transform platesSpawnPoint;
    [SerializeField] private Transform platePrefab;

    private List<ChickenTool> chickenTools;

    private float spawnPlatesTimer;
    private int platesSpawnedCount;

    private void Awake() {
        chickenTools = new List<ChickenTool>();
    }

    public override void Interact(Player player) {
        if (!player.HasChickenTool() && platesSpawnedCount > 0) {
            ChickenTool lastPlate = chickenTools[platesSpawnedCount - 1];
            lastPlate.SetParent(player);
            
            chickenTools.RemoveAt(platesSpawnedCount - 1);

            platesSpawnedCount -= 1;
        }
    }

    private void Update() {
        spawnPlatesTimer += Time.deltaTime;

        if (spawnPlatesTimer > spawnPlatesTimerMax) {
            SpawnPlate();
            spawnPlatesTimer = 0.0f;
        }
    }

    private void SpawnPlate() {
        if (platesSpawnedCount < platesSpawnedCountMax) {
            Transform plateTransform = Instantiate(platePrefab, platesSpawnPoint);
            plateTransform.localPosition = new Vector3(0, plateOffsetY * platesSpawnedCount, 0);

            chickenTools.Add(plateTransform.GetComponent<ChickenTool>());

            platesSpawnedCount += 1;
        }
    }
}