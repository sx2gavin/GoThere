using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTerrainGenerator : MonoBehaviour
{
    public GameObject landBlock;
    public int landBlockSize = 10;
    public int numBlocksOneSide = 10;
    public Vector3 landStartingPosition = new Vector3(-100, -100, -100);
    public int maxHeight = 10;

    // Start is called before the first frame update
    void Start()
    { 
        GenerateTerrain();
    }

    void GenerateTerrain()
    {
        int[][] heightMap = new int[numBlocksOneSide][];

        for (int i = 0; i < numBlocksOneSide; i++)
        {
            heightMap[i] = new int[numBlocksOneSide];
            for (int j = 0; j < numBlocksOneSide; j++)
            {
                var height = Random.Range(1, maxHeight);
                heightMap[i][j] = height;
                for (int k = 0; k < height; k++)
                {
                    Vector3 position = landStartingPosition + landBlockSize * new Vector3(i, k, j);
                    var block = Instantiate(landBlock, position, Quaternion.identity);
                }
            }
        }
    }
}
