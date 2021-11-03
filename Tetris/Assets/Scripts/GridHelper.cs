using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHelper : MonoBehaviour
{
    public static int Width = 10, Height = 20 + 4;
    public static Transform[,] Grid = new Transform[Width, Height];

    //redondear las posiciones
    public static Vector2 RoundVector(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }
    //verificar si la pieza esta dentro de la parilla
    public static bool IsInsideBorders(Vector2 pos)
    {
        if (pos.x >= 0 && pos.y >= 0 && pos.x < Width)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //metodo para borrar toda la fila
    public static void DeleteRow(int y)
    {
        for (int x = 0; x < Width; x++)
        {
            //elimina el objeto
            Destroy(Grid[x, y].gameObject);
            //libera el espacio
            Grid[x, y] = null;
        }
    }

    //bajar las demas filas
    public static void DecreaseRow(int y)
    {
        for (int x = 0; x < Width; x++)
        {
            if (Grid[x, y] != null)
            {
                Grid[x, y - 1] = Grid[x, y];
                Grid[x, y] = null;
                //repintamos el bloque una posicion mas abajo
                Grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public static void DecreaseRowsAbove(int y)
    {
        for (int i = y; i < Height; i++)
        {
            DecreaseRow(i);
        }
    }
    //verificar si una fila esta completa
    public static bool IsRowFull(int y)
    {
        for (int x = 0; x < Width; x++)
        {
            if (Grid[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }
    //borrar todas las filas
    public static void DeleteAllFullRows()
    {
        for (int y = 0; y < Height; y++)
        {
            if (IsRowFull(y))
            {
                DeleteRow(y);
                DecreaseRowsAbove(y + 1);
                y--;
            }
        }
        CleanPieces();
    }
    //si una pieza ya no tiene hijos sera eliminada completamente del escenario
    private static void CleanPieces()
    {
        foreach (GameObject piece in GameObject.FindGameObjectsWithTag("Piece"))
        {
            if (piece.transform.childCount == 0)
            {
                Destroy(piece);
            }
        }
    }
}