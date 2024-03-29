﻿using TrueRogueliike.Components;
using TrueRogueliike.Core;

class Program
{
    static void Main()
    {
        int width = 50; 
        int height = 20;

        Random random = new();

        GameObjectFactory factory = new();
        GameScene scene = new(width, height, factory, random); 

        Player player = factory.CreatePlayer(new VectorPosition(1, 1), scene);

        GameLoop gameLoop = new(scene, player); 
        
        gameLoop.Run(); 
    }
}
