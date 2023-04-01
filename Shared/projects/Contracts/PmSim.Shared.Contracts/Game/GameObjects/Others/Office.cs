using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Exceptions;
using PmSim.Shared.Contracts.Game.GameObjects.Employees;

namespace PmSim.Shared.Contracts.Game.GameObjects.Others;

public class Office
{
    public int OwnerId { get; set; } = -1;

    public int Capacity { get; }
        
    public int RentalPrice { get; }

    public bool DoesHaveTechSupport { get; set; }
    
    public List<Employee> Employees { get; } = new();

    public Office(OfficeTypes type)
    {
        switch (type)
        {
            case OfficeTypes.Small:
                Capacity = GameConstants.SmallOfficeCapacity;
                RentalPrice = GameConstants.SmallOfficeRentPrice;
                break;
            case OfficeTypes.Middle:
                Capacity = GameConstants.MiddleOfficeCapacity;
                RentalPrice = GameConstants.MiddleOfficeRentPrice;
                break;
            case OfficeTypes.Big:
                Capacity = GameConstants.BigOfficeCapacity;
                RentalPrice = GameConstants.BigOfficeRentPrice;
                break;
            default:
                throw new PmSimException("Office type is out of range.");
        }
    }

    public void AddEmployee(Employee employee)
    {
        Employees.Add(employee);
    }
}