using System;

public class BoardPrinter
{
    public int BoardSize = 8; // BoardSize X BoardSize
    
    public void PrintBoard(int[] queensPose)
	{
        char[][] board = new char[BoardSize][];
        for (int i=0; i < BoardSize; i++)
        {
            board[i] = new char[BoardSize];
            Array.Fill(board[i], '*');
            board[i][queensPose[i]] = 'Q';
            Console.Write(new string(board[i]) + '\n');
        }
	}
}
