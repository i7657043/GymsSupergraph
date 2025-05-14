namespace GymSupergraph.Libs;

//public class Manager
//{
//    public Manager(int id, int managerId, string managerName)
//    {
//        Id = id;
//        ManagerId = managerId;
//        ManagerName = managerName;
//    }

//    public int Id { get; set; }
//    public int ManagerId { get; set; }
//    public string ManagerName { get; set; } = string.Empty;
//}

public record Manager(int Id, int gymId, string ManagerName);