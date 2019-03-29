using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteeringProject;

using SOFT152SteeringLibrary;

namespace SOFT152Steering
{
    public partial class AntFoodForm : Form
    {

        // Declare a stationary object (For early testing, before lists)
        //private SOFT152Vector nestObject = new SOFT152Vector(100, 100);

        // the random object given to each Ant agent
        private Random randomGenerator;

        // A bitmap image used for double buffering
        private Bitmap backgroundImage;

        private SOFT152Vector foodPositionVector;

        // Creating new lists
        private List<AntAgent> antList;

        private List<Food> foodList;

        private List<Nest> nestList;

        private List<RobberAntAgent> robberAntList;

        private List<RobberNest> robberNestList;
        

        // For when the ants ask for directions
        private int antRadius = 8;

        // For when the ants are close proximity to food or nest
        private int closeProximityTo = 16;
        


        public AntFoodForm()
        {
            InitializeComponent();
            CreateBackgroundImage();

            antList = new List<AntAgent>();

            foodList = new List<Food>();

            nestList = new List<Nest>();

            robberAntList = new List<RobberAntAgent>();

            robberNestList = new List<RobberNest>();

            //CreateAnts();

        }

        /// <summary>
        /// Adds a specific number of ants to list, depending on user input.
        /// </summary>
        /// <param name="quantity"></param>
        private void CreateAnts(int quantity)
        {
            Rectangle worldLimits;

            // create a random object to pass to the ants
            randomGenerator = new Random();

            // Define some world size for the ants to move around on
            // assume the size of the world is the same size as the panel
            // on which they are displayed
            worldLimits = new Rectangle(0, 0, drawingPanel.Width, drawingPanel.Height);


            Random randomNumber = new Random();

            // Creating the list for ants
            for (int i = 0; i < quantity; i++)
            {
                // Creating random positions
                int xStart = randomNumber.Next(1, drawingPanel.Width);
                int yStart = randomNumber.Next(1, drawingPanel.Height);

                // Adding nest ant to list 'x' amount of times
                antList.Add(new AntAgent(new SOFT152Vector(xStart, yStart), randomGenerator, worldLimits));
            }

            

        }

        /// <summary>
        /// Adds a specific number of robber ants to list, depending on user input.
        /// </summary>
        /// <param name="quantity"></param>
        private void CreateRobberAnts(int quantity)
        {
            Rectangle robberAntWorldLimits;

            // create a radnom object to pass to the ants
            randomGenerator = new Random();

            // Define some world size for the ants to move around on
            // assume the size of the world is the same size as the panel
            // on which they are displayed
            robberAntWorldLimits = new Rectangle(0, 0, drawingPanel.Width, drawingPanel.Height);

            // Creating the list for robber ants
            for (int r = 0; r < quantity; r++)
            {
                robberAntList.Add(new RobberAntAgent(new SOFT152Vector(drawingPanel.Width / 2, drawingPanel.Height / 2), randomGenerator, robberAntWorldLimits));
            }
        }

        /// <summary>
        ///  Creates the background image to be used in double buffering 
        /// </summary>
        private void CreateBackgroundImage()
        {
            int imageWidth;
            int imageHeight;

            // the backgroundImage  can be any size
            // assume it is the same size as the panel 
            // on which the Ants are drawn
            imageWidth = drawingPanel.Width;
            imageHeight = drawingPanel.Height;

            backgroundImage = new Bitmap(drawingPanel.Width, drawingPanel.Height);
        }

       
        /// <summary>
        /// Tick tock
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            

