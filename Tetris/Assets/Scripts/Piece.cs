using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    private float LastFall = 0.0f;
    private void Start()
    {
        /*if (!IsValidPiecePosition())
        {
            Debug.Log("Game Over");
            Destroy(this.gameObject);
        }*/
    }
    private void Update()
    {
        //movimiento a la izquierda
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MovePieceHorizontal(-1);
        }
        else
        //movimiento a la derecha
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MovePieceHorizontal(1);
        }
        else
        //rotar la pieza
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            RotatePiece(90);
        }
        else
        //mover la ficha hacia abajo
        if (Input.GetKey(KeyCode.DownArrow) || (Time.time - LastFall) > 1.0f)
        {
            this.transform.position += new Vector3(0, -1, 0);
            if (IsValidPiecePosition())
            {
                //actualizo la parilla y guardo la nueva posicion
                UpdateGrid();
            }
            else
            {
                FindObjectOfType<Spawner>().ActivatePiece();
                this.transform.position += new Vector3(0, 1, 0);
                //como la pieza no puede bajar mas , a lo mejor se puede eliminar alguna fila 
                GridHelper.DeleteAllFullRows();
                //hacemos que aparezca una nueva ficha
                FindObjectOfType<Spawner>().SpawnNextPiece();
                //deshabilitar el script para que ya no se mueva la pieza
                this.enabled = false;
                if (this.transform.position.y > 19)
                {
                    Debug.Log("perdiste");
                }
                else
                {
                    FindObjectOfType<Spawner>().ActivatePiece();
                }

            }
            //cada un segundo la pieza deciende
            LastFall = Time.time;
        }

    }
    private void RotatePiece(int direction)
    {
        this.transform.Rotate(0, 0, -direction);
        if (IsValidPiecePosition())
        {
            //actualizo la parilla y guardo la nueva posicion
            UpdateGrid();
        }
        else
        {
            this.transform.Rotate(0, 0, direction);
        }
    }
    private void MovePieceHorizontal(int direction)
    {
        //muevo la pieza a la izquierda o  a la drecehca , dependiendo del parametro que me llegue
        this.transform.position += new Vector3(direction, 0, 0);
        //compruebo que la posicion sea valida
        if (IsValidPiecePosition())
        {
            //actualizo la parilla y guardo la nueva posicion
            UpdateGrid();
        }
        else
        {
            //si la posicion no es validad , revierto el movimiento 
            this.transform.position += new Vector3(-direction, 0, 0);
        }
    }
    private bool IsValidPiecePosition()
    {
        foreach (Transform block in this.transform)
        {
            //redondeo la posicion del bloque
            Vector2 Pos = GridHelper.RoundVector(block.position);
            //valido si la posicion es valida
            //para saber si esta fuera de los limites
            if (!GridHelper.IsInsideBorders(Pos))
            {
                return false;
            }
            //si ya hay otro bloque en esa misma posicion tampoco es valida
            Transform PossibleObject = GridHelper.Grid[(int)Pos.x, (int)Pos.y];
            if (PossibleObject != null && PossibleObject.parent != this.transform)
            {
                return false;
            }
        }
        return true;
    }

    //refrescar la estructura de datos
    private void UpdateGrid()
    {
        for (int y = 0; y < GridHelper.Height; y++)
        {
            for (int x = 0; x < GridHelper.Width; x++)
            {
                //el padre del bloque es el propio del script 
                if (GridHelper.Grid[x, y] != null && GridHelper.Grid[x, y].parent == this.transform)
                {
                    GridHelper.Grid[x, y] = null;
                }
            }
        }
        foreach (Transform block in this.transform)
        {
            Vector2 pos = GridHelper.RoundVector(block.position);
            GridHelper.Grid[(int)pos.x, (int)pos.y] =
            block;
        }

    }
}