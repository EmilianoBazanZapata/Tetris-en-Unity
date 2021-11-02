using System.Collections;
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
}