            // Loop to go through the ant list - For behaviours
            for (int i = 0; i < antList.Count; i++)
            {
                // Specifications
                antList[i].AgentSpeed = 1.6;
                antList[i].WanderLimits = 0.25;

                // Keeps the agents within the world
                antList[i].ShouldStayInWorldBounds = true;

                // Loop to go through the food list, and if they get close to it
                for (int f = 0; f < foodList.Count; f++)
                {

                    if (foodList.ElementAt(f) != null)
                    {
                        // To approach food if they get close to it.
                        // Once within this radius, they start to approach the center.
                        if (foodList[f].FoodPosition.Distance(antList[i].AgentPosition) < antList[i].ApproachRadius + closeProximityTo && antList[i].HasFood == false)
                        {

                            antList[i].KnowsFoodLocation = true;

                        } // When they get really close to it
                        if (foodList[f].FoodPosition.Distance(antList[i].AgentPosition) < antList[i].ApproachRadius && antList[i].HasFood == false)
                        {
                            
                            // If there is any food left
                            if (foodList[f].FoodSize >= 1)
                            {
                                
                                antList[i].SpecificFood = f;
                                PickupFood(i, f);

                            } // if not, destroy food
                            else
                            {
                                DestroyFood(i, f);

                            }

                        }

                    }
                    

                }
                
                // Loop for when they are close to nest
                for (int n = 0; n < nestList.Count; n++)
                {

                    // To approach nest if they get close to it
                    // Once within this radius, they start to approach the center.
                    if (nestList[n].NestPosition.Distance(antList[i].AgentPosition) < antList[i].ApproachRadius + closeProximityTo)
                    {

                        antList[i].KnowsNestLocation = true;

                    } 
                    
                    // When they get really close to it
                    if (nestList[n].NestPosition.Distance(antList[i].AgentPosition) < antList[i].ApproachRadius)
                    {

                        PutDownFood(i);
                        antList[i].SpecificNest = n;

                    }
                }
                    

                // Loop for when the ants communicate and ask for directions
                for (int p = 0; p < antList.Count; p++)
                {

                    // If statement for close proximity
                    if (Math.Sqrt(Math.Pow(antList[i].AgentPosition.X - antList[p].AgentPosition.X, 2) + Math.Pow(antList[i].AgentPosition.Y - antList[p].AgentPosition.Y, 2)) <= antRadius * 2)
                    {
                        // Ask for directions
                        AskForDirections(i, p);
                    }
                    
                }

                


                // Scenario 1: Does not know food location and does not have food (knows nothing)
                if (antList[i].KnowsFoodLocation == false && antList[i].HasFood == false) 
                {
                    antList[i].Wander();
                }

                
                // Scenario 2: Knows food location but doesnt have food
                if (antList[i].KnowsFoodLocation == true && antList[i].HasFood == false)
                {

                    if (foodList.ElementAt(antList[i].SpecificFood) != null)
                    {
                        antList[i].Approach(foodList[antList[i].SpecificFood].FoodPosition);
                    }
                    else
                    {
                        antList[i].Wander();
                    }
                    
                }

                // Scenario 3: Knows food location and has food
                if (antList[i].KnowsFoodLocation == true && antList[i].HasFood == true) 
                {                                                                           

                    // Conditions for if the ant knows where the nest is or not
                    // Nested if statement because of when the food was destroyed
                    if (antList[i].KnowsNestLocation == true)
                    {
                        antList[i].Approach(nestList[antList[i].SpecificNest].NestPosition);
                    }
                    else
                    {
                        antList[i].Wander();
                    }

                }

            } // End of antList loop

