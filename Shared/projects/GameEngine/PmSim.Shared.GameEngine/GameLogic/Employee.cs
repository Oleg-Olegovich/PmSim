using System;
using PmSim.Shared.Contracts.Game;
using PmSim.Shared.GameEngine.Dto;
using PmSim.Shared.GameEngine.Interfaces;

namespace PmSim.Shared.GameEngine.GameLogic
{
    internal class Employee : IAuctionLot
    {
        internal (int, int) NameNumbers { get; }

        internal SkillsPoints Skills { get; }

        internal int DesiredSalary
            => Math.Min(Math.Max(1,
                Constants.MaxSalary * (Skills.Programming + Skills.Music + Skills.Design + Skills.Management)
                / (4 * Constants.MaxSkillLevel) + (new Random()).Next(-1, 2)), Constants.MaxSalary);

        internal int Salary { get; set; }

        internal EmployeeTask CurrentTask { get; set; }

        protected Employee()
        {
            Skills = new SkillsPoints(0, 0, 0, 0, 0);
        }

        protected Employee(SkillsPoints skills)
        {
            NameNumbers = (0, 0);
            Skills = skills;
        }

        private Employee((int, int) nameNumbers, SkillsPoints skills)
        {
            NameNumbers = nameNumbers;
            Skills = skills;
        }

        internal void Work()
        {
            if (CurrentTask == null)
            {
                return;
            }

            switch (CurrentTask)
            {
                case EmployeeBackUpTask backUpTask:
                    backUpTask.Project.LastBackUp = (Project) backUpTask.Project.Clone();
                    break;
                case EmployeeWorkTask workTask:
                    switch (workTask.ProgressPointNumber)
                    {
                        case 0:
                            workTask.Project.Points.Programming -= Skills.Programming;
                            break;
                        case 1:
                            workTask.Project.Points.Design -= Skills.Design;
                            break;
                        case 2:
                            workTask.Project.Points.Music -= Skills.Music;
                            break;
                        default:
                            workTask.Project.Points.Management -= Skills.Management;
                            break;
                    }

                    break;
                default:
                    var result = (new Random()).NextDouble();
                    if (result < (double) Skills.Creativity / Constants.MaxSkillLevel)
                    {
                        CurrentTask.Player.Projects.Add(Game.GetProjectRandomly());
                    }

                    break;
            }

            CurrentTask = null;
        }

        internal static Employee Generate()
        {
            var name = GenerateName();
            var skills = new int[4];
            var random = new Random();
            var profession = random.Next(4);
            var type = random.Next(101);
            var creativity = 0;
            if (type < 50)
            {
                skills[profession] = random.Next(1, 3);
                creativity += random.Next(2);
            }
            else if (type < 85)
            {
                skills[0] = skills[1] = skills[2] = skills[3] = 1;
                skills[profession] = random.Next(1, 3);
                creativity += random.Next(1, 3);
            }
            else if (type < 95)
            {
                skills[profession] = random.Next(3, 5);
                creativity += random.Next(2, 4);
            }
            else if (type == 100)
            {
                skills[0] = skills[1] = skills[2] = skills[3] = 10;
                creativity = 5;
            }
            else
            {
                skills[0] = skills[1] = skills[2] = skills[3] = random.Next(1, 3);
                skills[profession] = random.Next(3, 5);
                creativity += random.Next(3, 4);
            }

            return new Employee(name, new SkillsPoints(skills[0], skills[2], skills[3],
                skills[4], creativity));
        }

        internal static Employee[] Generate(int number)
        {
            var employees = new Employee[number];
            for (var i = 0; i < employees.Length; ++i)
            {
                employees[i] = Generate();
            }

            return employees;
        }

        private static (int, int) GenerateName()
        {
            var random = new Random();
            var isMale = random.Next(100) < 50;
            int names, surnames;
            if (isMale)
            {
                names = Constants.MaleNamesNumber;
                surnames = Constants.MaleSurnamesNumber;
            }
            else
            {
                names = Constants.FemaleNamesNumber;
                surnames = Constants.FemaleSurnamesNumber;
            }

            var nameIndex = random.Next(0, names);
            var surnameIndex = random.Next(0, surnames);
            return (nameIndex, surnameIndex);
        }
    }
}