using PmSim.Shared.Contracts.Game.GameObjects.Employees;
using PmSim.Shared.Contracts.Game.GameObjects.Projects;
using PmSim.Shared.GameEngine.Dto;
using GameConstants = PmSim.Shared.Contracts.Game.GameConstants;

namespace PmSim.Shared.GameEngine.GameLogic;

internal class EmployeeLogic
{
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
            names = GameConstants.MaleNamesNumber;
            surnames = GameConstants.MaleSurnamesNumber;
        }
        else
        {
            names = GameConstants.FemaleNamesNumber;
            surnames = GameConstants.FemaleSurnamesNumber;
        }

        var nameIndex = random.Next(0, names);
        var surnameIndex = random.Next(0, surnames);
        return (nameIndex, surnameIndex);
    }

    internal static void Work(Employee employee)
    {
        if (employee.CurrentTask == null)
        {
            return;
        }

        switch (employee.CurrentTask)
        {
            case EmployeeBackUpTask backUpTask:
                backUpTask.Project.LastBackUp = (Project) backUpTask.Project.Clone();
                break;
            case EmployeeWorkTask workTask:
                switch (workTask.ProgressPointNumber)
                {
                    case 0:
                        workTask.Project.Points.Programming -= employee.Skills.Programming;
                        break;
                    case 1:
                        workTask.Project.Points.Design -= employee.Skills.Design;
                        break;
                    case 2:
                        workTask.Project.Points.Music -= employee.Skills.Music;
                        break;
                    default:
                        workTask.Project.Points.Management -= employee.Skills.Management;
                        break;
                }

                break;
            default:
                var result = (new Random()).NextDouble();
                if (result < (double) employee.Skills.Creativity / GameConstants.MaxSkillLevel)
                {
                    employee.CurrentTask.Player.Projects.Add(Game.GetProjectRandomly());
                }

                break;
        }

        employee.CurrentTask = null;
    }
}