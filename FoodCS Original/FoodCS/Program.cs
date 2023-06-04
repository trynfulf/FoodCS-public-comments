
//Skeleton Program code for the AQA A Level Paper 1 Summer 2020 examination
//this code should be used in conjunction with the Preliminary Material
//written by the AQA Programmer Team
//developed in the Visual Studio Community Edition programming environment
// this comment has appeared in some place within github. 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


/*
The following program is a simulation of restaurants and their business success over a length of time. The premise of the simulation is that there is a settlement with houses and restaurant with outlets. 
On any particular day a household has a chance to eat out at one of the outlets in the settlement.  This program tracks the profits/losses and delivery costs of those companies. s
  
  
  
 
Throughout the whole program, there is no negative number validation. 
*/



namespace FoodCS
{

    class Household
    {
        /* The household class is used to store information on individual houses pretty basic very struct like in nature. It is also used to calculate the changes that a particular restaurant may have when the simulation 
           is advanced. 
        
           Note: The location of each household is stored within its own particular instance and the settlement itself. This means that it is possible to have multiple households with the same coordinates 
                 in a particular generated settlement. */

        //rnd is static so that the same instance of rnd is used in every instance of household. 
        private static Random rnd = new Random(); 
        protected double chanceEatOutPerDay;
        protected int xCoord, yCoord, ID;
        protected static int nextID = 1; //nextID is static so that it is constant across all instances. Allows for a robust and efficient method to give each instance a unique ID. 
                                         //I like this element of the code. 
        
       
        //Constructor - This is ran when a new household is made. 
        public Household(int x, int y)
        {
            xCoord = x;
            yCoord = y;
            // On a given day each household can eat out. This is the probabiltiy of a particular household eating out. Each household can eat out only once and then the user must advance the simulation with key 4. 
            // An improvement may be to add a weighting to the probability so each household is less likely to eat out. This would potentially make the simulation more realistic. 
            chanceEatOutPerDay = rnd.NextDouble();
            
            //Assigns a unique ID to each household. This can be done with the simple snippet below due to the ID being a static attribute. This means that when a new household is made it has the ID of the previous instance
            //+1. This is because a static class has the same attributes as the previous instances. Ie. its static (doesn't change). 
            ID = nextID;
            nextID++; 
        }
        
        //Returns a string which contains the coordinates of the house along with it's probability of eating out. Formatting could be more consistent by using PadRight(). - M
        public string GetDetails()
        {
            string details;
            details = ID.ToString() + "     Coordinates: (" + xCoord.ToString() + ", " + yCoord.ToString() + ")     Eat out probability: " + chanceEatOutPerDay.ToString();
            return details;
        }
        
        //Returns the chance that the given household eats out each day. A description of this attribute is given above. 
        public double GetChanceEatOut()
        {
            return chanceEatOutPerDay;
        }
        
        //Returns the X Coordinate of the household in the settlement. Multiple households can have the same X-coordinate. 
        public int GetX()
        {
            return xCoord;
        }
        
        //Returns the Y Coordinate of the household in the settlement. Multiple households can have the same Y-coordinate. 
        public int GetY()
        {
            return yCoord;
        }
    }

    class Settlement
    {
        /* This class acts is extremely important as it acts as the place where all of the data on households are stored. 
           It also is where the household in the settlement are generated and also if a particular household will eat out on a particular day. 
           
           Note: This does not store any information about the companies, it ONLY contains information on the householdw ithin the settlement. 
         */

        private static Random rnd = new Random();
        protected int startNoOfHouseholds, xSize, ySize;
        // This list stores all of household within a particular settlement. Essentially this is a list of objects of type <Household> which is why the eating out is calculated within this class. 
        protected List<Household> households = new List<Household>(); 
        
        //Constructor
        public Settlement()
        {
            //These are the default parameters that are used within the simulation. 
            xSize = 1000;
            ySize = 1000;
            startNoOfHouseholds = 250;
            CreateHouseholds();
        }

        public int GetNumberOfHouseholds()
        {
            //Returns the number of households within a particular settlement. 
            return households.Count;
        }
        
        public int GetXSize()
        {
            //Returns the width of the settlement. ie. the largest X value possible of a particular household. 
            return xSize;
        }
        
        public int GetYSize()
        {
            //Returns the heigt of the settlement. ie. the largest Y value possible of a particular household. 
            return ySize;
        }

        public void GetRandomLocation(ref int x, ref int y)
        {
            /*
              The parameters of this method are passed by reference. This means that the data value is edited directly and a copy of it is not made. This is advantageous because. It allows for more return values. 
              This is because the values can be edited within the method and then a return can then also be made saying whether the method/process was successful or not. It is also more convenient because the values 
              can be edited directly and you dont have to faff with temporary variables etc. 
            
              One problem with this method is that it is possible for repeat coordinates to be generated. This means that multiple households can have the same XY value. This is not a huge problem but may be considered 
              undesirable. 
             */
            x = Convert.ToInt32(rnd.NextDouble() * xSize);
            y = Convert.ToInt32(rnd.NextDouble() * ySize);
        }
        
        public void CreateHouseholds()
        {
            //Calls the AddHousehold() method == to the start number of households. Not much to say here. This function is called at the beginning of the simulation. 
            for (int count = 0; count < startNoOfHouseholds; count++)
            {
                AddHousehold();
            }
        }

