using System.Numerics;
using System.Security.Cryptography;
using Raylib_CsLo;

namespace ui;

class GridInterface
{

    private static int widthNum = 9;
    private static int heightNum = 9;
    private static readonly Color onColor = Raylib.DARKGREEN;
    private static readonly Color offColor = Raylib.BLACK;
    public FireSpreadAutomata fireAutomata = new(widthNum, heightNum);
    private List<LamportInterface> grid = new();
    private readonly List<Color> colors = new()
    {
        new() {r = 34,g=177,b=76,a=255},
        new() {r=255,g=242,b=0,a=255},
        new() {r=209,g=198,b=0,a=255},
        new() {r=255,g= 127,b=39,a= 255},
        new() {r=255,g= 85,b=19,a=255},
        new() {r=237,g= 28,b=36,a=255},
        new() {r=136,g= 0,b=21,a=255},
        new() {r=51,g=51,b=51,a=255}};
    public void Draw()
    {
        ;
        int posX = 1;
        int posY = 1;
        for (int i = 0; i < widthNum * heightNum; i++)
        {
            if (i % heightNum == 0)
            {
                posY++;
                posX = 1;

            }
            // Console.WriteLine(i);
            // Console.WriteLine(posX);
            // Console.WriteLine(posY);
            // Console.WriteLine(grid[posX][posY - 1]);
            // Console.WriteLine(grid[posX][posY]);

            // Console.WriteLine((posX * 13 + posY * 27) % 4);
            // Raylib.DrawRectangle(40 + 10 * posX, 130 + 10 * posY, 10, 10, colors[(posX * 13 + posY * 27) % 4]);
            Raylib.DrawRectangle(40 + 60 * posX, 130 + 60 * posY, 58, 58, colors[fireAutomata.gridFire[posX][posY - 1]]);
            posX++;
        }
        // for (int i = 0; i < widthNum; i++)
        // {
        //     for (int ii = 0; ii < heightNum; i++)
        //     {
        //         // Raylib.DrawRectangleLinesEx(rect, fontSize / 4, onColor);
        //     }
        // }


        // var offset = new Vector2(Raylib.MeasureText(currentFileName ?? "EMPTY", fontSize) / 2, fontSize / 2);
        // Raylib.DrawText(currentFileName ?? "EMPTY", rect.X + rect.width / 2 - offset.X, rect.Y + rect.height / 2 - offset.Y, fontSize, currentFileName == null ? offColor : onColor);
    }

}


class LamportInterface
{

    class FileDrop
    {
        Rectangle rect;
        string? currentFilePath = null;
        string? currentFileName = null;

        private static readonly Color onColor = Raylib.DARKGREEN;
        private static readonly Color offColor = Raylib.BLACK;

        public bool HasFile { get => currentFilePath != null; }
        public string CurrentFilePath { get => currentFilePath ?? ""; }
        public string CurrentFileName { get => currentFileName ?? ""; }

        public FileDrop(Rectangle r)
        {
            rect = r;
        }

        public void CheckForFiles()
        {
            if (Raylib.IsFileDropped())
            {
                var mousePos = Raylib.GetMousePosition();
                if (Raylib.CheckCollisionPointRec(mousePos, rect))
                {
                    var droppedFiles = Raylib.GetDroppedFilesAndClear();
                    if (droppedFiles.Length == 0)
                        return;

                    currentFilePath = droppedFiles[0];
                    currentFileName = Path.GetFileName(currentFilePath);
                }
            }
        }

        public void Draw(int fontSize)
        {
            Raylib.DrawRectangleLinesEx(rect, fontSize / 4, currentFileName == null ? offColor : onColor);
            var offset = new Vector2(Raylib.MeasureText(currentFileName ?? "EMPTY", fontSize) / 2, fontSize / 2);
            Raylib.DrawText(currentFileName ?? "EMPTY", rect.X + rect.width / 2 - offset.X, rect.Y + rect.height / 2 - offset.Y, fontSize, currentFileName == null ? offColor : onColor);
        }
        public void Clear()
        {
            currentFilePath = null;
            currentFileName = null;
        }
    }

    enum UIState
    {
        SIGN,
        VERIFY
    };

    static GridInterface map = new();
    // Both States
    static Rectangle signButtonRec = new Rectangle(
        1200, 320, 80, 30
    );

    static Rectangle changeModeRec = new Rectangle(
        550, 20, 20, 20
    );

