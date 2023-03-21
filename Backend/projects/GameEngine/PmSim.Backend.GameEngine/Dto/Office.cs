using System;
using System.Collections.Generic;
using PmSim.Backend.GameEngine.GameLogic;
using PmSim.Backend.Gateway.Contracts.Enums;

namespace PmSim.Backend.GameEngine.Dto
{
    public class Office
    {
        private int _ownerId = -1;

        public int OwnerId
        {
            get => _ownerId;
            set
            {
                _ownerId = value;
            }
        }

        public int Capacity { get; }
        
        public int RentalPrice { get; }

        public List<Employee> Employees { get; } = new List<Employee>();

        public bool DoesHaveTechSupport { get; set; }

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
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public void AddEmployee(Employee employee)
        {
            Employees.Add(employee);
        }
    }
}