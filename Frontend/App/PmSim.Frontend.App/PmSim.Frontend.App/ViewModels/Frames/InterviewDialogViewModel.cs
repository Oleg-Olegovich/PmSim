using System;
using PmSim.Frontend.App.ViewModels.Screens;
using PmSim.Shared.Contracts.Game.Employees;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Frames;

public class InterviewDialogViewModel : BasicFrameViewModel
{
    private int _salary = 1;

    public int Salary
    {
        get => _salary;
        set => this.RaiseAndSetIfChanged(ref _salary, value);
    }
    
    public EmployeeStatus Employee { get; }
    
    public InterviewDialogViewModel(GameScreenViewModel gameScreen) : base(gameScreen)
    {
        Employee = Generate();
    }
    
    private static EmployeeStatus Generate()
    {
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

        return new EmployeeStatus()
        {
            Name = "Иван Иванов",
            Skills = new SkillsPoints(skills[0], skills[1], skills[2], skills[3], creativity)
        };
    }
}