using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionTrigger : MonoBehaviour
{
    public string instruction = "";
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameController.UpdateInstructionText(instruction);
        }
    }
}
