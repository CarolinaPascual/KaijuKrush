using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Board
{
    const int STATE_END = 0;
    const int STATE_NORMAL = 1;
    const int STATE_CHANGING = 2;
    const int STATE_ARRANGING = 3;


    public List<List<Tile>> matrixBoard { get; set; }
    private int boardHeight = 7;
    private int boardWidth = 7;
    private int[] firstSelect = { -1, -1 };
    private int[] secondSelect = { -1, -1 };
    public int movementsLeft { get; set; }
    public int targetScore { get; set; }
    public int current_state { get; set; }


    public Board()
    {

        current_state = STATE_ARRANGING;
        matrixBoard = new List<List<Tile>>();
        for (int i = 0; i < boardHeight; i++)
        {
            matrixBoard.Add(new List<Tile>());
            for (int j = 0; j < boardWidth; j++)
            {
                if ((j + i) % 2 == 0)
                {
                    Tile aux1 = new Tile("Sprites/Placeholders_Prototype/tile_Unselected01");
                    matrixBoard[i].Add(aux1);

                }
                else
                {
                    Tile aux1 = new Tile("Sprites/Placeholders_Prototype/tile_Unselected01");
                    matrixBoard[i].Add(aux1);
                }

                int boardWidthPx = (boardWidth * (int)matrixBoard[i][j].background.getWidth());
                int offsetTopLeft = (CGameConstants.SCREEN_WIDTH - boardWidthPx) / 2;
                int offsetCenter = offsetTopLeft + (int)matrixBoard[i][j].background.getWidth() / 2;

                matrixBoard[i][j].background.setY(i * matrixBoard[i][j].background.getHeight() + 600);
                matrixBoard[i][j].background.setX(j * matrixBoard[i][j].background.getWidth() + offsetCenter);
                matrixBoard[i][j].background.setName("Background " + i + " " + j);

            }

        }
        //fillBoard();
        fillSpaces();

    }

    public void fillBoard()
    {
        System.Random rng = new System.Random();
        for (int i = 0; i < matrixBoard.Count(); i++)
        {
            for (int j = 0; j < matrixBoard[i].Count(); j++)
            {
                int iconAux = 0;
                bool invalidIcon = true;
                while (invalidIcon)
                {
                    iconAux = rng.Next(0, 4);
                    invalidIcon = checkMatchCreate(iconAux, i, j);
                    //invalidIcon = false;
                }
                matrixBoard[i][j].setIcon("Sprites/Placeholders_Prototype/food" + iconAux.ToString());
                matrixBoard[i][j].food.Type = iconAux;
                matrixBoard[i][j].food.imgIcon.setX(matrixBoard[i][j].background.getX());
                matrixBoard[i][j].food.imgIcon.setY(matrixBoard[i][j].background.getY());
                matrixBoard[i][j].food.finalX = matrixBoard[i][j].background.getX();
                matrixBoard[i][j].food.finalY = matrixBoard[i][j].background.getY();
            }
        }

    }
    public void update()
    {
        for (int i = 0; i < matrixBoard.Count(); i++)
        {
            for (int j = 0; j < matrixBoard[0].Count(); j++)
            {
                matrixBoard[i][j].update();
            }
        }
        switch (current_state)
        {
            case STATE_END:
                break;
            case STATE_NORMAL:
                if (CKeyboard.pressed(CKeyboard.LEFT))
                {
                    CurrentStageData.currentKaiju.firstPower();
                    return;
                }
                tokenSelection();
                break;
            case STATE_CHANGING:
                switchingBehavior();
                break;
            case STATE_ARRANGING:
                if (boardState() == 0)
                {
                    if (checkMatches(true))
                    {
                        countScore();
                        deleteMatches();                        
                        cascadeBoard1();
                        fillSpaces();
                    }
                    else
                    {
                        current_state = STATE_NORMAL;
                        if (!possibleMatches())
                        {
                            fillBoard();
                        }
                        if (movementsLeft <= 0 |CurrentStageData.currentKaiju.scale >= 100 ) {
                            
                            current_state = STATE_END;
                        }
                    }
                }
                break;
        }
    }

    //Comportamiento del Board en STATE_NORMAL
    public void tokenSelection()
    {
        if (CMouse.firstPress(0))
        {

            int[] mousePos = getMouseTile();
            if (mousePos[0] != -1 & secondSelect[0] == -1)
            {
                firstSelect[0] = mousePos[0];
                firstSelect[1] = mousePos[1];
                matrixBoard[firstSelect[0]][firstSelect[1]].select();
            }

        }
        if (CMouse.pressed(0))
        {
            int[] mousePos = getMouseTile();
            if (mousePos[0] != firstSelect[0] | mousePos[1] != firstSelect[1]) { 
            if (mousePos[0] == -1 & firstSelect[0] != -1)
            {
                matrixBoard[firstSelect[0]][firstSelect[1]].deselect();
                firstSelect[0] = -1;
                firstSelect[1] = -1;
            }
            if (mousePos[0] != -1 & firstSelect[0] != -1)
            {
                secondSelect[0] = mousePos[0];
                secondSelect[1] = mousePos[1];
                matrixBoard[secondSelect[0]][secondSelect[1]].select();

                if (contiguousTiles(firstSelect[0], firstSelect[1], secondSelect[0], secondSelect[1]))
                {
                    switchTokens(firstSelect[0], firstSelect[1], secondSelect[0], secondSelect[1]);
                    current_state = STATE_CHANGING;
                    matrixBoard[firstSelect[0]][firstSelect[1]].deselect();
                    matrixBoard[secondSelect[0]][secondSelect[1]].deselect();
                }
                else
                {
                    matrixBoard[firstSelect[0]][firstSelect[1]].deselect();
                    matrixBoard[secondSelect[0]][secondSelect[1]].deselect();
                    firstSelect[0] = -1;
                    firstSelect[1] = -1;
                    secondSelect[0] = -1;
                    secondSelect[1] = -1;
                }
            }
            }
        }
        if (CMouse.release(0) & firstSelect[0] != -1)
        {
            matrixBoard[firstSelect[0]][firstSelect[1]].deselect();
        }
    }

    public void switchingBehavior()
    {
        //Cuando los tokens se dejan de mover
        if (boardState() == 0)
        {
            bool matched = checkMatches(true);
            //Si no hubo matches se revierte el match
            if (!matched)
            {
                switchTokens(secondSelect[0], secondSelect[1], firstSelect[0], firstSelect[1]);
                current_state = STATE_NORMAL;
                firstSelect[0] = -1;
                firstSelect[1] = -1;
                secondSelect[0] = -1;
                secondSelect[1] = -1;
            }
            else
            {
                movementsLeft--;
                current_state = STATE_ARRANGING;
                firstSelect[0] = -1;
                firstSelect[1] = -1;
                secondSelect[0] = -1;
                secondSelect[1] = -1;

            }

        }
    }



    public void render()
    {
        for (int i = 0; i < matrixBoard.Count(); i++)
        {
            for (int j = 0; j < matrixBoard[0].Count(); j++)
            {
                matrixBoard[i][j].render();
            }
        }
    }
    public bool checkMatchCreate(int aType, int i, int j)
    {
        bool horizontalMatch = false;

        if (j > 0)
        {
            horizontalMatch = (aType == matrixBoard[i][j - 1].food.Type);
        }
        if (j > 1 & horizontalMatch)
        {
            horizontalMatch = (aType == matrixBoard[i][j - 2].food.Type);
        }
        else
        {
            horizontalMatch = false;
        }

        bool verticalMatch = false;
        if (i > 0)
        {
            verticalMatch = (aType == matrixBoard[i - 1][j].food.Type);
        }
        if (i > 1 & verticalMatch)
        {
            verticalMatch = (aType == matrixBoard[i - 2][j].food.Type);
        }
        else
        {
            verticalMatch = false;
        }
        return (horizontalMatch | verticalMatch);
    }
    // booleano para saber si se marca o solo se chequea los matches
    public bool checkMatches(bool mark)
    {
        bool ret = false;
        for (int i = 0; i < matrixBoard.Count(); i++)
        {
            for (int j = 0; j < matrixBoard[0].Count(); j++)
            {
                if (j > 0 & j < matrixBoard[0].Count - 1)
                {
                    if (matrixBoard[i][j].food.Type == matrixBoard[i][j - 1].food.Type && matrixBoard[i][j].food.Type == matrixBoard[i][j + 1].food.Type)
                    {
                        ret = true;
                        if (mark)
                        {
                            matrixBoard[i][j].food.markMatch();
                            matrixBoard[i][j - 1].food.markMatch();
                            matrixBoard[i][j + 1].food.markMatch();
                        }
                    }
                }
                if (i > 0 & i < matrixBoard.Count - 1)
                {
                    if (matrixBoard[i][j].food.Type == matrixBoard[i - 1][j].food.Type && matrixBoard[i][j].food.Type == matrixBoard[i + 1][j].food.Type)
                    {
                        ret = true;
                        if (mark)
                        {
                            matrixBoard[i][j].food.markMatch();
                            matrixBoard[i - 1][j].food.markMatch();
                            matrixBoard[i + 1][j].food.markMatch();
                        }
                    }
                }
            }
        }
        return ret;
    }
    public void deleteMatches()
    {
        for (int i = matrixBoard.Count() - 1; i >= 0; i--)
        {
            for (int j = 0; j < matrixBoard[0].Count(); j++)
            {
                if (matrixBoard[i][j].food.matched == true)
                {
                    matrixBoard[i][j].clearFood();
                    for (int y = i - 1; y >= 0; y--)
                    {
                        if (matrixBoard[y][j].food.Type != -1)
                        {
                            matrixBoard[y][j].food.cascadeAmount++;
                        }
                    }
                }

            }
        }


    }
    public void cascadeBoard1()
    {
        for (int i = matrixBoard.Count() - 2; i >= 0; i--)
        {
            for (int j = matrixBoard[i].Count() - 1; j >= 0; j--)
            {
                int auxX = j;
                int auxY = i;
                while (matrixBoard[auxY + 1][auxX].food.Type == -1)
                {
                    switchTokens(auxY + 1, auxX, auxY, auxX);

                    matrixBoard[auxY][auxX].clearFood();
                    auxY++;
                    if (auxY >= matrixBoard.Count() - 1)
                    {
                        break;
                    }
                }


            }
        }
    }
    public void cascadeBoard()
    {
        for (int i = matrixBoard.Count() - 2; i >= 0; i--)
        {
            for (int j = 0; j < matrixBoard[i].Count(); j++)
            {
                cascadeTile(i, j);
            }
        }
    }

    public void cascadeTile(int y, int x)
    {
        int cascadeAmount = matrixBoard[y][x].food.cascadeAmount;

        if (cascadeAmount > 0 && matrixBoard[y][x].food.Type != -1)
        {
            matrixBoard[y + cascadeAmount][x].food.Type = matrixBoard[y][x].food.Type;
            matrixBoard[y + cascadeAmount][x].setIcon("Sprites/Placeholders_Prototype/food" + matrixBoard[y][x].food.Type);

            matrixBoard[y + cascadeAmount][x].food.imgIcon.setX(matrixBoard[y + cascadeAmount][x].background.getX());
            matrixBoard[y + cascadeAmount][x].food.imgIcon.setY(matrixBoard[y + cascadeAmount][x].background.getY());

            matrixBoard[y][x].clearFood();
            matrixBoard[y + cascadeAmount][x].food.cascadeAmount = 0;
        }

    }
    public void fillSpaces()
    {
        System.Random rng = new System.Random();
        int[] empties = new int[boardWidth];

        for (int y = 0; y < matrixBoard.Count(); y++)
        {
            for (int x = 0; x < matrixBoard.Count(); x++)
            {
                if (matrixBoard[y][x].food.Type == -1)
                {
                    empties[x]++;
                }
            }
        }
        for (int i = 0; i < boardWidth; i++)
        {
            for (int j = 0; j < empties[i]; j++)
            {
                bool matched = true;
                int iconAux = 0;
                while (matched)
                {
                    iconAux = rng.Next(0, 4);
                    if (j>1){
                        if (matrixBoard[j-2][i].food.Type != iconAux)
                        {
                            matched = false;
                        }
                        else
                        {
                            matched = true;
                        }                       
                    }
                    else { matched = false; }
                    if (!matched) { 
                    if (i > 1 )
                    {
                        if (empties[i - 2] >= j)
                        {
                            if (matrixBoard[j][i-2].food.Type != iconAux)
                            {
                                matched = false;
                            }
                            else
                            {
                                matched = true;
                            }
                        }
                    }
                    else { matched = false; }
                    
                }
                }

                matrixBoard[j][i].setIcon("Sprites/Placeholders_Prototype/food" + iconAux.ToString());
                matrixBoard[j][i].food.Type = iconAux;
                matrixBoard[j][i].food.imgIcon.setX(matrixBoard[j][i].background.getX());
                float y = matrixBoard[j][i].background.getY() - matrixBoard[j][i].background.getHeight() * empties[i];

                matrixBoard[j][i].food.imgIcon.setY(y);

                matrixBoard[j][i].food.finalX = matrixBoard[j][i].background.getX();
                matrixBoard[j][i].food.finalY = matrixBoard[j][i].background.getY();
                matrixBoard[j][i].food.imgIcon.setVisible(false);
            }



        }
    }

    public void switchTokens(int y1, int x1, int y2, int x2)
    {
        token auxiliar = matrixBoard[y1][x1].food;
        matrixBoard[y1][x1].food = matrixBoard[y2][x2].food;
        //matrixBoard[y1][x1].food.imgIcon.setX(matrixBoard[y1][x1].background.getX());
        //matrixBoard[y1][x1].food.imgIcon.setY(matrixBoard[y1][x1].background.getY());
        matrixBoard[y1][x1].food.finalX = matrixBoard[y1][x1].background.getX();
        matrixBoard[y1][x1].food.finalY = matrixBoard[y1][x1].background.getY();

        matrixBoard[y2][x2].food = auxiliar;
        matrixBoard[y2][x2].food.finalX = matrixBoard[y2][x2].background.getX();
        matrixBoard[y2][x2].food.finalY = matrixBoard[y2][x2].background.getY();
        //matrixBoard[y2][x2].food.imgIcon.setX(matrixBoard[y2][x2].background.getX());
        //matrixBoard[y2][x2].food.imgIcon.setY(matrixBoard[y2][x2].background.getY());

    }

    public bool contiguousTiles(int y1, int x1, int y2, int x2)
    {
        bool ret = false;
        if (y1 == -1 | y2 == -1)
        {
            return false;
        }
        if (y1 == y2)
        {
            if (x1 - x2 == 1 | x2 - x1 == 1)
            {
                ret = true;
            }

        }
        else if (x1 == x2)
        {
            if (y1 - y2 == 1 | y2 - y1 == 1)
            {
                ret = true;
            }
        }
        return ret;

    }

    public int[] getMouseTile()
    {
        Vector3 mousePos = CMouse.getPos();
        int[] ret = { -1, -1 };

        float minX = matrixBoard[0][0].background.getX() - matrixBoard[0][0].background.getWidth() / 2;
        float maxX = matrixBoard[0][boardWidth - 1].background.getX() + matrixBoard[0][boardWidth - 1].background.getWidth() / 2;
        float minY = matrixBoard[0][0].background.getY() - matrixBoard[0][0].background.getHeight() / 2;
        float maxY = matrixBoard[boardHeight - 1][0].background.getY() + matrixBoard[boardHeight - 1][0].background.getHeight() / 2;


        if (mousePos.x >= minX & mousePos.x <= maxX & mousePos.y >= minY & mousePos.y <= maxY)
        {
            ret[1] = (int)((mousePos.x - minX) / matrixBoard[0][0].background.getWidth());
            ret[0] = (int)((mousePos.y - minY) / matrixBoard[0][0].background.getHeight());

        }
        return ret;


    }   

    public void countScore()
    {
        float totalScore = 0;
        //recorrida por fila
        for (int i = 0; i < matrixBoard.Count(); i++)
        {
            for (int j = 0; j < matrixBoard[i].Count(); j++)
            {
                int count = 1;

                if (matrixBoard[i][j].food.matched & j+1 <matrixBoard[i].Count())
                {
                    while (matrixBoard[i][j+1].food.Type == matrixBoard[i][j].food.Type)
                    {
                        count++;
                        j++;
                        if (j >= matrixBoard[i].Count()-1) { break;}
                    }
                    if (count >= 3)
                    {
                        float auxScore = 3;
                        float multiplier = 1;
                        if (matrixBoard[i][j].food.Type == 3) { auxScore = 1; }
                        if (matrixBoard[i][j].food.Type == CurrentStageData.currentKaiju.prefferedFood) {
                            auxScore = 5;
                            CurrentStageData.currentKaiju.setState(1);
                        }
                        switch (count)
                        {
                            case 3:
                                multiplier = 1;
                                break;
                            case 4:
                                multiplier = 1.5f;
                                break;
                            case 5:
                                multiplier = 2.0f;
                                break;
                            case 6:
                                multiplier = 2.5f;
                                break;
                        }
                        auxScore = auxScore * multiplier;
                        totalScore += auxScore;
                        
                    }
                    count = 1;

                }
            }
        }
        //recorrida por columna
        for (int j = 0; j < matrixBoard.Count(); j++)
        {
            for (int i = 0; i < matrixBoard.Count(); i++)
            {
                int count = 1;

                if (matrixBoard[i][j].food.matched & i + 1 < matrixBoard.Count())
                {
                    while (matrixBoard[i+1][j].food.Type == matrixBoard[i][j].food.Type)
                    {
                        count++;
                        i++;
                        if (i >= matrixBoard.Count() - 1) { break; }
                    }
                    if (count >= 3)
                    {
                        float auxScore = 3;
                        float multiplier = 1;
                        if (matrixBoard[i][j].food.Type == 3) { auxScore = 1; }
                        if (matrixBoard[i][j].food.Type == CurrentStageData.currentKaiju.prefferedFood) { auxScore = 5;
                            CurrentStageData.currentKaiju.setState(1);
                        }
                        switch (count)
                        {
                            case 3:
                                multiplier = 1;
                                break;
                            case 4:
                                multiplier = 1.5f;
                                break;
                            case 5:
                                multiplier = 2.0f;
                                break;
                            case 6:
                                multiplier = 2.5f;
                                break;
                        }
                        auxScore = auxScore * multiplier;
                        totalScore += auxScore;
                        
                    }
                    count = 1;

                }
            }
        }
        CurrentStageData.addScore(totalScore);
    }

    public int boardState()
    {
        int ret = 0;
        for (int i = 0; i < matrixBoard.Count(); i++)
        {
            for (int j = 0; j < matrixBoard[i].Count; j++)
            {
                if (matrixBoard[i][j].food.current_state > ret)
                {
                    ret = matrixBoard[i][j].food.current_state;
                }
            }
        }
        return ret;
    }

    public bool possibleMatches()
    {
        bool possible = false;
        for (int i = 0; i < matrixBoard.Count(); i++)
        {
            for (int j = 0; j < matrixBoard[i].Count(); j++)
            {
                if (j < matrixBoard[i].Count() - 1)
                {
                    switchTokens(i, j, i, j + 1);
                    possible = checkMatches(false);
                    switchTokens(i, j, i, j + 1);
                    if (possible) { return possible; }
                }

                if (i < matrixBoard.Count() - 1 & !possible)
                {
                    switchTokens(i, j, i + 1, j);
                    possible = checkMatches(false);
                    switchTokens(i, j, i + 1, j);
                    if (possible) { return possible; }
                }
            }
        }



        return possible;
    }
    public int getMovementsLeft() {
        return movementsLeft;
    }
    public float getUpBorder()
    {
        return matrixBoard[0][0].background.getY() - matrixBoard[0][0].background.getHeight() / 2;
    }

    public void destroy()
    {
        for (int i = 0; i < matrixBoard.Count(); i++)
        {
            for (int j = 0; j < matrixBoard[i].Count(); j++)
            {
                matrixBoard[i][j].destroy();
            }
        }
        matrixBoard.Clear();
    
   
     }

}




