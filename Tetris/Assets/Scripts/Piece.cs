﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
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
            Vector2 pos = GridHelpher.RoundVector(block.position);
            GridHelper.Grid[(int)pos.x, (int)pos.y] =
            block;
        }

    }
}