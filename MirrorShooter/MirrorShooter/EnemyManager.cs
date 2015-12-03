using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MirrorShooter
{

    public enum EnemyType
    {
        Diver
    };
    public class EnemyManager
    {
        List<EnemyShip> enemyList;
        Player player;
        int level;
        int levelValue = 0;
        float timer = 0;
        float NextSpawn = 1000;
        int pow;
        bool isPlayerAlive = true;
        
       
        Random rand;
        Array values = Enum.GetValues(typeof(EnemyType));

        public EnemyManager(List<EnemyShip> a_enemyList,int start_level, Player a_player)
        {
            level = start_level;
            levelValue = level * 5 + 5 + (level-1)*5;
            pow = level;
            if (pow > 6)
                pow = 6;
            levelValue = level * 5 + (int)Math.Pow(2, pow) + 3;
            //levelValue = 100000;
            enemyList = a_enemyList;
            
            player = a_player;
            rand = new Random();
            
            
        }

        public void Update(float a_deltaTime)
        {
            if (isPlayerAlive)
            {
                timer += a_deltaTime;
                Console.WriteLine(timer);
                if (levelValue > 0)
                {
                    if (timer > NextSpawn)
                    {
                        SpawnEnemy();
                        timer = 0;
                    }
                }
                else
                {
                    if (enemyList.Count == 0)
                    {
                        ReadyNextLevel();
                    }
                }



            }
            else
            {


            }
        }

        public void ReadyNextLevel()
        {
            level++;
            if (pow > 6)
                pow = 6;
            levelValue = level * 5 + (int)Math.Pow(2, pow) + 3;
        }

        private void SpawnEnemy()
        {

            EnemyType randomType = (EnemyType)values.GetValue(rand.Next(values.Length));
            Vector2 spawnPosition = new Vector2();
            spawnPosition = GeneratePosition();
            switch (randomType)
            {
                case EnemyType.Diver:
                    Diver enemy = new Diver(spawnPosition, player, 0);
                    levelValue -= enemy.value;
                    enemyList.Add(enemy);
                    break;

            }
           
            
        }

       

        private Vector2 GeneratePosition()
        {
            float posX = rand.Next(100, 2900);
            float posY = rand.Next(100, 2900);
            Vector2 spawnPosition = new Vector2(posX, posY);
            if ((spawnPosition - player.GetMidPosition()).LengthSquared() > 200 * 200)
            {
                return spawnPosition;
            }
            else
            {
                return GeneratePosition();
            }
        }

        

        public int GetCurrentLevel()
        {
            return level;
        }



    }
}
