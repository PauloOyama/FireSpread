using System.Data.Common;
using System.Dynamic;

class FireSpreadAutomata
{
    private readonly int reticuladoWidth;
    private readonly int reticuladoHeight;
    private int burnagePercentage = 1;
    private int burnageTotal;


    public List<List<int>> gridFire;


    private int burnTotal() => reticuladoWidth * reticuladoHeight;
    public bool hasToStop() => burnagePercentage == burnageTotal;
    public float burnPercentage() => ((float)burnagePercentage / (float)burnageTotal) * 100;
    public FireSpreadAutomata(int width, int height)
    {
        reticuladoHeight = height;
        reticuladoWidth = width;
        gridFire = MakeGrid(reticuladoWidth);
        burnageTotal = burnTotal();
    }

    public static List<List<int>> MakeGrid(int width)
    {
        List<List<int>> gridAux = new List<List<int>> {
            new() {0,0,0,0,0,0,0,0,0},
            new() {0,0,0,0,0,0,0,0,0},
            new() {0,0,0,0,0,0,0,0,0},
            new() {0,0,0,0,0,0,0,0,0},
            new() {0,0,0,0,1,0,0,0,0},
            new() {0,0,0,0,0,0,0,0,0},
            new() {0,0,0,0,0,0,0,0,0},
            new() {0,0,0,0,0,0,0,0,0},
            new() {0,0,0,0,0,0,0,0,0},
        };



        List<List<int>> padding = new();
        List<int> aa = new();

        for (int y = 0; y < width + 2; y++) aa.Add(0);

        padding.Add(aa);

        for (int i = 0; i < gridAux.Count; i++)
        {
            List<int> paddingLine = new();
            for (int ii = 0; ii < gridAux[i].Count + 2; ii++)
            {
                if (ii == 0) paddingLine.Add(gridAux[i][ii]);
                else if (ii == (gridAux[i].Count + 1)) paddingLine.Add(gridAux[i][ii - 2]);
                else paddingLine.Add(gridAux[i][ii - 1]);

            }
            padding.Add(paddingLine);
        }
        padding.Add(aa);

        return padding;
    }

    public void UpdateGrid()
    {
        List<List<int>> changes = new List<List<int>>();
        for (int y = 1; y < reticuladoHeight + 1; y++)
        {
            for (int x = 1; x < reticuladoWidth + 1; x++)
            {
                //It's on fire
                if (gridFire[x][y] != 0)
                {
                    if (gridFire[x][y] < 7)
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


                    // if (x == 0)
                    // {
                    //     leftTop = gridFire[x - 1][y - 1] != 0 ? 1 : 0;
                    //     leftCenter = gridFire[x - 1][y] != 0 ? 1 : 0;
                    //     leftBottom = gridFire[x - 1][y + 1] != 0 ? 1 : 0;
                    //     centerTop = gridFire[x][y + 1] != 0 ? 1 : 0;
                    //     centerBottom = gridFire[x][y - 1] != 0 ? 1 : 0;
                    // }

                    // if (y == 0)
                    // {
                    //     leftTop = 0;
                    //     centerTop = 0;
                    //     rightTop = 0;

                    // }

                    // if (x == (reticuladoWidth - 1))
                    // {
                    //     rightCenter = gridFire[x + 1][y - 1] != 0 ? 1 : 0;
                    //     rightTop = gridFire[x + 1][y] != 0 ? 1 : 0;
                    //     rightBottom = gridFire[x + 1][y + 1] != 0 ? 1 : 0;
                    //     centerTop = gridFire[x][y + 1] != 0 ? 1 : 0;
                    //     centerBottom = gridFire[x][y - 1] != 0 ? 1 : 0;
                    // }

                    // if (y == (reticuladoHeight - 1))
                    // {
                    //     leftBottom = 0;
                    //     centerBottom = 0;
                    //     rightBottom = 0;
                    // }
                    if ((x > 0) && (x <= reticuladoWidth) && (y > 0) && (y <= reticuladoWidth))
                    {
                        //Checkers
                        leftTop = gridFire[x - 1][y - 1] != 0 ? 1 : 0;
                        leftCenter = gridFire[x - 1][y] != 0 ? 1 : 0;
                        leftBottom = gridFire[x - 1][y + 1] != 0 ? 1 : 0;
                        centerTop = gridFire[x][y - 1] != 0 ? 1 : 0;
                        centerBottom = gridFire[x][y + 1] != 0 ? 1 : 0;
                        rightCenter = gridFire[x + 1][y] != 0 ? 1 : 0;
                        rightTop = gridFire[x + 1][y - 1] != 0 ? 1 : 0;
                        rightBottom = gridFire[x + 1][y + 1] != 0 ? 1 : 0;

                        sumToBurn = leftBottom + leftCenter + leftTop + rightBottom + rightCenter + rightTop + centerBottom + centerTop;

                    }


                    //has Adjacent Burning
                    if (sumToBurn > 0)
                    {
                        Random RNG = new();
                        int probability = RNG.Next(0, 100);
                        if (probability > 70)
                            changes.Add(new List<int>() { x, y });
                        continue;
                    }



                }
            }

        }
        changes.ForEach(delegate (List<int> a)
{
    burnagePercentage++;
    gridFire[a[0]][a[1]] = 1;
});

    }


    public void PrintGrid()
    {
        for (int y = 1; y < reticuladoHeight; y++)
        {
            for (int x = 1; x < reticuladoWidth; x++)
            {
                Console.Write("{0} ", gridFire[x][y]);
            }
            Console.WriteLine("");
        }
    }



    // static void Main()
    // {
    //     FireSpreadAutomata grid = new(6, 6);

    //     grid.PrintGrid();
    //     grid.UpdateGrid();
    //     grid.PrintGrid();



    // }
}