            // Loop designated to the robber ants
            for (int r = 0; r < robberAntList.Count; r++)
            {

                // loop for when robber ants are near robber nest
                for (int q = 0; q < robberNestList.Count; q++)
                {

                    // To approach nest if they get close to it
                    // Once within this radius, they start to approach the center.
                    if (robberNestList[q].NestPosition.Distance(robberAntList[r].AgentPosition) < robberAntList[r].ApproachRadius + closeProximityTo)
                    {

                        robberAntList[r].KnowsNestLocation = true;

                    } 

                    // When they get really close to it
                    if (robberNestList[q].NestPosition.Distance(robberAntList[r].AgentPosition) < robberAntList[r].ApproachRadius)
                    {

                        if (robberAntList[r].HasFood == true)
                        {
                            PutDownStolenFood(r);
                        }
                        
                        robberAntList[r].SpecificNest = q;

                    }
                }

                // Loop for when the robber ants steal from an ant
                for (int p = 0; p < antList.Count; p++)
                {

                    // If statement for close proximity
                    if (Math.Sqrt(Math.Pow(robberAntList[r].AgentPosition.X - antList[p].AgentPosition.X, 2) + Math.Pow(robberAntList[r].AgentPosition.Y - antList[p].AgentPosition.Y, 2)) <= antRadius * 2)
                    {
                        // Checks if the ant has food to be stolen
                        if (antList[p].HasFood == true && robberAntList[r].HasFood == false)
                        {
                            // Steal the food
                            StealFood(r, p);

                        }


                    }
                    
                    
                }


                // Loop for when the ants communicate and ask for directions
                for (int p = 0; p < robberAntList.Count; p++)
                {

                    // If statement for close proximity
                    if (Math.Sqrt(Math.Pow(robberAntList[r].AgentPosition.X - robberAntList[p].AgentPosition.X, 2) + Math.Pow(robberAntList[r].AgentPosition.Y - robberAntList[p].AgentPosition.Y, 2)) <= antRadius * 2)
                    {
                        // Ask for directions
                        AskForTheftDirections(r, p);

                    }
                    
                }




                // Scenario 1: Does not know food location and does not have food (knows nothing)
                if (robberAntList[r].KnowsFoodLocation == false && robberAntList[r].HasFood == false)
                {
                    robberAntList[r].Wander();
                    robberAntList[r].RobberAntCounter = 0;
                }

                // Scenario 2: Knows food location but doesnt have food
                if (robberAntList[r].KnowsFoodLocation == true && robberAntList[r].HasFood == false)
                {

                    // Declares the food position point and vector
                    // So the robber ant can approach this position
                    Point foodPoint = new Point(robberAntList[r].FoodPosX, robberAntList[r].FoodPosY);
                    foodPositionVector = new SOFT152Vector(foodPoint.X, foodPoint.Y);

                    
                    // Checks if the counter is below 500
                    if (robberAntList[r].RobberAntCounter < 200)
                    {
                        // Approach the food location
                        robberAntList[r].Approach(foodPositionVector);
                        robberAntList[r].RobberAntCounter++;

                    }
                    else
                    {
                        // Forgets food location
                        robberAntList[r].KnowsFoodLocation = false;

                        // Resets counter
                        robberAntList[r].RobberAntCounter = 0;

                        // Loop so any ants close to them will also forget the food
                        // location and counter resets to 0
                        for (int p = 0; p < robberAntList.Count; p++)
                        {
                            // If statements for close proximity
                            // Horizontal distance
                            if (robberAntList[r].AgentPosition.X <= (robberAntList[p].AgentPosition.X + antRadius))
                            {

                                // && condition to prevent long if statement
                                if (robberAntList[r].AgentPosition.X >= (robberAntList[p].AgentPosition.X - antRadius))
                                {

                                    // Vertical distance
                                    if (robberAntList[r].AgentPosition.Y <= (robberAntList[p].AgentPosition.Y + antRadius))
                                    {

                                        // && condition to prevent long if statement
                                        if (robberAntList[r].AgentPosition.Y >= (robberAntList[p].AgentPosition.Y - antRadius))
                                        {

                                            // Forgets food location
                                            robberAntList[p].KnowsFoodLocation = false;

                                            robberAntList[p].RobberAntCounter = 0;

                                        }
                                        

                                    }

                                }

                                
                            }
                        }
                    }

                    

                    // If statements so if the ant isnt there with food, they will instead wander ^
                    

                }

                // Scenario 3: Knows food location and has food
                if (robberAntList[r].KnowsFoodLocation == true && robberAntList[r].HasFood == true)
                {
                    
                    // Conditions for if the ant knows where the nest is or not
                    // Nested if statement because of when the food was destroyed
                    if (robberAntList[r].KnowsNestLocation == true)
                    {
                        robberAntList[r].Approach(robberNestList[robberAntList[r].SpecificNest].NestPosition);
                    }
                    else
                    {
                        robberAntList[r].Wander();
                    }

                }


            } // End of robberAntList loop


            DrawAgentsDoubleBuffering();
        }

        
        /// <summary>
        /// Method that makes the ant pick up some food. By changing booleans.
        /// The "antID" is which ant in the list, the method applies to.
        /// </summary>
        /// <param name="antID"></param>
        private void PickupFood(int antID, int foodID)
        {

            // Changes booleans
            antList[antID].HasFood = true;
            antList[antID].KnowsFoodLocation = true;
                
            // To make the food smaller
            foodList[foodID].FoodSize -= 0.05;
            
            
        }

        /// <summary>
        /// Method that makes the ant put down their food. By Changing booleans.
        /// The "antID" is which ant in the list, the method applies to.
        /// </summary>
        /// <param name="antID"></param>
        private void PutDownFood(int antID)
        {

            antList[antID].HasFood = false;

            antList[antID].KnowsNestLocation = true;

            // For early testing
            // antList[antID].Approach(foodObject);

        }

