using SZFA_LAB05.Models;

CSVProcessor proc = new("sports.csv");
Workout[] workouts = proc.AllItems();
foreach (Workout workout in workouts)
{
    Console.WriteLine(workout);
}