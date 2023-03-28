using System;
using System.Collections.Generic;
using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game;
using PmSim.Shared.GameEngine.GameLogic;

namespace PmSim.Shared.GameEngine.Dto
{
    internal class Office
    {
        private int _ownerId = -1;

        internal int OwnerId
        {
            get => _ownerId;
            set
            {
                _ownerId = value;
            }
        }

        internal int Capacity { get; }
        
        internal int RentalPrice { get; }

        internal List<Employee> Employees { get; } = new();

        internal bool DoesHaveTechSupport { get; set; }

        internal Office(OfficeTypes type)
        {
            switch (type)
            {
                case OfficeTypes.Small:
                    Capacity = Constants.SmallOfficeCapacity;
                    RentalPrice = Constants.SmallOfficeRentPrice;
                    break;
                case OfficeTypes.Middle:
                    Capacity = Constants.MiddleOfficeCapacity;
                    RentalPrice = Constants.MiddleOfficeRentPrice;
                    break;
                case OfficeTypes.Big:
                    Capacity = Constants.BigOfficeCapacity;
                    RentalPrice = Constants.BigOfficeRentPrice;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        internal void AddEmployee(Employee employee)
        {
            Employees.Add(employee);
        }
    }
}