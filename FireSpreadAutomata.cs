
using System.Net;

enum WindDirection
{
    West_East,
    East_West,
    North_South,
    South_North
}
class FireSpreadAutomata
{
    private readonly int reticuladoWidth;
    private readonly int reticuladoHeight;
    private int burnagePercentage = 1;
    private int burnageTotal;
    private WindDirection windDirection;
    private int windSpeed = 20;



    public List<List<Vegetation>> gridFire;

    VegetationMap vegetationMap = new();
    private int BurnTotal() => reticuladoWidth * reticuladoHeight;
    public bool HasToStop() => burnagePercentage == burnageTotal;
    public float BurnPercentage() => ((float)burnagePercentage / (float)burnageTotal) * 100;
    public FireSpreadAutomata(int width, int height, WindDirection wind)
    {
        reticuladoHeight = height;
        reticuladoWidth = width;
        windDirection = wind;
        gridFire = vegetationMap.MakeGrid();
        burnageTotal = BurnTotal();
    }

    public void UpdateGrid()
    {
        List<List<int>> changes = new();
        for (int y = 1; y < reticuladoHeight + 1; y++)
        {
            for (int x = 1; x < reticuladoWidth + 1; x++)
            {
                //Grid hardcoded 
                int leftTop = 0;
                int leftCenter = 0;
                int leftBottom = 0;
                int rightCenter = 0;
                int rightTop = 0;
                int rightBottom = 0;
                int centerTop = 0;
                int centerBottom = 0;
                int sumToBurn = 0;


                if ((x > 0) && (x <= reticuladoWidth) && (y > 0) && (y <= reticuladoWidth))
                {
                    //Checkers
                    leftTop = gridFire[x - 1][y - 1].IsBurning() ? 1 : 0;
                    leftCenter = gridFire[x - 1][y].IsBurning() ? 1 : 0;
                    leftBottom = gridFire[x - 1][y + 1].IsBurning() ? 1 : 0;
                    centerTop = gridFire[x][y - 1].IsBurning() ? 1 : 0;
                    centerBottom = gridFire[x][y + 1].IsBurning() ? 1 : 0;
                    rightCenter = gridFire[x + 1][y].IsBurning() ? 1 : 0;
                    rightTop = gridFire[x + 1][y - 1].IsBurning() ? 1 : 0;
                    rightBottom = gridFire[x + 1][y + 1].IsBurning() ? 1 : 0;

                    sumToBurn = leftBottom + leftCenter + leftTop + rightBottom + rightCenter + rightTop + centerBottom + centerTop;

                }


                //Vegetation on Fire
                if (gridFire[x][y].IsBurning())
                {

                    gridFire[x][y].UpdateBurn();
                    Random RNG = new();

                    //There are other cells burning in the neighbourhood
                    if (sumToBurn > 0)
                    {
                        //leftTop
                        int probability = RNG.Next(0, 10000);
                        if (probability <= (gridFire[x - 1][y - 1].GetProbabilityToBurn() * GetProbabilityOfWind(x, y, x - 1, y - 1, windDirection, windSpeed)))
                            changes.Add(new List<int>() { x - 1, y - 1 });

                        //leftCenter
                        probability = RNG.Next(0, 10000);
                        int veg = gridFire[x - 1][y].GetProbabilityToBurn();
                        int probs = (gridFire[x - 1][y].GetProbabilityToBurn() * GetProbabilityOfWind(x, y, x - 1, y, windDirection, windSpeed));
                        if (probability <= (gridFire[x - 1][y].GetProbabilityToBurn() * GetProbabilityOfWind(x, y, x - 1, y, windDirection, windSpeed)))
                            changes.Add(new List<int>() { x - 1, y });

                        //leftBottom
                        probability = RNG.Next(0, 10000);
                        if (probability <= (gridFire[x - 1][y + 1].GetProbabilityToBurn() * GetProbabilityOfWind(x, y, x - 1, y + 1, windDirection, windSpeed)))
                            changes.Add(new List<int>() { x - 1, y + 1 });

                        //centerTop
                        probability = RNG.Next(0, 10000);
                        if (probability <= (gridFire[x][y - 1].GetProbabilityToBurn() * GetProbabilityOfWind(x, y, x, y - 1, windDirection, windSpeed)))
                            changes.Add(new List<int>() { x, y - 1 });

                        //centerBottom
                        probability = RNG.Next(0, 10000);
                        if (probability <= (gridFire[x][y + 1].GetProbabilityToBurn() * GetProbabilityOfWind(x, y, x, y + 1, windDirection, windSpeed)))
                            changes.Add(new List<int>() { x, y + 1 });

                        //rightCenter
                        probability = RNG.Next(0, 10000);
                        veg = gridFire[x + 1][y].GetProbabilityToBurn();
                        probs = (gridFire[x + 1][y].GetProbabilityToBurn() * GetProbabilityOfWind(x, y, x + 1, y, windDirection, windSpeed));
                        if (probability <= (gridFire[x + 1][y].GetProbabilityToBurn() * GetProbabilityOfWind(x, y, x + 1, y, windDirection, windSpeed)))
                            changes.Add(new List<int>() { x + 1, y });

                        //rightTop
                        probability = RNG.Next(0, 10000);
                        if (probability <= (gridFire[x + 1][y - 1].GetProbabilityToBurn() * GetProbabilityOfWind(x, y, x + 1, y - 1, windDirection, windSpeed)))
                            changes.Add(new List<int>() { x + 1, y - 1 });

                        //rightBottom
                        probability = RNG.Next(0, 10000);
                        if (probability <= (gridFire[x + 1][y + 1].GetProbabilityToBurn() * GetProbabilityOfWind(x, y, x + 1, y + 1, windDirection, windSpeed)))
                            changes.Add(new List<int>() { x + 1, y + 1 });
                        continue;
                    }
                    //Just the current sell is Burning
                    else
                    {
                        //leftTop
                        int probability = RNG.Next(0, 100);
                        if (probability <= gridFire[x - 1][y - 1].GetProbabilityToBurn())
                            changes.Add(new List<int>() { x - 1, y - 1 });

                        //leftCenter
                        probability = RNG.Next(0, 100);
                        if (probability <= gridFire[x - 1][y].GetProbabilityToBurn())
                            changes.Add(new List<int>() { x - 1, y });

                        //leftBottom
                        probability = RNG.Next(0, 100);
                        if (probability <= gridFire[x - 1][y + 1].GetProbabilityToBurn())
                            changes.Add(new List<int>() { x - 1, y + 1 });

                        //centerTop
                        probability = RNG.Next(0, 100);
                        if (probability <= gridFire[x][y - 1].GetProbabilityToBurn())
                            changes.Add(new List<int>() { x, y - 1 });

                        //centerBottom
                        probability = RNG.Next(0, 100);
                        if (probability <= gridFire[x][y + 1].GetProbabilityToBurn())
                            changes.Add(new List<int>() { x, y + 1 });

                        //rightCenter
                        probability = RNG.Next(0, 100);
                        if (probability <= gridFire[x + 1][y].GetProbabilityToBurn())
                            changes.Add(new List<int>() { x + 1, y });

                        //rightTop
                        probability = RNG.Next(0, 100);
                        if (probability <= gridFire[x + 1][y - 1].GetProbabilityToBurn())
                            changes.Add(new List<int>() { x + 1, y - 1 });

                        //rightBottom
                        probability = RNG.Next(0, 100);
                        if (probability <= gridFire[x + 1][y + 1].GetProbabilityToBurn())
                            changes.Add(new List<int>() { x + 1, y + 1 });
                        continue;
                    }
                }
                else
                {
                    if (sumToBurn > 0)
                    {
                        Random RNG = new();
                        int probability = RNG.Next(0, 100);
                        if (probability <= gridFire[x][y].GetProbabilityToBurn())
                            changes.Add(new List<int>() { x, y });
                        continue;
                    }
                }




            }

        }
        changes.ForEach(delegate (List<int> a)
        {
            if (!gridFire[a[0]][a[1]].IsBurning())
            {
                burnagePercentage++;
                gridFire[a[0]][a[1]].SetFire();

            }
        });

    }


    private static int GetProbabilityOfWind(int currX, int currY, int posX, int posY, WindDirection windDir, int windSpeed)
    {
        int distance;
        if (windSpeed == 0)
            return 100;

        switch (windDir)
        {
            case WindDirection.East_West:
                distance = currX - posX;
                distance += 1;

                return 100 - (2 * windSpeed * (2 - Math.Abs(distance)));
            case WindDirection.West_East:
                distance = currX - posX;
                distance -= 1;

                return 100 - (windSpeed * (2 - Math.Abs(distance)));
            case WindDirection.South_North:
                distance = currY - posY;
                distance -= 1;

                return 100 - (2 * windSpeed * (2 - Math.Abs(distance)));
            case WindDirection.North_South:
                distance = currY - posY;
                distance -= 1;

                return 100 - (windSpeed * (2 - Math.Abs(distance)));

        }
        return 100;
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
