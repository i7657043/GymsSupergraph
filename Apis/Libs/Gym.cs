namespace GymSupergraph.Libs;

//public class Gym
//{
//    public Gym(int id, string gymName)
//    {
//        Id = id;
//        GymName = gymName;
//    }

//    public int Id;
//    public string GymName { get; set; } = string.Empty;
//}

public record Gym(int Id, string GymName);