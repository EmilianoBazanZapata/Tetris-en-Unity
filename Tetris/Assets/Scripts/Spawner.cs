using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] LevelPices;
    private void Start() {
        SpawnNetPiece();
    }
    public void SpawnNetPiece()
    {
        //decido aleatoriamente que pieza debe spawnearse
        int i = Random.Range(0,LevelPices.Length);
        //se instancia en la misma posicion del padre ademas de mantener su rotacion
        Instantiate(LevelPices[i],this.transform.position, Quaternion.identity);
    }
}
