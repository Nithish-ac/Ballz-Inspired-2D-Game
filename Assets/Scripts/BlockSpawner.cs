using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField]
    private Block blockPrefab;
    [SerializeField]
    private Block pblockPrefab;

    private int playWidth = 7; // Number of blocks in a row
    private float distanceBetweenBlock;
    private float spawnRangeMin = -2.25f; // Minimum x-axis position
    private float spawnRangeMax = 2.25f;  // Maximum x-axis position
    private int rowsSpawned;

    private List<Block> blocksSpawned = new List<Block>();

    private void OnEnable()
    {
        // Calculate distance between blocks based on range and playWidth
        distanceBetweenBlock = (spawnRangeMax - spawnRangeMin) / (playWidth - 1);

        for (int i = 0; i < 1; i++)
        {
            SpawnRowOfBlocks();
        }
    }

    public void SpawnRowOfBlocks()
    {
        // Move existing blocks down
        foreach (var block in blocksSpawned)
        {
            if (block != null)
            {
                block.transform.position += new Vector3(0, -0.85f, 0); // Move blocks down by -0.75
            }
        }

        // Randomly pick one index for the pblock
        int pblockIndex = UnityEngine.Random.Range(0, playWidth);

        // Spawn new blocks in the row
        for (int i = 0; i < playWidth; i++)
        {
            if (i == pblockIndex)
            {
                // Spawn the pblock at the chosen index
                var pblock = Instantiate(pblockPrefab, GetPosition(i), Quaternion.identity);
                pblock.SetHits(1); // Assuming pblock has a single hit or some unique behavior

                blocksSpawned.Add(pblock);
            }
            else if (UnityEngine.Random.Range(0, 100) <= 30) // 30% chance to spawn a normal block
            {
                var block = Instantiate(blockPrefab, GetPosition(i), Quaternion.identity);
                int hits = UnityEngine.Random.Range(1, 3) + rowsSpawned;

                block.SetHits(hits);

                blocksSpawned.Add(block);
            }
        }

        rowsSpawned++;
    }

    private Vector3 GetPosition(int i)
    {
        // Calculate position based on spawn range and index
        float xPosition = spawnRangeMin + i * distanceBetweenBlock;
        return new Vector3(xPosition, transform.position.y, transform.position.z);
    }
}
