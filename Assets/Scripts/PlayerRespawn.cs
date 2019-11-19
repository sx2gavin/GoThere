using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Vector3 lastCheckpointPos;
    public bool useCheckpointPos = true;

    private PlayerMovement player;

    private static PlayerRespawn instance;
    public static PlayerRespawn Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        if (useCheckpointPos)
        {
            instance.ResetPlayer();
        }
    }

    // Start is called before the first frame update
    void ResetPlayer()
    {
        player = FindObjectOfType<PlayerMovement>();
        player.transform.position = lastCheckpointPos;
    }

    public void SetNewCheckpoint(Vector3 checkpointPos)
    {
        lastCheckpointPos = checkpointPos;
    }

    public void DontDestroy()
    {
        DontDestroyOnLoad(this);
    }

}
