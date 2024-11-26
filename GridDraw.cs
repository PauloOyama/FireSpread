using System.ComponentModel;

class FireSpreadAutomata
{
    private readonly int reticuladoWidth;
    private readonly int reticuladoHeight;

    public List<List<int>> gridFire;
    public FireSpreadAutomata(int width, int height)
    {
        reticuladoHeight = height;
        reticuladoWidth = width;
        gridFire = MakeGrid();
    }

    public static List<List<int>> MakeGrid()
    {
        return new List<List<int>> {
            new() {0,0,0,0,0},
            new() {0,0,0,0,0},
            new() {0,0,1,0,0},
            new() {0,0,0,0,0},
            new() {0,0,0,0,0}
        };
    }


    public void UpdateGrid()
    {
        for (int x = 0; x < reticuladoHeight; x++)
        {
            for (int y = 0; y < reticuladoHeight; y++)
            {
                //It's on fire
                if (gridFire[x][y] != 0)
                {
                    gridFire[x][y]++;
                }
                else
                {
                    int leftTop = 0;
                    int leftCenter = 0;
                    int leftBottom = 0;
                    int rightCenter = 0;
                    int rightTop = 0;
                    int rightBottom = 0;
                    int centerTop = 0;
                    int centerBottom = 0;
                    int sumToBurn = 0;


                    if (x == 0)
                    {
                        leftTop = gridFire[x - 1][y - 1] != 0 ? 1 : 0;
                        leftCenter = gridFire[x - 1][y] != 0 ? 1 : 0;
                        leftBottom = gridFire[x - 1][y + 1] != 0 ? 1 : 0;
                        centerTop = gridFire[x][y + 1] != 0 ? 1 : 0;
                        centerBottom = gridFire[x][y - 1] != 0 ? 1 : 0;
                    }

                    if (y == 0)
                    {
                        leftTop = 0;
                        centerTop = 0;
                        rightTop = 0;

                    }

                    if (x == (reticuladoWidth - 1))
                    {
                        rightCenter = gridFire[x + 1][y - 1] != 0 ? 1 : 0;
                        rightTop = gridFire[x + 1][y] != 0 ? 1 : 0;
                        rightBottom = gridFire[x + 1][y + 1] != 0 ? 1 : 0;
                        centerTop = gridFire[x][y + 1] != 0 ? 1 : 0;
                        centerBottom = gridFire[x][y - 1] != 0 ? 1 : 0;
                    }

                    // if (y == (reticuladoHeight - 1))
                    // {
                    //     leftBottom = 0;
                    //     centerBottom = 0;
                    //     rightBottom = 0;
                    // }
                    if ((x > 0) && (x < (reticuladoWidth - 1)) && (y > 0) && (y < (reticuladoWidth - 1)))
                    {
                        //Checkers
                        leftTop = gridFire[x - 1][y - 1] != 0 ? 1 : 0;
                        leftCenter = gridFire[x - 1][y] != 0 ? 1 : 0;
                        leftBottom = gridFire[x - 1][y + 1] != 0 ? 1 : 0;
                        centerTop = gridFire[x][y + 1] != 0 ? 1 : 0;
                        centerBottom = gridFire[x][y - 1] != 0 ? 1 : 0;
                        rightCenter = gridFire[x + 1][y - 1] != 0 ? 1 : 0;
                        rightTop = gridFire[x + 1][y] != 0 ? 1 : 0;
                        rightBottom = gridFire[x + 1][y + 1] != 0 ? 1 : 0;

                        sumToBurn = leftBottom + leftCenter + leftTop + rightBottom + rightCenter + rightTop + centerBottom + centerTop;

                    }


                    //has Adjacent Burning
                    if (sumToBurn > 0)
                    {
                        Random RNG = new();
                        int probability = RNG.Next(0, 100);
                        if (probability > 70)
                            gridFire[x][y] = 1;
                        Console.WriteLine("Pegou fogo!");
                        continue;
                    }
                }
            }

        }
    }



    static void Main()
    {
        FireSpreadAutomata grid = new(5, 5);

        grid.UpdateGrid();

    }
}