        /// <summary>
        /// Method that makes the ant put down their stolen food. By Changing booleans.
        /// The "robberAntID" is which ant in the list, the method applies to.
        /// </summary>
        /// <param name="robberAntID"></param>
        private void PutDownStolenFood(int robberAntID)
        {
            robberAntList[robberAntID].HasFood = false;

            robberAntList[robberAntID].KnowsNestLocation = true;
        }

        /// <summary>
        /// Method that allows a robber ant to steal food from an ant.
        /// </summary>
        /// <param name="robberAntID"></param>
        /// <param name="antID"></param>
        private void StealFood(int robberAntID, int antID)
        {
            // Receives the food
            robberAntList[robberAntID].HasFood = true;

            // Remembers the food location
            robberAntList[robberAntID].KnowsFoodLocation = true;

            // Ant loses food
            antList[antID].HasFood = false;

            // Sets a position for the food location
            robberAntList[robberAntID].FoodPosX = (int)antList[antID].AgentPosition.X;
            robberAntList[robberAntID].FoodPosY = (int)antList[antID].AgentPosition.Y;
            

        }

        /// <summary>
        /// Method that will erase the food pile. By removing food item from
        /// the list, and making it null.
        /// </summary>
        /// <param name="antID"></param>
        /// <param name="foodID"></param>
        private void DestroyFood(int antID, int foodID)
        {

            // Destroy food object 
            foodList.RemoveAt(foodID);

            // Inserts null value so there is something in value foodID
            foodList.Insert(foodID, null);

            
            // Makes all the ants forget the food location of SpecificFood
            for (int i = 0; i < antList.Count; i++)
            {

                if (antList[i].SpecificFood == foodID)
                {

                    // Ants forget food
                    antList[antID].KnowsFoodLocation = false;

                    // Increases the specific food integer
                    antList[antID].SpecificFood++;

                }
            }
            

        }


        /// <summary>
        /// Method that allows the ants to communicate between each other.
        /// Depending on the situation, certain booleans change.
        /// </summary>
        /// <param name="antID"></param>
        /// <param name="subAnt"></param>
        private void AskForDirections(int antID, int subAnt)
        {

            // Depending on what the ant knows
            if (antList[antID].KnowsFoodLocation == true && antList[antID].KnowsNestLocation == false)
            {
                antList[subAnt].KnowsFoodLocation = true;

                antList[subAnt].SpecificFood = antList[antID].SpecificFood;
            }
            else if (antList[antID].KnowsFoodLocation == false && antList[antID].KnowsNestLocation == true)
            {
                antList[subAnt].KnowsNestLocation = true;

                antList[subAnt].SpecificNest = antList[antID].SpecificNest;
            }
            else if (antList[antID].KnowsFoodLocation == true && antList[antID].KnowsNestLocation == true)
            {
                antList[subAnt].KnowsFoodLocation = true;
                antList[subAnt].KnowsNestLocation = true;

                antList[subAnt].SpecificFood = antList[antID].SpecificFood;
                antList[subAnt].SpecificNest = antList[antID].SpecificNest;
            }

            
        }


        /// <summary>
        /// Method that allows the robber ants to communicate between each other.
        /// Depending on the situation, certain booleans change.
        /// </summary>
        /// <param name="robberAntID"></param>
        /// <param name="subAnt"></param>
        private void AskForTheftDirections(int robberAntID, int subAnt)
        {
            // Depending on what robber ant knows
            if (robberAntList[robberAntID].KnowsFoodLocation == true && robberAntList[robberAntID].KnowsNestLocation == false)
            {
                robberAntList[subAnt].KnowsFoodLocation = true;


                robberAntList[subAnt].FoodPosX = robberAntList[robberAntID].FoodPosX;
                robberAntList[subAnt].FoodPosY = robberAntList[robberAntID].FoodPosY;
            }
            else if (robberAntList[robberAntID].KnowsFoodLocation == false && robberAntList[robberAntID].KnowsNestLocation == true)
            {
                robberAntList[subAnt].KnowsNestLocation = true;
            }
            else if (robberAntList[robberAntID].KnowsFoodLocation == true && robberAntList[robberAntID].KnowsNestLocation == true)
            {
                robberAntList[subAnt].KnowsFoodLocation = true;
                robberAntList[subAnt].KnowsNestLocation = true;

                robberAntList[subAnt].FoodPosX = robberAntList[robberAntID].FoodPosX;
                robberAntList[subAnt].FoodPosY = robberAntList[robberAntID].FoodPosY;
            }
        }


