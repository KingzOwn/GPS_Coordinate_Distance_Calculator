using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace GPS_Coordinate_Distance_Calculator
{
    class Program
    {
        private static double Distance(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(Deg2rad(lat1)) * Math.Sin(Deg2rad(lat2)) + Math.Cos(Deg2rad(lat1)) * Math.Cos(Deg2rad(lat2)) * Math.Cos(Deg2rad(theta));
            dist = Math.Acos(dist);
            dist = Rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist * 0.8684;
            }
            return (dist);
        }

        private static double Deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private static double Rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }

        static void Main(string[] args)
        {
            double totalDistance = 0;
            double currentDistance;

            GeoCoordinate lastCoordinate;
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\d\Desktop\gps-vehicle-1374 (2) (1).csv");
            file.ReadLine();
            string[] splitLine = file.ReadLine().Split(',');
            lastCoordinate = new GeoCoordinate(Convert.ToDouble(splitLine[3]), Convert.ToDouble(splitLine[4]));
            System.Diagnostics.Debug.WriteLine($"Coordinate: {splitLine[3]}, {splitLine[4]}.  Location: {splitLine[1]}");
            string line;
            line = file.ReadLine();
            while (line != null)
            {
                splitLine = line.Split(',');
                GeoCoordinate currentCoordinate = new GeoCoordinate(Convert.ToDouble(splitLine[3]), Convert.ToDouble(splitLine[4]));
                //currentDistance = lastCoordinate.GetDistanceTo(currentCoordinate);
                currentDistance = Distance(lastCoordinate.Latitude, lastCoordinate.Longitude, currentCoordinate.Latitude, currentCoordinate.Longitude, 'K');
                totalDistance += currentDistance;
                System.Diagnostics.Debug.WriteLine($"Coordinate: {splitLine[3]}, {splitLine[4]} Current Distance: {currentDistance}  Total Distance: {totalDistance} Location: {splitLine[1]}");
                lastCoordinate = currentCoordinate;
                line = file.ReadLine();
            }
            
        }

        
    }
}