    // Sign State
    static FileDrop fileToSign = new FileDrop(
        new Rectangle(
            200, 150, 200, 80
        )
    );

    // Verify State


    static FileDrop signatureDrop = new FileDrop(
        new Rectangle(
            300, 210, 200, 80
        )
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
                    // messageDrop.CheckForFiles();
                    // pubkeyDrop.CheckForFiles();
                    // Raylib.DrawText("Message:", 1200, 68, 20, Raylib.BLACK);
                    // messageDrop.Draw(20);
                    Raylib.DrawText("Interation (t): ", 800, 188, 30, Raylib.BLACK);
                    Raylib.DrawText(TPeriod.ToString(), 1050, 180, 50, Raylib.BLACK);
                    Raylib.DrawText("Burn Percentage (%): ", 800, 388, 30, Raylib.BLACK);
                    Console.WriteLine(map.fireAutomata.burnPercentage());
                    Raylib.DrawText(map.fireAutomata.burnPercentage().ToString(), 1150, 380, 50, Raylib.BLACK);
                    // pubkeyDrop.Draw(20);
                    // Raylib.DrawText("Signature:", 1100, 188, 20, Raylib.BLACK);
                    // Thread.Sleep(6000);
                    // Console.ReadLine();

                    if (RayGui.GuiButton(signButtonRec, "Verify!"))
                    {
                        if (signatureDrop.HasFile)
                        {
                            bool validated = true;
                            // verifier.ValidateSignatureFromFiles(
                            //     messageDrop.CurrentFilePath,
                            //     pubkeyDrop.CurrentFilePath,
                            //     signatureDrop.CurrentFilePath
                            // );

                            if (validated)
                            {
                                Console.WriteLine("INFO: Signature verified! The message came from the sender!");
                            }
                            else
                            {
                                Console.WriteLine("INFO: Invalid signature for the given message!");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"WARN: Missing files!");
                        }
                    }

                    if (RayGui.GuiButton(changeModeRec, "#61#"))
                    {
                        // messageDrop.Clear();
                        // pubkeyDrop.Clear();
                        state = UIState.SIGN;
                    }
                }
                break;
            default: // SIGN
                {
                    Raylib.DrawText("Celullar Automata", 220, 20, 20, Raylib.BLACK);
                    fileToSign.CheckForFiles();
                    // fileToSign.Draw(20);

                    if (RayGui.GuiButton(signButtonRec, "Start"))
                    {
                        state = UIState.VERIFY;
                        if (fileToSign.HasFile)
                        {
                            // signer.Init();
                            // var sign = signer.SignFile(fileToSign.CurrentFilePath);
                            // signer.DumpSig(sign!, $"{fileToSign.CurrentFileName}.sig");
                            // signer.DumpPublicKey($"{fileToSign.CurrentFileName}.pub");
                            Console.WriteLine($"INFO: File signed.");
                            Console.WriteLine($"INFO: Files {fileToSign.CurrentFileName}.sig and {fileToSign.CurrentFileName}.pub created.");
                        }
                        else
                        {
                            Console.WriteLine("WARN: No file given!");
                        }
                    }

                    if (RayGui.GuiButton(changeModeRec, "#61#"))
                    {
                        fileToSign.Clear();
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
        HashAlgorithm hashFunc = SHA256.Create();
        // signer = new LamportSigner(hashFunc);
        // verifier = new LamportVerifier(hashFunc);

        Console.WriteLine("\n");

        // Verify State fileDroppers
        // FileDrop message = new FileDrop()

        while (!Raylib.WindowShouldClose())
        {
            // if (Raylib.IsFileDropped())
            // {
            //     var droppedFiles = Raylib.GetDroppedFilesAndClear();
            //     droppedFiles.ToList().ForEach((string s) => {Console.WriteLine($"File dropped: {s}");});
            //     var mousePos = Raylib.GetMousePosition();
            //     Console.WriteLine($"Mouse: ({mousePos.X},{mousePos.Y})");
            // }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.WHITE);

            UpdateAndDraw(ref currentState, period);
            Thread.Sleep(1000);
            // int fontSize = 20;
            // Vector2 offset = new Vector2(Raylib.MeasureText("Badabingus.", fontSize) / 2, fontSize/2);
            // Raylib.DrawText("Badabingus.", 400 - offset.X, 200 - offset.Y, fontSize, Raylib.BLACK);
            Raylib.EndDrawing();
            period++;
        }

        Raylib.CloseWindow();
    }


}