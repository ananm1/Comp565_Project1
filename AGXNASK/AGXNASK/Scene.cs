//Team: Anan Mallik, John A.
//Topic: Project 1

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace AGXNASK 
{

public class Scene : Stage 
{
   public Scene() { }
    
   protected override void LoadContent() 
   {

       base.LoadContent(); 

       Model3D sphinx1 = new Model3D(this, "sphinx", "sphinx");
       sphinx1.IsCollidable = false;
       sphinx1.addObject(new Vector3(400 * spacing, terrain.surfaceHeight(100, 100), 400 * spacing), 
           new Vector3(0, 2, 0), 0.79f);
       Components.Add(sphinx1);

       Model3D coffin1 = new Model3D(this, "coffin", "coffin");
       Model3D coffin2 = new Model3D(this, "coffin", "coffin");
       Model3D coffin3 = new Model3D(this, "coffin", "coffin");
       Model3D coffin4 = new Model3D(this, "coffin", "coffin");
       coffin1.IsCollidable = false;
       coffin2.IsCollidable = false;
       coffin3.IsCollidable = false;
       coffin4.IsCollidable = false;
       coffin1.addObject(new Vector3(30 * spacing, terrain.surfaceHeight(100, 50), 230 * spacing),
           new Vector3(0, 1, 0), 0.79f);
       coffin2.addObject(new Vector3(50 * spacing, terrain.surfaceHeight(100, 50), 400 * spacing),
           new Vector3(0, 1, 0), 0.79f);
       coffin3.addObject(new Vector3(250 * spacing, terrain.surfaceHeight(100, 50), 250 * spacing),
           new Vector3(0, 1, 0), 0.79f);
       coffin4.addObject(new Vector3(305 * spacing, terrain.surfaceHeight(100, 50), 450 * spacing),
           new Vector3(0, 1, 0), 0.79f);
       Components.Add(coffin1);
       Components.Add(coffin2);
       Components.Add(coffin3);
       Components.Add(coffin4);


      // create the Scene entities -- Inspector.
      // create walls for obstacle avoidance or path finding algorithms
      Wall wall = new Wall(this, "wall", "100x100x100Brick");
      Components.Add(wall);
      // Set initial camera and projection matrix
      nextCamera();  // select the first camera
      }


   protected override void Update(GameTime gameTime)
   {
      base.Update(gameTime);

   }

      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      static void Main(string[] args) 
      {
         using (Scene stage = new Scene()){ stage.Run(); 
      }
         }
   }
}
