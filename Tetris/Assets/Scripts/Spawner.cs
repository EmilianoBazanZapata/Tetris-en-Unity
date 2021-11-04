using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] LevelPices;
    public GameObject CurrentPiece, NextPieceTetris , NextPiece;
    public static Vector3 NextPosition;
    private void Start()
    {
        NextPieceTetris = Instantiate(LevelPices[0], this.transform.position , Quaternion.identity);
        SpawnNextPiece();
    }
    public void SpawnNextPiece()
    {
        CurrentPiece = NextPieceTetris;
        CurrentPiece.GetComponent<Piece>().enabled = true;
        //decido aleatoriamente que pieza debe spawnearse
        int i = Random.Range(0, LevelPices.Length);
        var next = i;
        //se instancia en la misma posicion del padre ademas de mantener su rotacion
        NextPieceTetris = Instantiate(LevelPices[i], this.transform.position, Quaternion.identity);
        NextPieceTetris.GetComponent<Piece>().enabled = false;
        NextPieceTetris.SetActive(false);
        ViewNextPiece();
    }

    public void ViewNextPiece()
    {
        Destroy(NextPiece);
        var pos = new Vector3(6.4f,26.5f,0.0f);
        NextPiece = Instantiate(NextPieceTetris, pos, Quaternion.identity);
        NextPiece.GetComponent<Piece>().enabled = false;
        NextPiece.SetActive(true);
    }
    public void ActivatePiece()
    {
        CurrentPiece.SetActive(true);
    }
}