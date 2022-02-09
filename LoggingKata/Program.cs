using GeoCoordinatePortable;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            // TODO:  Find the two Taco Bells that are the furthest from one another.
            // HINT:  You'll need two nested forloops ---------------------------

            logger.LogInfo("Log initialized . . . . . who cares");


            // use File.ReadAllLines(path) to grab all the lines from your csv file
            // Log and error if you get 0 lines and a warning if you get 1 line
            string[] lines = File.ReadAllLines(csvPath);
           
            if(lines.Length==0)
            {
                logger.LogError("file has no input");
            }
            if (lines.Length==1)
            {
                logger.LogWarning("file only has one line of input");
            }


            logger.LogInfo($"Lines: {lines[0]}");

            // Create a new instance of your TacoParser class
            var parser = new TacoParser();

            // Grab an IEnumerable of locations using the Select command: var locations = lines.Select(parser.Parse);
            ITrackable[] locations = lines.Select(line => parser.Parse(line)).ToArray();

            var tacoList = new List<ITrackable>();
            foreach(var line in lines)
            {
                tacoList.Add(parser.Parse(line));
            }
            // DON'T FORGET TO LOG YOUR STEPS

            // Now that your Parse method is completed, START BELOW ----------

            // TODO: Create two `ITrackable` variables with initial values of `null`. These will be used to store your two taco bells that are the farthest from each other.
            // Create a `double` variable to store the distance
            ITrackable tacoBell1 = null;
            ITrackable tacoBell2 = null;
            double distance = 0;

            // Include the Geolocation toolbox, so you can compare locations: `using GeoCoordinatePortable;`

            //HINT NESTED LOOPS SECTION---------------------
            // Do a loop for your locations to grab each location as the origin (perhaps: `locA`)
            for (int i = 0; i < locations.Length; i++)
            {
                // Create a new corA Coordinate with your locA's lat and long
                var locA = locations[i];

                // Create a new Coordinate with your locB's lat and long
                var CorA = new GeoCoordinate();
                var corB = new GeoCoordinate();
                ITrackable locB = null;
                
                CorA.Latitude = locA.Location.Latitude;
                CorA.Longitude = locA.Location.Longitude;

                // Now, do another loop on the locations with the scope of your first loop, so you can grab the "destination" location (perhaps: `locB`)
                for (int j = 0; j < locations.Length; j++)
                {
                    locB = locations[j];
                    corB.Latitude = locA.Location.Latitude;
                    corB.Longitude = locA.Location.Longitude;
                }




                // Now, compare the two using `.GetDistanceTo()`, which returns a double
                // If the distance is greater than the currently saved distance, update the distance and the two `ITrackable` variables you set above
                if (CorA.GetDistanceTo(corB) > distance)
                {
                    distance = CorA.GetDistanceTo(corB);
                    tacoBell1 = locA;
                    tacoBell2 = locB;

                }

                // Once you've looped through everything, you've found the two Taco Bells farthest away from each other.
                logger.LogInfo($"{tacoBell1.Name} and {tacoBell2.Name} are the farthest appart. ");
            }





        }
    }
}
