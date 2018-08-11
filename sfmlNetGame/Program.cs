using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace sfmlNetGame
{
    class Program
    {
        static RenderWindow window;
        static string inputKeybordRight = "1: ";
        static string currentInputKeybordRight = "";
        static int countLinesRight = 1;

        static string inputKeybordLeft = "1: ";
        static string currentInputKeybordLeft = "";
        static int countLinesLeft = 1;

        static bool rightPlayer = true;

        static Texture faceTexture;
        static Sprite faceSprite;

        enum MoveDirections { left = 0, right = 1, up = 2, down = 3, stop = 4 }
        static MoveDirections playerDirection = MoveDirections.right;

        static Clock clk = new Clock();
        static float deltaTime;

        static Texture enemyTexture;
        static List<Sprite> enemySprites = new List<Sprite>(5);

        static Random rnd = new Random();

        static Texture flameTexture;
        static Sprite flameSprite;
        static bool assFlameShoot = false;
        static float shootDelay = 30f;

        static Texture waveTexture;
        static Sprite waveSprite;
        static bool waveShoot = false;
        static float waveDelay = 30f;

        static int numberWave = 1;
        static int countEnemies = 3;

        static bool playGame = true;

        static int countKills = 0;

        static void CloseWindow(object sender, EventArgs e)
        {
            window.Close();
        }

        static void ExecuteCommand(string command)
        {
            switch (command)
            {
                case "move_right();":
                    if (rightPlayer == true)
                    {
                        playerDirection = MoveDirections.right;
                    }
                    break;
                case "move_left();":
                    if (rightPlayer == true)
                    {
                        playerDirection = MoveDirections.left;
                    }
                    break;
                case "move_up();":
                    if (rightPlayer == true)
                    {
                        playerDirection = MoveDirections.up;
                    }
                    break;
                case "move_down();":
                    if (rightPlayer == true)
                    {
                        playerDirection = MoveDirections.down;
                    }
                    break;
                case "clear();":
                    if (rightPlayer == true)
                    {
                        countLinesRight = 1;
                        inputKeybordRight = "1: ";
                    }
                    else
                    {
                        countLinesLeft = 1;
                        inputKeybordLeft = "1: ";
                    }
                    break;

                case "ass_flame();":
                    if (rightPlayer == false)
                    {
                        if (assFlameShoot == false)
                        {
                            shootDelay = 2f;
                            assFlameShoot = true;
                        }
                    }
                    break;

                case "ass_wave();":
                    if (rightPlayer == false)
                    {
                        if (waveShoot == false)
                        {
                            waveDelay = 0.5f;
                            waveShoot = true;
                        }
                    }
                    break;
            }
        }

        //ловля нажатий клавиши
        static void KeyPressed(object sender, KeyEventArgs e)
        {
            if (rightPlayer == true)
            {
                if ((int)e.Code >= 0 && (int)e.Code <= 25)
                {
                    currentInputKeybordRight += Convert.ToChar(e.Code + 97);
                    inputKeybordRight += Convert.ToChar(e.Code + 97);
                }
                if ((int)e.Code == 26)
                {
                    currentInputKeybordRight += ")";
                    inputKeybordRight += ")";
                }
                if ((int)e.Code == 35)
                {
                    currentInputKeybordRight += "(";
                    inputKeybordRight += "(";
                }
                if ((int)e.Code == 48)
                {
                    currentInputKeybordRight += ";";
                    inputKeybordRight += ";";
                }
                if ((int)e.Code == 56)
                {
                    currentInputKeybordRight += "_";
                    inputKeybordRight += "_";
                }
                if ((int)e.Code == 58)
                {
                    ExecuteCommand(currentInputKeybordRight);
                    if (currentInputKeybordRight != "clear();")
                    {
                        countLinesRight++;
                        inputKeybordRight += "\n" + countLinesRight + ": ";
                    }
                    currentInputKeybordRight = "";
                }
                if ((int)e.Code == 59 && currentInputKeybordRight.Length > 0)
                {
                    currentInputKeybordRight = currentInputKeybordRight.Remove(currentInputKeybordRight.Length - 1);

                    inputKeybordRight = inputKeybordRight.Remove(inputKeybordRight.Length - 1);

                }
            }
            else
            {
                if ((int)e.Code >= 0 && (int)e.Code <= 25)
                {
                    currentInputKeybordLeft += Convert.ToChar(e.Code + 97);
                    inputKeybordLeft += Convert.ToChar(e.Code + 97);
                }
                if ((int)e.Code == 26)
                {
                    currentInputKeybordLeft += ")";
                    inputKeybordLeft += ")";
                }
                if ((int)e.Code == 35)
                {
                    currentInputKeybordLeft += "(";
                    inputKeybordLeft += "(";
                }
                if ((int)e.Code == 48)
                {
                    currentInputKeybordLeft += ";";
                    inputKeybordLeft += ";";
                }
                if ((int)e.Code == 56)
                {
                    currentInputKeybordLeft += "_";
                    inputKeybordLeft += "_";
                }
                if ((int)e.Code == 58)
                {
                    ExecuteCommand(currentInputKeybordLeft);
                    if (currentInputKeybordLeft != "clear();")
                    {
                        countLinesLeft++;
                        inputKeybordLeft += "\n" + countLinesLeft + ": ";
                    }
                    currentInputKeybordLeft = "";
                }
                if ((int)e.Code == 59 && currentInputKeybordLeft.Length > 0)
                {
                    currentInputKeybordLeft = currentInputKeybordLeft.Remove(currentInputKeybordLeft.Length - 1);

                    inputKeybordLeft = inputKeybordLeft.Remove(inputKeybordLeft.Length - 1);

                }
            }
            if ((int)e.Code == 60)
            {
                rightPlayer = !rightPlayer;
            }
        }

        static void PlayerMove()
        {
            switch (playerDirection)
            {
                case MoveDirections.right:
                    faceSprite.Position =
                        new Vector2f(faceSprite.Position.X + 30f * deltaTime,
                        faceSprite.Position.Y);

                    if (faceSprite.Position.X + 32 >= 960)
                    {
                        playerDirection = MoveDirections.left;
                    }

                    break;
                case MoveDirections.left:
                    faceSprite.Position =
                        new Vector2f(faceSprite.Position.X - 30f * deltaTime,
                        faceSprite.Position.Y);

                    if (faceSprite.Position.X <= 320)
                    {
                        playerDirection = MoveDirections.right;
                    }

                    break;
                case MoveDirections.up:
                    faceSprite.Position =
                        new Vector2f(faceSprite.Position.X,
                        faceSprite.Position.Y - 30f * deltaTime);

                    if (faceSprite.Position.Y <= 0)
                    {
                        playerDirection = MoveDirections.down;
                    }

                    break;
                case MoveDirections.down:
                    faceSprite.Position =
                        new Vector2f(faceSprite.Position.X,
                        faceSprite.Position.Y + 30f * deltaTime);

                    if (faceSprite.Position.Y + 32 >= 640)
                    {
                        playerDirection = MoveDirections.up;
                    }

                    break;
            }
        }

        static void EnemyMove()
        {
            for (int i = 0; i < enemySprites.Count; i++)
            {
                float xE = enemySprites[i].Position.X;
                float yE = enemySprites[i].Position.Y;

                float xF = faceSprite.Position.X;
                float yF = faceSprite.Position.Y;

                double A = Math.Atan2(yE - yF, xE - xF);

                float dy, dx;

                dx = -30f * (float)Math.Cos(A);
                dy = -30f * (float)Math.Sin(A);

                enemySprites[i].Position =
                    new Vector2f(enemySprites[i].Position.X + dx * deltaTime,
                    enemySprites[i].Position.Y + dy * deltaTime);
            }
        }

        static void Main(string[] args)
        {
            //работо с окном и консолью

            window = new RenderWindow(new VideoMode(1280, 640), "Real Time Code");
            window.Closed += CloseWindow;
            window.KeyPressed += KeyPressed;

            window.Position = new Vector2i(0, 0);

            //загрузка рожи
            faceTexture = new Texture("face.png");
            faceSprite = new Sprite(faceTexture);
            faceSprite.Position = new Vector2f(320, 0);

            //загрузка врага
            enemyTexture = new Texture("enemy.png");

            //загрузка  пламени
            flameTexture = new Texture("ass_flame.png");
            flameSprite = new Sprite(flameTexture);

            //загрузка волны
            waveTexture = new Texture("ass_wave.png");
            waveSprite = new Sprite(waveTexture);


            for (int i = 0; i < numberWave * countEnemies; i++)
            {
                enemySprites.Add(new Sprite(enemyTexture));

                enemySprites[i].Position =
                    new Vector2f(rnd.Next(320, 960 - 32),
                    rnd.Next(320, 640 - 32));
            }

            //работа с шришфотом и подложкой
            Font font = new Font("arial.ttf");
            Text textRight;
            Text textLeft;

            RectangleShape consoleRight = new RectangleShape(new Vector2f(320, 640));
            consoleRight.FillColor = new Color(76, 145, 89);
            consoleRight.Position = new Vector2f(960, 0);


            RectangleShape consoleLeft = new RectangleShape(new Vector2f(320, 640));
            consoleLeft.FillColor = new Color(234, 67, 56);
            consoleLeft.Position = new Vector2f(0, 0);


            //игровой цикл
            while (window.IsOpen)
            {
                window.DispatchEvents();

                if (playGame == true)
                {

                    deltaTime = clk.Restart().AsSeconds();

                    //отрисовка кода
                    textRight = new Text(inputKeybordRight, font, 24);
                    textRight.Position = new Vector2f(960, 0);
                    textRight.Color = Color.Yellow;

                    textLeft = new Text(inputKeybordLeft, font, 24);
                    textLeft.Position = new Vector2f(0, 0);
                    textLeft.Color = Color.Yellow;

                    if (rightPlayer == true)
                    {
                        consoleRight.OutlineThickness = -3;
                        consoleRight.OutlineColor = Color.Yellow;

                        consoleLeft.OutlineThickness = 0;
                    }
                    else
                    {
                        consoleLeft.OutlineThickness = -3;
                        consoleLeft.OutlineColor = Color.Yellow;

                        consoleRight.OutlineThickness = 0;
                    }

                    EnemyMove();
                    PlayerMove();

                    if(countKills > 99)
                    {
                        window.Clear(new Color((byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255), 255));
                    }

                    window.Clear();
                    for (int i = 0; i < enemySprites.Count; i++)
                    {
                        window.Draw(enemySprites[i]);
                    }

                    if (assFlameShoot == true)
                    {
                        FloatRect flameRect = new FloatRect();

                        switch (playerDirection)
                        {
                            case MoveDirections.left:
                                flameSprite.Rotation = 0f;
                                flameSprite.Position = new Vector2f(faceSprite.Position.X + 32, faceSprite.Position.Y);

                                flameRect = new FloatRect(faceSprite.Position.X + 32, faceSprite.Position.Y, 280, 32);

                                break;
                            case MoveDirections.right:
                                flameSprite.Rotation = 180f;
                                flameSprite.Position = new Vector2f(faceSprite.Position.X, faceSprite.Position.Y + 32);

                                flameRect = new FloatRect(faceSprite.Position.X, faceSprite.Position.Y, -280, 32);

                                break;
                            case MoveDirections.down:
                                flameSprite.Rotation = 270f;
                                flameSprite.Position = new Vector2f(faceSprite.Position.X, faceSprite.Position.Y);

                                flameRect = new FloatRect(faceSprite.Position.X, faceSprite.Position.Y, 32, -280);

                                break;
                            case MoveDirections.up:
                                flameSprite.Rotation = 90f;
                                flameSprite.Position = new Vector2f(faceSprite.Position.X + 32, faceSprite.Position.Y + 32);

                                flameRect = new FloatRect(faceSprite.Position.X, faceSprite.Position.Y + 32, 32, 280);
                                break;
                        }

                        shootDelay -= deltaTime;
                        if (shootDelay <= 0) { assFlameShoot = false; }

                        window.Draw(flameSprite);

                        for (int i = 0; i < enemySprites.Count; i++)
                        {
                            FloatRect currentEnemy = new FloatRect(enemySprites[i].Position.X, enemySprites[i].Position.Y, 32, 32);

                            if (flameRect.Intersects(currentEnemy) == true)
                            {
                                enemySprites.RemoveAt(i);
                                i = -1;

                                countKills++;
                            }
                        }
                    }

                    if (waveShoot == true)
                    {
                        waveDelay -= deltaTime;
                        if (waveDelay <= 0) { waveShoot = false; }


                        waveSprite.Position =
                            new Vector2f(
                                faceSprite.Position.X - (200 - 32) / 2, faceSprite.Position.Y - (200 - 32) / 2);

                        window.Draw(waveSprite);

                        float xcFace = faceSprite.Position.X + 16;
                        float ycFace = faceSprite.Position.Y + 16;

                        for (int i = 0; i < enemySprites.Count; i++)
                        {
                            float xcEnemy = enemySprites[i].Position.X + 16;
                            float ycEnemy = enemySprites[i].Position.Y + 16;

                            float distance = (float)Math.Sqrt
                                (
                                  (xcEnemy - xcFace) * (xcEnemy - xcFace) + (ycEnemy - ycFace) * (ycEnemy - ycFace)
                                );

                            if (distance <= 116)
                            {
                                enemySprites.RemoveAt(i);
                                i = -1;

                                countKills++;
                            }
                        }
                    }

                    if (enemySprites.Count == 0)
                    {
                        numberWave++;

                        for (int i = 0; i < numberWave * countEnemies; i++)
                        {
                            enemySprites.Add(new Sprite(enemyTexture));

                            enemySprites[i].Position =
                                new Vector2f(rnd.Next(320, 960 - 32),
                                rnd.Next(320, 640 - 32));
                        }

                    }

                    FloatRect faceRect = new FloatRect(faceSprite.Position.X, faceSprite.Position.Y, 32, 32);

                    for (int i = 0; i < enemySprites.Count; i++)
                    {
                        FloatRect currentEnemyRect = new FloatRect(enemySprites[i].Position.X, enemySprites[i].Position.Y, 32, 32);

                        if (faceRect.Intersects(currentEnemyRect))
                        {
                            playGame = false;
                        }
                    }

                    window.Draw(faceSprite);

                    window.Draw(consoleRight);
                    window.Draw(consoleLeft);

                    window.Draw(textRight);
                    window.Draw(textLeft);

                    window.Display();
                }
                else
                {
                    string infoGameOver = String.Format("Вы убили {0} врагов\n и пережили {1} волн", countKills, numberWave);
                    Text textGameOver = new Text(infoGameOver, font, 24);

                    textGameOver.Position = new Vector2f(500, 320);
                    textGameOver.Color = Color.Red;

                    window.Draw(textGameOver);
                    window.Display();
                }
            }
        }
    }
}
