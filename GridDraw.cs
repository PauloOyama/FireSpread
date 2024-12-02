using Raylib_CsLo;

namespace ui;

class GridInterface
{

    private static int widthNum = 100;
    private static int heightNum = 100;
    private static readonly Color onColor = Raylib.DARKGREEN;
    private static readonly Color offColor = Raylib.BLACK;
    public FireSpreadAutomata fireAutomata = new(widthNum, heightNum, WindDirection.West_East);

    public GridInterface(int size) => widthNum = size;

    public void Draw()
    {

        int posX = 1;
        int posY = 1;
        for (int i = 0; i < widthNum * heightNum; i++)
        {
            if (i % heightNum == 0)
            {
                posY++;
                posX = 1;

            }

            Raylib.DrawRectangle(40 + 7 * posX, 130 + 7 * posY, 7, 7, fireAutomata.gridFire[posX][posY - 1].GetColor());
            posX++;
        }
    }

}


class LamportInterface
{
    enum UIState
    {
        SIGN,
        VERIFY
    };

    static GridInterface map = new(100);
    static Rectangle signButtonRec = new Rectangle(
        1200, 320, 80, 30
    );

    static Rectangle changeModeRec = new Rectangle(
        550, 20, 20, 20
    );


    static void UpdateAndDraw(ref UIState state, int TPeriod)
    {
        switch (state)
        {
            case UIState.VERIFY:
                {
                    map.Draw();
                    map.fireAutomata.UpdateGrid();
                    Raylib.DrawText("Wildfire Simulation", 900, 20, 40, Raylib.BLACK);

                    Raylib.DrawText("Interation (t): ", 800, 188, 30, Raylib.BLACK);
                    Raylib.DrawText(TPeriod.ToString(), 1050, 180, 50, Raylib.BLACK);
                    Raylib.DrawText("Burn Percentage (%): ", 800, 388, 30, Raylib.BLACK);
                    Console.WriteLine(map.fireAutomata.BurnPercentage());
                    Raylib.DrawText(map.fireAutomata.BurnPercentage().ToString(), 1150, 380, 50, Raylib.BLACK);


                    // if (RayGui.GuiButton(signButtonRec, "Verify!"))
                    // {

                    //     Console.WriteLine($"WARN: Missing files!");

                    // }

                    // if (RayGui.GuiButton(changeModeRec, "#61#"))
                    // {
                    //     state = UIState.SIGN;
                    // }
                }
                break;
            default:
                {
                    Raylib.DrawText("Celullar Automata", 220, 20, 20, Raylib.BLACK);



                    if (RayGui.GuiButton(signButtonRec, "Start"))
                    {
                        state = UIState.VERIFY;

                        Console.WriteLine($"INFO: File signed.");

                    }

                    if (RayGui.GuiButton(changeModeRec, "#61#"))
                    {

                        state = UIState.VERIFY;
                    }
                }
                break;
        }
    }

    static void Main()
    {
        int period = 0;
        Raylib.InitWindow(1700, 900, "Cellular Automata");
        Raylib.SetTargetFPS(60);

        UIState currentState = UIState.VERIFY;

        Console.WriteLine("\n");


        while (!Raylib.WindowShouldClose())
        {

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.WHITE);

            UpdateAndDraw(ref currentState, period);
            //Speed to burn
            Thread.Sleep(300);
            Raylib.EndDrawing();
            period++;
            // if (period == 21) Console.ReadLine();
            // if (period == 41) Console.ReadLine();
            // if (period == 61) Console.ReadLine();
            // if (period == 81) Console.ReadLine();
        }

        Raylib.CloseWindow();
    }


}