        public void AddHousehold()
        {
            // Adds a new instance of the household class to the settlement. In practice, just adding to the households list. 
            // The coordinates are passed by reference to edit them directly. This is more convenient and nicer to work with. 
            int x = 0, y = 0; 
            GetRandomLocation(ref x, ref y);
            Household temp = new Household(x, y);
            households.Add(temp);
        }

        public void DisplayHouseholds()
        {
            // The main problem with this method is that it is extremely verbose. This is because it prints all of the household information in one solid black that it misaligned and untidy. 
            // This makes the information much harder to read and understand which makes the simulation unecessarily difficult to use. 
            // 
            // To improve this, paging would have to implemented. This would allow the user to see the household information in smaller chunks at a time making it easier to understand. Increments of 20 for example.

            Console.WriteLine("\n**********************************");
            Console.WriteLine("*** Details of all households: ***");
            Console.WriteLine("**********************************\n");
            foreach (var h in households)
            {
                Console.WriteLine(h.GetDetails());
            }
            Console.WriteLine();
        }

        public bool FindOutIfHouseholdEatsOut(int householdNo, ref int x, ref int y)
        {
            // The chance of a household eating out is defined by the random number generated below. This is a good way to judge whether a household eats out because in real life the chance is a variable value and not
            // every one has the same chance of eating out on a particular day. Therefore, I find this a very realistic part of the simulation
            double eatOutRNo = rnd.NextDouble();
            // XY coords, no purpose, probably used in some questions later on. 
            x = households[householdNo].GetX();
            y = households[householdNo].GetY();
            if (eatOutRNo < households[householdNo].GetChanceEatOut())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    class LargeSettlement : Settlement
    {

        /*
          The large settlement class below is just a poor excuse for inheritance. The main difference between this class and the settlement class above is the
          change of the (extraXSize, extraYSize and extraHouseholds) parameters. These parameters simply add onto the original xSize, ySize and households parameters. 

          We can tell that the class is a child class due to the base() keyword. This is the place where you have to put in the inherited attributes. Like the super() keyword in Python. 

          This class is redundant because the extras can simply be given a default value in the settlement class above and added on in the constructor. This will reduce 
          the amount of code in the file which the program easier to read, understand and maintain.                                          
        */
        public LargeSettlement(int extraXSize, int extraYSize, int extraHouseholds)
            : base()
        {
            xSize += extraXSize;
            ySize += extraYSize;
            startNoOfHouseholds += extraHouseholds;
            for (int count = 1; count < extraHouseholds + 1; count++)
            {
                AddHousehold();
            }
        }
    }

    class Outlet
    {
        /* An outlet is a place in the simulation where a household can eat out and the company can make money. This done by recording the visits to a company and the costs of that outlet.  
           The outlets also have XY coordinate to allow for them to be accounted with delivery costs and movement etc. 
         
           Note: the ID's of outlets are not printed properly. This is because an array is zero-indexed and not one-indexed. This makes it unnecessarily difficult for the user to change/modify
                 the outlets because the user cannot easily access the ID of the outlet that they would like to change.
         */


        private static Random rnd = new Random();
        // The visitsToday attribute is used to keep track of the number of households that have eaten out on a particular day.   
        protected int visitsToday, xCoord, yCoord, capacity, maxCapacity;
        protected double dailyCosts;

        public Outlet(int xCoord, int yCoord, int maxCapacityBase)
        {
            // XY coordinates just like the households. This allows for delivery costs and fuel costs to be calculated based off of distance. 
            this.xCoord = xCoord;
            this.yCoord = yCoord;
            // This is the running value of how many people that eat out at a particular outlet. This means that all outlets of a company have the same base number of households that eat their but some 
            // outlets can take more customers in a given day.
            capacity = Convert.ToInt32(maxCapacityBase * 0.6);
            // The maximum number of households that can at the outlet in one day. 
            maxCapacity = maxCapacityBase + Convert.ToInt32(rnd.NextDouble() * 50) - Convert.ToInt32(rnd.NextDouble() * 50);
            // This is the amount of money that a particular outlet must pay in order to stay open. This would have to be paid even if the outlet served 0 households in one day. 
            dailyCosts = maxCapacityBase * 0.2 + capacity * 0.5 + 100;
            // Used to set the initial value of visitsToday to 0.  
            NewDay();
        }
        
        public int GetCapacity()
        {
            // returns the number of households that eat out at that particular outlet. 
            return capacity;
        }

        public int GetX()
        {
            return xCoord;
        }

        public int GetY()
        {
            return yCoord;
        }

        public void AlterDailyCost(double amount)
        {
            // allows the user to change the amount of money an outlet has to pay to stay open. This is equivalent of a setter. 
            dailyCosts += amount;
        }

        public int AlterCapacity(int change)
        {
            // This method allows outlets to recieve more or less customers on a particular day in the simulation. 
            // To compensate for the increased number of customers, the daily costs are also calculated to be respective. 
            // 
            // Note: The method returns the change. essentially meaning that the method returns the input it has been given. 
            //       This is not useful in any way and so the method should be of type <void> to clean up the method and make it
            //       more simple. 

            int oldCapacity = capacity;
            capacity += change;
            if (capacity > maxCapacity)
            {
                capacity = maxCapacity;
                return maxCapacity - oldCapacity;
            }
            else if (capacity < 0)
            {
                capacity = 0;
            }
            dailyCosts = maxCapacity * 0.2 + capacity * 0.5 + 100;
            return change;
        }

        public void IncrementVisits()
        {
            // When a household eats out at a particular outlet this method is run to show that the outlet has been visited. 
            visitsToday++;
        }

        public void NewDay()
        {
            // resets the number of households that have visited a particular outlet on the day of the simulation. 
            visitsToday = 0;
        }
        public double CalculateDailyProfitLoss(double avgCostPerMeal, double avgPricePerMeal)
        {
            // This method returns the amount of profit or loss of a particular outlet. 
            // This is calculated by finding the average money gained for each meal. -> (avgPricePerMeal - avgCostPerMeal)
            // And then multiplying it by the number of meals to work out the total average money made.
            // You must also subtract the money required to keeping the outlet open. 
            return (avgPricePerMeal - avgCostPerMeal) * visitsToday - dailyCosts;
        }
        
        public string GetDetails()
        {
            // This method returns useful information regarding the individual outlet. This is similar to the households shown earlier. 
            // One improvement would be to add padding to the string. This would make the data much tidier and hence, easier to read. 
            string details = "";
            details = "Coordinates: (" + xCoord.ToString() + ", " + yCoord.ToString() + ")     Capacity: " + capacity.ToString() + "      Maximum Capacity: ";
            details += maxCapacity.ToString() + "      Daily Costs: " + dailyCosts.ToString() + "      Visits today: " + visitsToday.ToString();
            return details;
        }
    }
    
    class Company
    {
        // It is important to know that companies cannot go bankrupt. This is because -> ? 

        private static Random rnd = new Random();
        protected string name, category;
        protected double balance, reputationScore, avgCostPerMeal, avgPricePerMeal, dailyCosts, familyOutletCost, fastFoodOutletCost, namedChefOutletCost, fuelCostPerUnit, baseCostOfDelivery;
        // This list stores all of the outlets that are owned by a particular company. It stores the instances themselves and not just
        // ID's or names. 
        protected List<Outlet> outlets = new List<Outlet>();
        // The different types of company. 
        protected int familyFoodOutletCapacity, fastFoodOutletCapacity, namedChefOutletCapacity;
        public Company(string name, string category, double balance, int x, int y, double fuelCostPerUnit, double baseCostOfDelivery)
        {
            // constants based off of the type of company. 
            familyOutletCost = 1000;
            fastFoodOutletCost = 2000;
            namedChefOutletCost = 15000;
            familyFoodOutletCapacity = 150;
            fastFoodOutletCapacity = 200;
            namedChefOutletCapacity = 50;
            // Setting the company details. Also applying data based off of the companies type. Seen with the selection below. 
            this.name = name;
            this.category = category;
            this.balance = balance;
            this.fuelCostPerUnit = fuelCostPerUnit;
            this.baseCostOfDelivery = baseCostOfDelivery;
            reputationScore = 100;
            dailyCosts = 100;
            if (category == "fast food")
            {
                avgCostPerMeal = 5;
                avgPricePerMeal = 10;
                reputationScore += rnd.NextDouble() * 10 - 8;
            }
            else if (category == "family")
            {
                avgCostPerMeal = 12;
                avgPricePerMeal = 14;
                reputationScore += rnd.NextDouble() * 30 - 5;
            }
            else
            {
                avgCostPerMeal = 20;
                avgPricePerMeal = 40;
                reputationScore += rnd.NextDouble() * 50;
            }
            OpenOutlet(x, y);
        }

        public string GetName()
        {
            // Gives the name of the specific company
            return name;
        }

        public int GetNumberOfOutlets()
        {
            // Gives the number of outlets within the settlement that are part of this company. 
            return outlets.Count;
        }

        public double GetReputationScore()
        {
            // Returns the reputation of the company. 
            // The reputation is the cahnce that a household will eat at that company. If the companies reputation is low then the households will not eat at that companies outlets. 
            // The repuation of a company comes into full effect with the ProcessDayEnd() and ProcessReputationEvents() methods in the simulation class.
            return reputationScore;
        }

        // Note: All of the setters below have very little validation such as negative number validation, range checks etc. This does increase the likelihood of the simulation/program breaking. 

        public void AlterDailyCosts(double change)
        {
            // Allows the user to change the costs that a company has with all of its outlets. This is deducted from the companies balance after the day ends.
            dailyCosts += change;
        }

        public void AlterAvgCostPerMeal(double change)
        {
            // Allows the user to change the price of the food and hence, change the amount of profit and costs that they have. 
            // As alluded to, this features when at the end of the day when the company has to calculate the profit/losses on a day. 
            avgCostPerMeal += change;
        }

        public void AlterFuelCostPerUnit(double change)
        {
            // This allows the user to change the cost of the fuel from each of its outlets to eachother. 
            // This is used when the company is calculating its costs at the end of the day. This is because the the distance between the outlets and the fuel cost is used to make up part of the the total company cost. 
            fuelCostPerUnit += change;
            if (fuelCostPerUnit < 0)
            {
                fuelCostPerUnit = 0; 
            }
        }

        public void AlterReputation(double change)
        {
            // Allows the user to change the particular reputation of the company. This is useful to see the effects of more/less households eating out at a particular restaurant. 
            reputationScore += change;
        }

        public void NewDay()
        {
            // This method resets the visitsToday of all of the outlets of the same company. 
            foreach (var o in outlets)
            {
                o.NewDay();
            }
        }

        public void AddVisitToNearestOutlet(int x, int y)
        {
            // This method allows the company to loop through the households and find the closest one to them. The XY parameters act as the location of the household within the settlement. 
            int nearestOutlet = 0;
            double nearestOutletDistance, currentDistance;
            // Finding the nearest distance from the specified point to the first outlet in the company. This is to establish a possible distance from the outlet to the specific location but not necessarily the shortest. 
            nearestOutletDistance = Math.Sqrt((Math.Pow(outlets[0].GetX() - x, 2)) + (Math.Pow(outlets[0].GetY() - y, 2)));
            for (int current = 1; current < outlets.Count; current++)
            {
                // Looping through all of the outlets in the company and calculating their distance from the defined point. This is to gauge whether the new outlet is actually closer to the point.  
                currentDistance = Math.Sqrt((Math.Pow(outlets[current].GetX() - x, 2)) + (Math.Pow(outlets[current].GetY() - y, 2)));
                // If the new calculated distance is less than the old distance then it must be closer and so that outlet is now the nearest. 
                // This information is stored in the selection below. 
                if (currentDistance < nearestOutletDistance)
                {
                    nearestOutletDistance = currentDistance;
                    nearestOutlet = current;
                }
            }
            // Because the nearest outlet has been found, that outlet can now recieve a visit. 
            outlets[nearestOutlet].IncrementVisits();
        }

        public string GetDetails()
        {
            // Gives important information on the company. Again the UI is verbose and challenging to use. Padding should be implemented to make the information easier to read and also tider on the screen. 
            // Note: When printing the outlet information the information is printed as if the array is one-indexed and not zero-indexed. This will make the UI more confusing for the user because the outlet ID displayed
            //       does not correspond to the list that stores the outlets. 
            string details = "";
            details += "Name: " + name + "\nType of business: " + category + "\n";
            details += "Current balance: " + balance.ToString() + "\nAverage cost per meal: " + avgCostPerMeal.ToString() + "\n";
            details += "Average price per meal: " + avgPricePerMeal.ToString() + "\nDaily costs: " + dailyCosts.ToString() + "\n";
            details += "Delivery costs: " + CalculateDeliveryCost().ToString() + "\nReputation: " + reputationScore.ToString() + "\n\n";
            details += "Number of outlets: " + outlets.Count.ToString() + "\nOutlets\n";
            for (int current = 1; current < outlets.Count + 1; current++)
            {
                details += current + ". " + outlets[current - 1].GetDetails() + "\n";
            }
            return details;
        }

        public string ProcessDayEnd()
        {
            // This method is run when the day has ended. 2 different things are done with this method. 
            // These are changing the attributes of the company such as deliveryCosts etc and then also displaying these changes through strings. 
            // The UI is better than most other elements of the program but I do feel some improvements could be made. 

            string details = "";
            // Acts as the total profit/loss of the company.
            double profitLossFromOutlets = 0;
            // Acts as the profit/loss of the individual outlet.
            double profitLossFromThisOutlet = 0;
            double deliveryCosts;
            if (outlets.Count > 1)
            {
                // Taking into account however many outlets a particular company may have. 
                deliveryCosts = baseCostOfDelivery + CalculateDeliveryCost();
            }
            else
            { 
                // The else clause is used because there may be only 1 outlet for company. This would mean that there are no delivery costs as an outlet cannot deliver to itself. 
                deliveryCosts = baseCostOfDelivery;
            }
            details += "Daily costs for company: " + dailyCosts.ToString() + "\nCost for delivering produce to outlets: " + deliveryCosts.ToString() + "\n";
            for (int current = 0; current < outlets.Count; current++)
            {
                // Calculating the individual profit/loss of a specific outlet within the company. 
                profitLossFromThisOutlet = outlets[current].CalculateDailyProfitLoss(avgCostPerMeal, avgPricePerMeal);
                details += "Outlet " + (current + 1) + " profit/loss: " + profitLossFromThisOutlet.ToString() + "\n";
                profitLossFromOutlets += profitLossFromThisOutlet;
            }
            details += "Previous balance for company: " + balance.ToString() + "\n";
            // Changing the total balance of the company based off of the distance travelled for delivery and also the daily running costs. 
            balance += profitLossFromOutlets - dailyCosts - deliveryCosts;
            details += "New balance for company: " + balance.ToString();
            return details;
        }

        public bool CloseOutlet(int ID)
        {
            // Because the display is shown as being one-indexed but the array storing the companies outlets being zero-indexed this method will eventually return an out_of_range error. 
            // This is because the if there is only outlet present, the user will enter "1" from the display but this index does not exist within the program. 
            // This means that the user must enter one less than the ID of the outlet they would like to remove. 
            bool closeCompany = false;
            outlets.RemoveAt(ID);
            // If a company has no outlets then it doesn't exist and so it shuts down. 
            if (outlets.Count == 0)
            {
                closeCompany = true;
            }
            return closeCompany;
        }

        public void ExpandOutlet(int ID)
        {
            // This method also suffers with the indexing issue. Becuase the user must input one less than their desired outlet ID. 
            // Note: There is no validation of the change variable, this could create problems within the simulation due to attributes being engative, out_of_range etc. 
            int change, result;
            Console.Write("Enter amount you would like to expand the capacity by: ");
            change = Convert.ToInt32(Console.ReadLine());
            result = outlets[ID].AlterCapacity(change);
            if (result == change)
            {
                Console.WriteLine("Capacity adjusted.");
            }
            else
            {
                Console.WriteLine("Only some of that capacity added, outlet now at maximum capacity.");
            }
        }

        public void OpenOutlet(int x, int y)
        {
            // This method is pretty self explanatory. 
            int capacity;
            if (category == "fast food")
            {
                balance -= fastFoodOutletCost;
                capacity = fastFoodOutletCapacity;
            }
            else if (category == "family")
            {
                balance -= familyOutletCost;
                capacity = familyFoodOutletCapacity;
            }
            else
            {
                balance -= namedChefOutletCost;
                capacity = namedChefOutletCapacity;
            }
            Outlet newOutlet = new Outlet(x, y, capacity);
            outlets.Add(newOutlet);
        }
        
        public List<int> GetListOfOutlets()
        {
            // This method returns a list of all of the outlet instances within a company. This is used to clacualte and modify specific outlets later on in the program. 
            // This method is odd because it returns a list of outlets that is created within the method. Even though this attribute already exists. You can simply just return the list that is already within the company and 
            // not create a new unecessary list. This wastes memory and makes the program less efficient and more unusable. 

            List<int> temp = new List<int>();
            for (int current = 0; current < outlets.Count; current++)
            {
                temp.Add(current);
            }
            return temp;
        }
        
        private double GetDistanceBetweenTwoOutlets(int outlet1, int outlet2)
        {
            // This method calculates the distance between 2 defined outlets by using Pythagoras' theorem. This is a well written piece of code that is used only to calculate the delivery cost between 2 outlets. 
            // It is not used anywhere else. 
            // 
            // One improvement would be to make the code more explicit/readable. This is to make it easier to understand. Aside from this, I would not make any changes. 
            return Math.Sqrt((Math.Pow(outlets[outlet1].GetX() - outlets[outlet2].GetX(), 2)) + (Math.Pow(outlets[outlet1].GetY() - outlets[outlet2].GetY(), 2)));
        }

        public double CalculateDeliveryCost()
        {
            // This method also uses a temporary list which as said previously is not advised. 
            // 
            // The delivery cost is calculated by summing the distance between all of the outlets and then multiplying by the fuel costs to find the total. 
            List<int> listOfOutlets = new List<int>(GetListOfOutlets());
            double totalDistance = 0;
            double totalCost = 0;
            for (int current = 0; current < listOfOutlets.Count - 1; current++)
            {
                // summing the distance between all of the outlets. The loop ensures all outlets are covered. 
                totalDistance += GetDistanceBetweenTwoOutlets(listOfOutlets[current], listOfOutlets[current + 1]);
            }
            // The fuel cost is indvidual for each company within the simulation. This is true when using the default companies. 
            // Otherwise, the fuel costs can be custom for each company. 
            totalCost = totalDistance * fuelCostPerUnit;
            return totalCost;
        }
    }
    
    class Simulation
    {
        private static Random rnd = new Random();
        // This is the instance of the settlement class, and so this is where the households are stored.
        protected Settlement simulationSettlement;
        // This stores the number of companies within the simulation.
        protected int noOfCompanies;
        protected double fuelCostPerUnit, baseCostForDelivery;
        // This is the list that stores the company objects.
        protected List<Company> companies = new List<Company>();
        private int daysPassed = 0; 

        public Simulation()
        {
            // This constructor sets up the starting conditions for the simulation. Such as the fuel cost and delivery cost constants. That are
            // used throughout the simulation. 
            fuelCostPerUnit = 0.0098;
            baseCostForDelivery = 100;
            string choice;
            Console.Write("Enter L for a large settlement, anything else for a normal size settlement: ");
            choice = Console.ReadLine();
            if (choice == "L")
            {
                int extraX, extraY, extraHouseholds;
                Console.Write("Enter additional amount to add to X size of settlement: ");
                extraX = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter additional amount to add to Y size of settlement: ");
                extraY = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter additional number of households to add to settlement: ");
                extraHouseholds = Convert.ToInt32(Console.ReadLine()); 
                // Although simulationSettlement is of type Settlement, this encompasses all inherited classes. 
                // This is why the simulationSettlement can be assigned a largeSettlement without needing a new
                // type declaration. 

                simulationSettlement = new LargeSettlement(extraX, extraY, extraHouseholds);
            }
            else
            {
                simulationSettlement = new Settlement();
            }
            // This is where the companies can be selected. The default companies are for the exam boards.
            Console.Write("Enter D for default companies, anything else to add your own start companies: ");
            choice = Console.ReadLine();
            if (choice == "D")
            {
                noOfCompanies = 3;
                Company company1 = new Company("AQA Burgers", "fast food", 100000, 200, 203, fuelCostPerUnit, baseCostForDelivery);
                companies.Add(company1);
                // These hard coded values are necessary not required. 
                companies[0].OpenOutlet(300, 987);
                companies[0].OpenOutlet(500, 500);
                companies[0].OpenOutlet(305, 303);
                companies[0].OpenOutlet(874, 456);
                companies[0].OpenOutlet(23, 408);
                companies[0].OpenOutlet(412, 318);
                Company company2 = new Company("Ben Thor Cuisine", "named chef", 100400, 390, 800, fuelCostPerUnit, baseCostForDelivery);
                companies.Add(company2);
                Company company3 = new Company("Paltry Poultry", "fast food", 25000, 800, 390, fuelCostPerUnit, baseCostForDelivery);
                companies.Add(company3);
                companies[2].OpenOutlet(400, 390);
                companies[2].OpenOutlet(820, 370);
                companies[2].OpenOutlet(800, 600);
            }
            else
            {
                // This is where the user can add their own custom companies to the simulation
                Console.Write("Enter number of companies that exist at start of simulation: ");
                noOfCompanies = Convert.ToInt32(Console.ReadLine());
                for (int count = 1; count < noOfCompanies + 1; count++)
                {
                    // This method call simply collects all of the information required to create a company. 
                    // The values collected are the one hard-coded above. 
                    // It is simply a large block of input/output. 
                    AddCompany();
                }
            }
        }

        public void DisplayMenu()
        {
            // Menu to allow the user to interact with the simulation
            Console.WriteLine("\n*********************************");
            Console.WriteLine("**********    MENU     **********");
            Console.WriteLine("*********************************");
            Console.WriteLine("1. Display details of households");
            Console.WriteLine("2. Display details of companies");
            Console.WriteLine("3. Modify company");
            Console.WriteLine("4. Add new company");
            Console.WriteLine("5. Delete company");
            Console.WriteLine("6. Advance to next day");
            Console.WriteLine("Q. Quit");
            Console.Write("\n Enter your choice: ");
        }

        private void DisplayCompaniesAtDayEnd()
        {
            // Outputs all of the information of the companies. Refer to previous comments for more information.
            string details;
            Console.WriteLine("\n**********************");
            Console.WriteLine("***** Companies: *****");
            Console.WriteLine("**********************\n");
            foreach (var c in companies)
            {
                Console.WriteLine(c.GetName());
                Console.WriteLine();
                details = c.ProcessDayEnd();
                Console.WriteLine(details + "\n");
            }
        }
        
        /*
         The following methods come under the Events category. These are things and changes that happen to the 
         conditions of the simulation. The way these occur is a random number in the DisplayEventsAtDayEnd() method.
         */

        private void ProcessAddHouseholdsEvent()
        {
            // Adds a random number of households to the simulation. This to give the impression that a settlement 
            // ultimately grows over a period of time. 
            //
            // Note: The number of households can exceed the settlement limit when using this method. 
            int NoOfNewHouseholds = rnd.Next(1, 5);
            for (int i = 1; i < NoOfNewHouseholds + 1; i++)
            {
                simulationSettlement.AddHousehold();
            }
            Console.WriteLine(NoOfNewHouseholds.ToString() + " new households have been added to the settlement.");
        }

        private void ProcessCostOfFuelChangeEvent()
        {
            // Changes the price of fuel in the simulation. This mainly impacts delvery costs of the companies.
            double fuelCostChange = rnd.Next(1, 10) / 10.0;
            int upOrDown = rnd.Next(0, 2);
            // Choosing a random company
            int companyNo = rnd.Next(0, companies.Count);
            if (upOrDown == 0)
            {
                Console.WriteLine("The cost of fuel has gone up by " + fuelCostChange.ToString() + " for " + companies[companyNo].GetName());
            }
            else
            {
                Console.WriteLine("The cost of fuel has gone down by " + fuelCostChange.ToString() + " for " + companies[companyNo].GetName());
                // Ensuring that the cost is being subtracted. 
                fuelCostChange *= -1;
            }
            // Applying the change. This is done at the end to make the code more concise. 
            companies[companyNo].AlterFuelCostPerUnit(fuelCostChange);
        }

        private void ProcessReputationChangeEvent()
        {
            // This method works in exactly the same way as the reputation event. 
            double reputationChange = rnd.Next(1, 10) / 10.0;
            int upOrDownOrScandal = rnd.Next(0, 3);
            int companyNo = rnd.Next(0, companies.Count);
            if (upOrDownOrScandal == 0)
            {
                Console.WriteLine("The reputation of " + companies[companyNo].GetName() + " has gone up by " + reputationChange.ToString());
            }
            else if (upOrDownOrScandal == 1)
            {
                Console.WriteLine("The reputation of " + companies[companyNo].GetName() + " has gone down by " + reputationChange.ToString());
                reputationChange *= -1;
            }
            else
            {
                reputationChange *= 10;
                Console.WriteLine("The company " + companies[companyNo].GetName() + " has had a scandal and so its reputation has gone done by " + reputationChange.ToString());
                reputationChange *= -1; 
            }
            companies[companyNo].AlterReputation(reputationChange);
        }

        private void ProcessCostChangeEvent()
        {
            // This method is responsible for changing and modifying the costs relating to food. 
            // It applies to either the average cost or the daily costs. 
            // 
            // Note: A change will always happen to the costs either up or down. This is not very realistic because on some occasions. 
            //       a company and its outlets will maintain some form of consistency. 

            double costToChange = rnd.Next(0, 2);
            // Essentially a random boolean -> 0 to 1 is the range ie. TRUE or FALSE
            int upOrDown = rnd.Next(0, 2);
            // Choosing which company the change will apply. 
            int companyNo = rnd.Next(0, companies.Count);
            double amountOfChange;
            // Daily costs are changing. 
            if (costToChange == 0)
            {
                amountOfChange = rnd.Next(1, 20) / 10.0;
                if (upOrDown == 0)
                {
                    Console.WriteLine("The daily costs for " + companies[companyNo].GetName() + " have gone up by " + amountOfChange.ToString());
                }
                else
                {
                    Console.WriteLine("The daily costs for " + companies[companyNo].GetName() + " have gone down by " + amountOfChange.ToString());
                    amountOfChange *= -1;
                }
                companies[companyNo].AlterDailyCosts(amountOfChange);
            }
            // Average costs of meals are changing. 
            else
            {
                amountOfChange = rnd.Next(1, 10) / 10.0;
                if (upOrDown == 0)
                {
                    Console.WriteLine("The average cost of a meal for " + companies[companyNo].GetName() + " has gone up by " + amountOfChange.ToString());
                }
                else
                {
                    Console.WriteLine("The average cost of a meal for " + companies[companyNo].GetName() + " has gone down by " + amountOfChange.ToString());
                    amountOfChange *= -1;
                }
                companies[companyNo].AlterAvgCostPerMeal(amountOfChange);
            }
        }

        private void DisplayEventsAtDayEnd()
        {
            // This method is the method that decides whether particular events will happen. 
            // Each event has its own random number that decides if each event will occur. 
            Console.WriteLine("\n***********************");
            Console.WriteLine("*****   Events:   *****");
            Console.WriteLine("***********************\n");
            double eventRanNo;
            eventRanNo = rnd.NextDouble();
            // Deciding if any events event take place. 
            if (eventRanNo < 0.25)
            {
                eventRanNo = rnd.NextDouble();
                if (eventRanNo < 0.25)
                {
                    ProcessAddHouseholdsEvent();
                }
                eventRanNo = rnd.NextDouble();
                if (eventRanNo < 0.5)
                {
                    ProcessCostOfFuelChangeEvent();
                }
                eventRanNo = rnd.NextDouble();
                if (eventRanNo < 0.5)
                {
                    ProcessReputationChangeEvent();
                }
                eventRanNo = rnd.NextDouble();
                if (eventRanNo >= 0.5)
                {
                    ProcessCostChangeEvent();
                }
            }
            else
            {
                Console.WriteLine("No events.");
            }
        }

        public void ProcessDayEnd()
        {
            double totalReputation = 0;
            List<double> reputations = new List<double>();
            int companyRNo, current, loopMax, x = 0, y = 0;   
            // Looping for all of the companies. 
            foreach (var c in companies)
            {
                // Clearing the visits of all companies and all of their respective outlets. 
                c.NewDay();
                // Calcualting the sum of all of the reputations. Also adding the cumulative reputation to the list.  
                totalReputation += c.GetReputationScore();
                reputations.Add(totalReputation);
            }
            loopMax = simulationSettlement.GetNumberOfHouseholds() - 1;
            for (int counter = 0; counter < loopMax + 1; counter++)
            {
                // Finding out if an individual household will eat out. 
                if (simulationSettlement.FindOutIfHouseholdEatsOut(counter, ref x, ref y))
                {
                    // generating a random number between 1  and the total reputation in the simulation. 
                    companyRNo = rnd.Next(1, Convert.ToInt32(totalReputation) + 1);
                    current = 0;
                    // Trying to determine which restaurant, the household will eat out at. 
                    while (current < reputations.Count)
                    {
                        if (companyRNo < reputations[current])
                        {
                            // Determining the closest outlet of that particular company and then the household will
                            // visit that outlet. 
                            companies[current].AddVisitToNearestOutlet(x, y);
                            break;
                        } 
                        current++;
                    }
                }

            }
            // Displaying all of the relevant information at the end of the day. 
            DisplayCompaniesAtDayEnd();
            DisplayEventsAtDayEnd();

            daysPassed++;
            Console.WriteLine("{0} days have passed. ", daysPassed);
        }

        private void AddCompany()
        {
            int balance, x = 0, y = 0;
            string companyName, typeOfCompany = "9";
            // creating a list that contains all of the names of the companies within the sim. 
            // These are stored as string and allow the DO WHILE loop after to ensure that a company of the same name cannot be generated. 
            // This is because a duplicate will already be inside the list. 
            List<string> names = new List<string>(); 
            foreach (Company c in companies)
            {
                names.Add(c.GetName());
            }

            do
            {
                Console.Write("Enter a name for the company: ");
                companyName = Console.ReadLine();
            }
            while (names.Contains(companyName) == true);


            Console.Write("Enter the starting balance for the company: ");
            balance = Convert.ToInt32(Console.ReadLine());
            while (typeOfCompany != "1" && typeOfCompany != "2" && typeOfCompany != "3")
            {
                Console.Write("Enter 1 for a fast food company, 2 for a family company or 3 for a named chef company: ");
                typeOfCompany = Console.ReadLine();
            }
            if (typeOfCompany == "1")
            {
                typeOfCompany = "fast food";
            }
            else if (typeOfCompany == "2")
            {
                typeOfCompany = "family";
            }
            else
            {
                typeOfCompany = "named chief";
            }
            simulationSettlement.GetRandomLocation(ref x, ref y);
            Company newCompany = new Company(companyName, typeOfCompany, balance, x, y, fuelCostPerUnit, baseCostForDelivery);
            companies.Add(newCompany);
        }

        public int GetIndexOfCompany(string companyName)
        { 
            // This algorithm is essentially a linear search. More efficient searching algorithms could be used but
            // this works perfectly and a more efficient algorithms is more effort than its worth. 
            int index = -1;
            for (int current = 0; current < companies.Count; current++)
            {
                // .ToLower() to make the method more robust. This is  becuase it is now case-insesitive.
                if (companies[current].GetName().ToLower() == companyName.ToLower())
                {
                    return current;
                }
            }
            return index;
        }

        public void ModifyCompany(int index)
        {
            string choice;
            int outletIndex, x, y;
            bool closeCompany;
            Console.WriteLine("\n*********************************");
            Console.WriteLine("*******  MODIFY COMPANY   *******");
            Console.WriteLine("*********************************");
            Console.WriteLine("1. Open new outlet");
            Console.WriteLine("2. Close outlet");
            Console.WriteLine("3. Expand outlet");
            Console.Write("\nEnter your choice: ");
            choice = Console.ReadLine();
            // Options 2 and 3 are both processed together because they both require outler ID whereas option 1 does not.
            if (choice == "2" || choice == "3")
            {
                Console.Write("Enter ID of outlet: ");
                outletIndex = Convert.ToInt32(Console.ReadLine());
                if (outletIndex > 0 && outletIndex <= companies[index].GetNumberOfOutlets())
                {
                    if (choice == "2")
                    {
                        closeCompany = companies[index].CloseOutlet(outletIndex - 1);
                        if (closeCompany)
                        {
                            Console.WriteLine("That company has now closed down as it has no outlets.");
                            companies.RemoveAt(index);
                        }
                    }
                    else
                    {
                        companies[index].ExpandOutlet(outletIndex - 1);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid outlet ID.");
                }
            }
            else if (choice == "1")
            {
                Console.Write("Enter X coordinate for new outlet: ");
                x = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Y coordinate for new outlet: ");
                y = Convert.ToInt32(Console.ReadLine());
                if (x >= 0 && x < simulationSettlement.GetXSize() && y >= 0 && y < simulationSettlement.GetYSize())
                {
                    companies[index].OpenOutlet(x, y);
                }
                else
                {
                    Console.WriteLine("Invalid coordinates.");
                }
            }
            Console.WriteLine();
        }

        public void DeleteCompany()
        {
            int index = -1;
            string companyName;
            bool closeCompany;
            int i;
            // checking to ensure a valid company name. Would be better to use a do loop here. 
            while (index == -1)
            {
                Console.Write(" Enter company name: ");
                companyName = Console.ReadLine();
                index = GetIndexOfCompany(companyName);
            }
            // removing all of the companies automatically. This is the place that improves from the previous system. 
            // Old system you must use delete outlet manually. 
            i = companies[index].GetNumberOfOutlets(); 
            do
            {
                closeCompany = companies[index].CloseOutlet(i - 1);
                i--;
            }
            while (closeCompany == false);
            // we can out that the company has closed because we know for certain that it has no outlets. 
            Console.WriteLine(" That company has now closed down as it has not outlets.");
            companies.RemoveAt(index);

        }

        public void DisplayCompanies()
        {
            // This is a relatively nice method. 
            Console.WriteLine("\n*********************************");
            Console.WriteLine("*** Details of all companies: ***");
            Console.WriteLine("*********************************\n");
            foreach (var c in companies)
            {
                Console.WriteLine(c.GetDetails() + "\n");
            }
            Console.WriteLine();
        }

        public void Run()
        {
            // This is the menu and amalgamation of all of the method discussed previously. 
            string choice = "";
            int index;
            string companyName;
            // Ensuring that the simulation will continue forever until, it is stopped by the user. 
            while (choice != "Q")
            {
                DisplayMenu();
                choice = Console.ReadLine();
                // The switch allows for many options to be added very easily and processed separately from eachother. 
                // It also makes the code much more readable and easy to program with. 
                switch (choice)
                {
                    case "1":
                        simulationSettlement.DisplayHouseholds();
                        break;
                    case "2":
                        DisplayCompanies();
                        break;
                    case "3":
                        index = -1;
                        while (index == -1)
                        {
                            Console.Write(" Enter company name: ");
                            companyName = Console.ReadLine();
                            index = GetIndexOfCompany(companyName);
                        }
                        ModifyCompany(index);
                        break;
                    case "4":
                        AddCompany();
                        break;
                    case "5": 
                        DeleteCompany();
                        break; 
                    case "6":
                        // To advance a number of days we can simplt process multiple days at once. This is shown below. 
                        // It is better to perform the advance within the switch and not the method itself because this will ensure
                        // items are in range and the create a more safe program -> defensive design. 
                        Console.Write(" Enter number of days to advance: ");
                        int num = Convert.ToInt32(Console.ReadLine());
                        int i = 0;
                        do
                        {
                            ProcessDayEnd();
                            i++;
                        }
                        while (i != num); 

                        break;
                    case "Q":
                        Console.WriteLine("Simulation finished, press Enter to close.");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Creating the simulation.
            Simulation thisSim = new Simulation();
            // Running the simulation. 
            thisSim.Run();
        }
    }
}