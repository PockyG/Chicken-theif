using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MirrorShooter
{

    public enum ChickenState
    {
        Opening,
        Winner,
        Loser,
        Play,
    }
    public class InGame : GameState
    {
        public Player player;
        public Camera camera;
        public List<Plane> planeList;
        public List<Player> playerList;
        public List<Chicken> chickenList;
        public Chicken selectedChicken = null;
        public ParticleEngine pEngine;
        public Selector selector;

        public Game1 Manager;

        public ChickenPlayer chickenPlayer;
        

        public Vector2 FontPos;
        public Random random;
        public Picture2D background;
        public Picture2D shootButton;
        public Picture2D bullet;
        public Picture2D tutorial;

        public const int PLAYFIELD_WIDTH = 3000;
        public const int PLAYFIELD_HEIGHT = 3000;

        //ChickenPlayer properties
        public int selectColour = 0;

        //ChickenSpawner
        float timeBetweenChicken = 10;
        float chickenTimer = 0;
        int numOfChickens = 300;
        int chickenCounter = 0;


        //PLAYER PROPS
        float openingStartDelay = 5000;
        float TimerBeforeShot = 20000;
        float shootTimer = 0;


        //Opening stuff.
        ChickenState gameState = ChickenState.Opening;


        public InGame(int starting_level, Game1 manager)
            : base()
        {
            //instantiations
            Manager = manager;
            random = new Random();
            player = new Player(new Vector2(100, 100), 40);
            playerList = new List<Player>();
            playerList.Add(player);
            planeList = new List<Plane>();
            camera = new Camera(Vector2.Zero);
            chickenList = new List<Chicken>();
            pEngine = new ParticleEngine(new Vector2(0, 0));
            selector = new Selector(new Vector2(100, 100));
            FontPos = new Vector2(40, 40);

            tutorial = new Picture2D(3, 600, 600, new Vector2(1280/2, 720/2));
            tutorial.isVisible = true;
            bullet = new Picture2D(2, 50, 50, new Vector2(0, 0));
            background = new Picture2D(0, 1280, 720, new Vector2(1280 / 2, 720 / 2));
            shootButton = new Picture2D(1, 150, 100, new Vector2(100, 100));

            


            float box = 400;
            InputManager.SetCamera(camera);
            Plane plane = new Plane(new Vector3(1, 0, 20));
            planeList.Add(plane);
            plane = new Plane(new Vector3(0, 1, -200));
            planeList.Add(plane);
            plane = new Plane(new Vector3(-1, 0, 1200));
            planeList.Add(plane);
            plane = new Plane(new Vector3(0, -1, 670));
            planeList.Add(plane);








            //some kind of epic opening

            camera.SetPlayer(player);


        }


        public void CheckGameOver()
        {
            if (player.gunAmmo <= 0)
            {
                gameState = ChickenState.Loser;
                foreach (Chicken i in chickenList)
                {
                    i.Flee(chickenPlayer.GetMidPosition());

                }
            }

           
           
        }
        public override void Update(float a_deltaTime)
        {
            //OPENING HERE
            switch (gameState)
            {
                case ChickenState.Opening:
                    {
                        if (InputManager.Instance.KeyPressed(Keys.W))
                        {
                            selectColour = 0;
                            gameState = ChickenState.Play;
                            tutorial.isVisible = false;

                        }
                        else if (InputManager.Instance.KeyPressed(Keys.S))
                        {
                            selectColour = 1;
                            gameState = ChickenState.Play;
                            tutorial.isVisible = false;
                        }
                        else if (InputManager.Instance.KeyPressed(Keys.A))
                        {
                            selectColour = 2;
                            gameState = ChickenState.Play;
                            tutorial.isVisible = false;
                        }
                        else if (InputManager.Instance.KeyPressed(Keys.D))
                        {
                            selectColour = 3;
                            gameState = ChickenState.Play;
                            tutorial.isVisible = false;
                        }
                        

                        break;
                    }
                case ChickenState.Play:
                    {

                        
                            //START OF GAME
                            shootTimer += a_deltaTime;
                        if (shootTimer > TimerBeforeShot)
                        {
                            shootTimer = 0;
                            player.gunAmmo--;
                            CheckGameOver();
                        }


                        pEngine.Update(a_deltaTime);
                        pEngine.position = InputManager.Instance.GetMousePosition() + camera.TopLeft;

                        chickenTimer += a_deltaTime;
                        if (chickenCounter < numOfChickens)
                        {
                            if (chickenTimer > timeBetweenChicken)
                            {
                                chickenTimer = 0;
                                int temp = random.Next(360);
                                Vector2 startVel = new Vector2((float)Math.Cos(temp * (Math.PI / 180)) * 0.5f, (float)Math.Sin(temp * (Math.PI / 180)) * 0.5f);

                                Chicken tempChicken = new Chicken(new Vector2(500, 500), 50, 35, startVel);
                                //Sets the chicken thief.
                                if (chickenCounter == 100)
                                {
                                    chickenPlayer = new ChickenPlayer(new Vector2(500, 500), 50, 35, startVel);
                                    tempChicken = chickenPlayer;
                                    tempChicken.chickenColour = selectColour;
                                    tempChicken.walletTaker = true;
                                }
                                chickenList.Add(tempChicken);
                                chickenCounter++;
                            }
                        }


                        for (int i = chickenList.Count - 1; i >= 0; i--)
                        {
                            chickenList[i].Update(a_deltaTime);
                            if (chickenList[i].isAlive == false)
                            {
                                chickenList.RemoveAt(i);
                            }


                        }


                        for (int i = playerList.Count - 1; i >= 0; i--)
                        {
                            playerList[i].Update(a_deltaTime);
                            if (playerList[i].isAlive == false)
                            {
                                playerList.RemoveAt(i);
                                continue;
                            }
                        }






                        //Chicken physics
                        ChickensInChickens();
                        ChickensWallReaction();


                        if (selector != null)
                        {
                            selector.Update(a_deltaTime);
                        }

                        if (InputManager.Instance.MousePressed(MouseButton.Right))
                        {
                            

                           
                                foreach (Chicken i in chickenList)
                                {
                                    if ((InputManager.Instance.GetMouseWorldPosition() - new Vector2(player.GetRadius(), player.GetRadius()) - i.GetMidPosition()).LengthSquared() < 100 * 100)
                                    {
                                        //collision
                                        i.Flee(InputManager.Instance.GetMouseWorldPosition() - new Vector2(player.GetRadius(), player.GetRadius()));


                                    }

                                }
                            

                        }
                        if (InputManager.Instance.MousePressed(MouseButton.Left))
                        {
                            //selects the shoot button
                            if (((InputManager.Instance.GetMouseWorldPosition() - shootButton.GetMidPosition()).LengthSquared() < 100 * 100) && selectedChicken != null)
                            {
                                //If player shoots the wallet taker. WINNAAAA.
                                if (selectedChicken.walletTaker == true)
                                {
                                    gameState = ChickenState.Winner;
                                }
                                shootTimer = 0;
                                player.gunAmmo--;
                                CheckGameOver();

                                selectedChicken.Death();
                                selector.isVisible = false;
                                selectedChicken = null;

                            }


                            foreach (Chicken i in chickenList)
                            {
                                if (selectedChicken != null)
                                {
                                    selectedChicken = null;
                                    selector.isVisible = false;

                                }
                                if ((InputManager.Instance.GetMouseWorldPosition() - i.GetMidPosition() - new Vector2(i.GetRadiusHitbox(), i.GetRadiusHitbox())).LengthSquared() < i.GetRadiusHitbox() * i.GetRadiusHitbox())
                                {
                                    selectedChicken = i;
                                    i.isSelected = true;
                                    selector.Follow(i);
                                    selector.isVisible = true;

                                    break;
                                }
                            }

                            

                        }





                        /*
                        camera.Update(a_deltaTime);
                         */
                        break;
                    }

               case ChickenState.Winner:
                    {
                  
                                                chickenTimer += a_deltaTime;
                        if (chickenCounter < numOfChickens)
                        {
                            if (chickenTimer > timeBetweenChicken)
                            {
                                chickenTimer = 0;
                                int temp = random.Next(360);
                                Vector2 startVel = new Vector2((float)Math.Cos(temp * (Math.PI / 180)) * 0.5f, (float)Math.Sin(temp * (Math.PI / 180)) * 0.5f);

                                Chicken tempChicken = new Chicken(new Vector2(500, 500), 50, 35, startVel);
                                if (chickenCounter == 100)
                                {
                                    chickenPlayer = new ChickenPlayer(new Vector2(500, 500), 50, 35, startVel);
                                    tempChicken = chickenPlayer;
                                    tempChicken.chickenColour = selectColour;
                                    tempChicken.walletTaker = true;
                                }
                                chickenList.Add(tempChicken);
                                chickenCounter++;
                            }
                        }


                        for (int i = chickenList.Count - 1; i >= 0; i--)
                        {
                            chickenList[i].Update(a_deltaTime);
                            if (chickenList[i].isAlive == false)
                            {
                                chickenList.RemoveAt(i);
                            }


                        }


                        for (int i = playerList.Count - 1; i >= 0; i--)
                        {
                            playerList[i].Update(a_deltaTime);
                            if (playerList[i].isAlive == false)
                            {
                                playerList.RemoveAt(i);
                                continue;
                            }
                        }

                     






                        //Chicken physics
                        ChickensInChickens();
                        ChickensWallReaction();


                        if (InputManager.Instance.KeyPressed(Keys.R))
                        {
                            Manager.RestartGame();
                        }
                        break;
                    }
                case ChickenState.Loser:
                    {
                        
                                                chickenTimer += a_deltaTime;



                        if (chickenCounter < numOfChickens)
                        {
                            if (chickenTimer > timeBetweenChicken)
                            {
                                chickenTimer = 0;
                                int temp = random.Next(360);
                                Vector2 startVel = new Vector2((float)Math.Cos(temp * (Math.PI / 180)) * 0.5f, (float)Math.Sin(temp * (Math.PI / 180)) * 0.5f);

                                Chicken tempChicken = new Chicken(new Vector2(500, 500), 50, 35, startVel);
                                if (chickenCounter == 100)
                                {
                                    chickenPlayer = new ChickenPlayer(new Vector2(500, 500), 50, 35, startVel);
                                    tempChicken = chickenPlayer;
                                    tempChicken.chickenColour = selectColour;
                                    tempChicken.walletTaker = true;
                                }
                                chickenList.Add(tempChicken);
                                chickenCounter++;
                            }
                        }


                        for (int i = chickenList.Count - 1; i >= 0; i--)
                        {
                            chickenList[i].Update(a_deltaTime);
                            if (chickenList[i].isAlive == false)
                            {
                                chickenList.RemoveAt(i);
                            }

                            


                        }


                        for (int i = playerList.Count - 1; i >= 0; i--)
                        {
                            playerList[i].Update(a_deltaTime);
                            if (playerList[i].isAlive == false)
                            {
                                playerList.RemoveAt(i);
                                continue;
                            }
                        }

                        
                            
                        

                        //Chicken physics
                        ChickensInChickens();
                        ChickensWallReaction();

                        if (InputManager.Instance.KeyPressed(Keys.R))
                        {
                            Manager.RestartGame();
                        }

                        break;
                    }
            

            }






        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch, camera);
            switch (gameState)
            {
                case ChickenState.Opening:
                    {
                        tutorial.Draw(spriteBatch, camera);
                        spriteBatch.DrawString(Game1.Font1, "...it took your wallet.", FontPos, Color.Blue,
                                0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
                        break;
                    }
                case ChickenState.Play:
                    {

                        foreach (Player i in playerList)
                        {
                            i.Draw(spriteBatch, camera);
                        }


                        pEngine.Draw(spriteBatch, camera);

                        foreach (Chicken i in chickenList)
                        {
                            i.Draw(spriteBatch, camera);
                        }
                        selector.Draw(spriteBatch, camera);
                        if (selectedChicken != null)
                        {
                            shootButton.isVisible = true;
                        }
                        else
                        {
                            shootButton.isVisible = false;

                        }

                        for (int i = 0; i < player.gunAmmo; i++)
                        {
                            bullet.SetMidPosition(new Vector2(1280 -  60 - (i * 40), 50));
                            bullet.Draw(spriteBatch, camera);
                        }

                        shootButton.Draw(spriteBatch, camera);
                        spriteBatch.DrawString(Game1.Font1, "That chicken just stole your wallet..", FontPos, Color.Blue,
                            0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);

                        spriteBatch.DrawString(Game1.Font1, "TIME UNTIL SHOT: " + (20 - ((int)shootTimer/1000)).ToString(), new Vector2(820, 30), Color.Red, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
                        break;
                    }
                case ChickenState.Winner:
                    {
                       
                        foreach (Chicken i in chickenList)
                        {
                            i.Draw(spriteBatch, camera);
                        }

                        spriteBatch.DrawString(Game1.Font1, "WINNER WINNER!", new Vector2(1280 / 2 - 300, 0), Color.Red, 0, new Vector2(0, 0), 4.0f, SpriteEffects.None, 0.5f);
                        spriteBatch.DrawString(Game1.Font1, "CHICKEN DINNER! ", new Vector2(1280 / 2 - 300, 100), Color.Red, 0, new Vector2(0, 0), 3.8f, SpriteEffects.None, 0.5f);
                        spriteBatch.DrawString(Game1.Font1, "R to restart. ", new Vector2(1280 / 2 - 300, 200), Color.Red, 0, new Vector2(0, 0), 1.3f, SpriteEffects.None, 0.5f);
                        break;
                    }
                case ChickenState.Loser:
                    {
 
                        foreach (Chicken i in chickenList)
                        {
                            i.Draw(spriteBatch, camera);
                            i.winner = true;

                        }
                        spriteBatch.DrawString(Game1.Font1, "THE CHICKEN GOT AWAY!  ", new Vector2(1280 / 2 - 400, 0), Color.Red, 0, new Vector2(0, 0), 4.0f, SpriteEffects.None, 0.5f);
                        spriteBatch.DrawString(Game1.Font1, "All your monies :(  ", new Vector2(1280 / 2 - 400, 100), Color.Red, 0, new Vector2(0, 0), 3.8f, SpriteEffects.None, 0.5f);
                        spriteBatch.DrawString(Game1.Font1, "R to restart. ", new Vector2(1280 / 2 - 300, 200), Color.Red, 0, new Vector2(0, 0), 1.3f, SpriteEffects.None, 0.5f);
                        
                        break;
                    }
            }




           
           

        }





        public void ChickensInChickens()
        {
            for (int i = 0; i < chickenList.Count; i++)
            {
                
                Chicken chicken1 = (Chicken)chickenList[i];
                for (int j = i + 1; j < chickenList.Count; j++)
                {
                   
                    Chicken chicken2 = (Chicken)chickenList[j];
                    if ((chicken2.GetMidPosition() - chicken1.GetMidPosition()).LengthSquared() < (chicken1.GetRadiusHitbox() + chicken2.GetRadiusHitbox()) * (chicken1.GetRadiusHitbox() + chicken2.GetRadiusHitbox()))
                    {
                        //collision happened
                        Vector2 N = chicken1.GetMidPosition() - chicken2.GetMidPosition();
                        N.Normalize();
                        N *= 0.1f;
                        float velAlongNormal = Vector2.Dot(chicken1.GetVelocity() - chicken2.GetVelocity(), N);
                        if (velAlongNormal > 0)
                            continue;


                        if (chicken1.GetVelocity().LengthSquared() < 0.005f)
                            chicken1.SetVelocity(chicken1.GetVelocity() + N);

                        if (chicken2.GetVelocity().LengthSquared() < 0.005f)
                            chicken2.SetVelocity(chicken2.GetVelocity() - N);
                    }


                }


            }
        }

        public void ChickensWallReaction()
        {
            foreach (Chicken i in chickenList)
            {
                foreach (Plane j in planeList)
                {
                    Vector2 N = new Vector2(j.projection.X, j.projection.Y);
                    N.Normalize();
                    Chicken chicken = (Chicken)i;

                    float distance = Vector2.Dot(i.GetMidPosition(), N) + j.projection.Z;
                    if (distance - chicken.GetRadiusHitbox() < 0 && Vector2.Dot(chicken.GetVelocity(), N) < 0)
                    {

                        //float e = i.GetRestitution();

                        chicken.SetVelocity(chicken.GetVelocity() - 2 * N * Vector2.Dot(N, chicken.GetVelocity()));






                    }





                }

            }
        }



    }
}