        /// <summary>
        /// Graphics to draw the ants and robber ants
        /// </summary>
        private void DrawAgents()
        {

            // using FillRectangle to draw the agents
            // so declare variables to draw with
            float agentXPosition;
            float agentYPosition;

            // some arbitary size to draw the Ant
            float antSize;

            antSize = 3.0f;

            Brush solidBrush;

            // get the graphics context of the panel
            using (Graphics g = drawingPanel.CreateGraphics())
            {   

                for (int i = 0; i < antList.Count; i++)
                {
                    

                    // Get the ant agent position
                    agentXPosition = (float)antList[i].AgentPosition.X;
                    agentYPosition = (float)antList[i].AgentPosition.Y;

                    // Create a brush
                    solidBrush = new SolidBrush(Color.Red);

                    // Draw the ant
                    g.FillEllipse(solidBrush, agentXPosition, agentYPosition, antSize, antSize);

                    // Dispose of resources
                    solidBrush.Dispose();


                }

                // Draws the robber ants
                for (int r = 0; r < robberAntList.Count; r++)
                {
                    // Get the robber ant agent position
                    agentXPosition = (float)robberAntList[r].AgentPosition.X;
                    agentYPosition = (float)robberAntList[r].AgentPosition.Y;

                    // Create a brush
                    solidBrush = new SolidBrush(Color.Brown);

                    // Draw the ant
                    g.FillEllipse(solidBrush, agentXPosition, agentYPosition, antSize, antSize);

                    // Dispose of resources
                    solidBrush.Dispose();
                }



            }

        }

        /// <summary>
        /// Draws the ants and any stationary objects using double buffering
        /// </summary>
        private void DrawAgentsDoubleBuffering()
        {

            // using FillRectangle to draw the agents
            // so declare variables to draw with
            float agentXPosition;
            float agentYPosition;

            // some arbitary size to draw the Ant
            float antSize;

            antSize = 3.0f;

            Brush solidBrush;

            // get the graphics context of the background image
            using (Graphics backgroundGraphics =  Graphics.FromImage(backgroundImage))
            {

                // For the background colour. COMMENT OUT FOR A CREEPY EFFECT
                backgroundGraphics.Clear(Color.Silver);

                // change colour of brush
                solidBrush = new SolidBrush(Color.Blue);
                

                // Loop for drawing the ants in the list
                for (int i = 0; i < antList.Count; i++)
                {
                    

                    // Gets the agent position
                    agentXPosition = (float)antList[i].AgentPosition.X;
                    agentYPosition = (float)antList[i].AgentPosition.Y;

                    /* 
                    // Early testing
                    if (antList[i].HasFood == true)
                    {
                        solidBrush = new SolidBrush(Color.Red);
                    }
                    else if (antList[i].KnowsNestLocation == true)
                    {
                        solidBrush = new SolidBrush(Color.Blue);
                    }
                    else
                    {
                        solidBrush = new SolidBrush(Color.Black);

                    }
                    */

                    // Creates a brush
                    solidBrush = new SolidBrush(Color.Black);

                    // Draws the ants on the backgroundImage
                    backgroundGraphics.FillEllipse(solidBrush, agentXPosition, agentYPosition, antSize, antSize);

                    if (antList[i].HasFood == true)
                    {
                        // Create new brush
                        solidBrush = new SolidBrush(Color.White);

                        // Draw the food
                        backgroundGraphics.FillEllipse(solidBrush, agentXPosition - (float)0.5, agentYPosition - (float)0.5, antSize / 1.2f, antSize / 1.2f);

                    }
                    
                    
                }

                // Loop for drawing the food in the list
                for (int f = 0; f < foodList.Count; f++)
                {
                    if (foodList.ElementAt(f) != null)
                    {
                        // Adding new brush
                        solidBrush = new SolidBrush(Color.White);

                        // Drawing food
                        backgroundGraphics.FillEllipse(solidBrush, (float)foodList[f].FoodPosition.X - 10, (float)foodList[f].FoodPosition.Y - 10, (float)foodList[f].FoodSize, (float)foodList[f].FoodSize);

                    }
                    

                }

                // Loop for drawing the nest in the list
                for (int n = 0; n < nestList.Count; n++)
                {

                    // Adding new brush
                    solidBrush = new SolidBrush(Color.Black);

                    // Drawing nest
                    backgroundGraphics.FillEllipse(solidBrush, (float)nestList[n].NestPosition.X - 10, (float)nestList[n].NestPosition.Y - 10, nestList[n].NestSize, nestList[n].NestSize);
                }


                // Draws the robber ants double buffering
                for (int r = 0; r < robberAntList.Count; r++)
                {
                    // Gets the agent position
                    agentXPosition = (float)robberAntList[r].AgentPosition.X;
                    agentYPosition = (float)robberAntList[r].AgentPosition.Y;


                    /*
                    // Early testing
                    if (robberAntList[r].HasFood == true)
                    {
                        solidBrush = new SolidBrush(Color.Gold);
                    }
                    else if (robberAntList[r].KnowsNestLocation == true)
                    {
                        solidBrush = new SolidBrush(Color.Green);
                    }
                    else
                    {
                        solidBrush = new SolidBrush(Color.Brown);

                    }
                    */

                    // Creates a brush
                    solidBrush = new SolidBrush(Color.Brown);
                    
                    // Draws the ants on the backgroundImage
                    backgroundGraphics.FillEllipse(solidBrush, agentXPosition, agentYPosition, antSize, antSize);

                    if (robberAntList[r].HasFood == true)
                    {
                        // Create new brush
                        solidBrush = new SolidBrush(Color.White);

                        // Draw the food
                        backgroundGraphics.FillEllipse(solidBrush, agentXPosition - (float)0.5, agentYPosition - (float)0.5, antSize / 1.2f, antSize / 1.2f);

                    }

                    
                }

                
                // Loop to draw the robber nests
                for (int q = 0; q < robberNestList.Count; q++)
                {
                    solidBrush = new SolidBrush(Color.Green);
                    // Drawing nest
                    backgroundGraphics.FillEllipse(solidBrush, (float)robberNestList[q].NestPosition.X - 10, (float)robberNestList[q].NestPosition.Y - 10, robberNestList[q].NestSize, robberNestList[q].NestSize);

                }


            }

            // now draw the image on the panel
            using (Graphics g = drawingPanel.CreateGraphics())
            {
                g.DrawImage(backgroundImage, 0, 0, drawingPanel.Width, drawingPanel.Height);
            }

                // dispose of resources
                solidBrush.Dispose();
        }


