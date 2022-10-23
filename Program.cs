using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Sources;
using GameEngine;

namespace Snake{
    class Program {
        public static Engine engine = new Engine();
        public static int direction = 1;
        public static Script headScript = new Script();
        public static Random r = new Random();
        public static bool isdead = false;
        public static int score = 0;
        public static void Main(string[] args) {
            engine.CreateGameObject("head", "", 1, 1, '#');
            headScript.Start = headStart;
            headScript.Update = headUpdate;
            engine.GameObjects[0].AddScript(headScript);
            engine.CreateGameObject("food", "", r.Next(1, 49), r.Next(1, 25), '@');
            engine.CreateGameObject("food", "", r.Next(1, 49), r.Next(1, 25), '@');
            engine.setResolution(50, 25);
            engine.setFramerate(4);
            engine.defaultFramerate = 4;
            engine.changeColor(ConsoleColor.White, ConsoleColor.DarkGray);
            engine.changeDefaultcolor(ConsoleColor.White, ConsoleColor.Black);
            engine.debug = true;
            engine.Init();
            while (true) {
                int key = (int)Console.ReadKey(true).Key;
                if(key == 87 && direction != 1 || key == 38 && direction != 1){
                    direction = 0;
                }
                else if (key==83 && direction != 0 || key == 40 && direction != 0){
                    direction = 1;
                }
                else if (key == 37 && direction != 3 || key == 65&& direction != 3){
                    direction = 2;
                }
                else if (key == 39 && direction != 2  || key == 68 && direction != 2){
                    direction = 3;
                }
            }
        }
        public static void foodEaten(){
            engine.CreateGameObject("body","",0,0, '█');
            score++;
            engine.GameObjects[1].X = r.Next(1,49);
            engine.GameObjects[1].Y = r.Next(1, 24);
        }
        public static void foodEaten2(){
            engine.CreateGameObject("body", "", 0, 0, '█');
            score++;
            engine.GameObjects[2].X = r.Next(1, 49);
            engine.GameObjects[2].Y = r.Next(1, 24);
        }
        public static void headUpdate(){
            if (engine.GameObjects[1].X == engine.GameObjects[0].X && engine.GameObjects[1].Y == engine.GameObjects[0].Y){
                foodEaten();
                engine.GameObjects[engine.GameObjects.Count - 1].X = engine.GameObjects[engine.GameObjects.Count - 2].X;
                engine.GameObjects[engine.GameObjects.Count - 1].Y = engine.GameObjects[engine.GameObjects.Count - 2].Y;
            }
            if (engine.GameObjects[2].X == engine.GameObjects[0].X && engine.GameObjects[2].Y == engine.GameObjects[0].Y){
                foodEaten2();
                engine.GameObjects[engine.GameObjects.Count - 1].X = engine.GameObjects[engine.GameObjects.Count - 2].X;
                engine.GameObjects[engine.GameObjects.Count - 1].Y = engine.GameObjects[engine.GameObjects.Count - 2].Y;
            }
            engine.addTextBefore("Score:" +score.ToString());
            if (engine.GameObjects.Count > 3){
                
            
            for (int i = engine.GameObjects.Count-1; i > 3; i--){
                if(engine.GameObjects[i].X == engine.GameObjects[0].X && engine.GameObjects[i].Y == engine.GameObjects[0].Y){
                death();
                }
                engine.GameObjects[i].X = engine.GameObjects[i - 1].X;
                engine.GameObjects[i].Y = engine.GameObjects[i - 1].Y;
            }
            engine.GameObjects[3].X = engine.GameObjects[0].X;
            engine.GameObjects[3].Y = engine.GameObjects[0].Y;
            }
            if (direction == 0){
                if (engine.GameObjects[0].Y == 0) { death(); }
                engine.GameObjects[0].Y--;
            }
            else if (direction == 1){
                if (engine.GameObjects[0].Y == 24) { death(); }
                engine.GameObjects[0].Y++;
            }
            else if (direction == 2){
                if (engine.GameObjects[0].X == 0) { death(); }
                engine.GameObjects[0].X--;
            }
            else if (direction == 3){
                if (engine.GameObjects[0].X == 49) { death(); }
                engine.GameObjects[0].X++;
            }
        }
        public static void headStart() {}
        public static void death(){
            engine.changeColor(ConsoleColor.White, ConsoleColor.Red);
            engine.Stop();
            Console.WriteLine("you losed");
        }
    }
}