        private void stopButton_Click(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            timer.Start();
        }

        
        /// <summary>
        /// Method for when the user clicks on the drawing panel.
        /// It will either add a new food item, or nest item to their list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawingPanel_MouseClick(object sender, MouseEventArgs e)
        {

            // Gets a point from mouse position
            Point mousePoint = e.Location;

            // For if the Nest radio button is checked
            if (nestRadioButton.Checked == true)
            {

                // Adds new nest to list
                nestList.Add(new Nest(new SOFT152Vector(mousePoint.X, mousePoint.Y)));
            }
            else if (foodRadioButton.Checked == true)
            {
                
                // Adds new food to list
                foodList.Add(new Food(new SOFT152Vector(mousePoint.X, mousePoint.Y)));

            }
            else if (robberNestRadioButton.Checked == true)
            {
                
                // Adds new roober nest to list
                robberNestList.Add(new RobberNest(new SOFT152Vector(mousePoint.X, mousePoint.Y)));
            }
            
        }

        
        /// <summary>
        /// Reads user input and calls CreateAnts() with a specific quantity.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createAntsButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Converts input textBox to integer
                int numberOfAnts = Convert.ToInt32(inputAntsTextBox.Text);

                // Runs method for the amount of ants to create
                CreateAnts(numberOfAnts);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Input is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

            // Starts the timer
            timer.Start();
        }

        /// <summary>
        /// Reads user input and calls CreateRobberAnts() with a specific quanitity.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createRobberAntsButton_Click(object sender, EventArgs e)
        {

            try
            {
                // Converts input textBox to integer
                int numberOfRobberAnts = Convert.ToInt32(inputRobberAntsTextBox.Text);

                // Declares the robber nest vector to middle of drawing panel
                //robberNestVector = new SOFT152Vector(drawingPanel.Width / 2, drawingPanel.Height / 2);


                // Runs method for the amount of ants to create
                CreateRobberAnts(numberOfRobberAnts);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Input is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

            // Starts the timer
            timer.Start();
        }
    }
}
