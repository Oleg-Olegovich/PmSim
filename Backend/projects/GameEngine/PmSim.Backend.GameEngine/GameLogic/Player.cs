using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PmSim.Backend.GameEngine.Dto;
using PmSim.Shared.Contracts.Enums;

namespace PmSim.Backend.GameEngine.GameLogic
{
    public class Player : Employee
    {
        public int Money { get; set; }

        public int CompletedProjects { get; set; }

        public bool IsStartupOpen { get; set; }

        public bool IsOut { get; set; }

        public string Name { get; }

        public int Id { get; }

        public bool IsBackgroundChosen { get; set; }

        /// <summary>
        /// This is necessary so that the player cannot exceed the number of action per stage.
        /// </summary>
        public int ActionsNumber { get; set; }

        /// <summary>
        /// This repository contains projects.
        /// </summary>
        public List<Project> Projects { get; } = new List<Project>();

        public List<int> Opportunities { get; } = new List<int>();

        public Player(int id, string name, int capital)
        {
            Id = id;
            Name = name;
            Money = capital;
        }

        public Player(int id, string name, Professions profession, int capital)
            : base(GetSkillsByProfession(profession))
        {
            Id = id;
            Money = capital;
            if (profession == Professions.Major)
            {
                Money *= 2;
            }

            Name = name;
        }

        protected Player(int id, string name, SkillsPoints skills, int capital)
            : base(skills)
        {
            Id = id;
            Name = name;
            Money = capital;
        }

        public void SetSkillsByProfession(Professions profession)
        {
            var skills = GetSkillsByProfession(profession);
            Skills.Programming = skills.Programming;
            Skills.Music = skills.Music;
            Skills.Design = skills.Design;
            Skills.Management = skills.Management;
            Skills.Creativity = skills.Creativity;
            if (profession == Professions.Major)
            {
                Money *= 2;
            }
        }

        /// <summary>
        /// Sums up the results of the sprint.
        /// </summary>
        public async Task SummingUp()
        {
            ActionsNumber = 0;
            await SummarizeProjectsResults();
            await CollectIncome();
            await CheckIsOut();
        }

        public async Task CheckIsOut()
        {
            if (Money < 0)
            {
                IsOut = true;
            }
        }

        /// <summary>
        /// Summarizes the results of the developments.
        /// If a project or feature has been completed, they begin to make a income.
        /// The reward is awarded immediately.
        /// </summary>
        private async Task SummarizeProjectsResults()
        {
            var random = new Random();
            foreach (var project in Projects)
            {
                if (project.IsDone && !project.Reward.IsCollected)
                {
                    Money += project.Reward.Prize;
                    for (var i = 0; i < project.Reward.Opportunities; ++i)
                    {
                        Opportunities.Add(random.Next(1));
                    }

                    project.Reward.IsCollected = true;
                    continue;
                }

                project.Deadline -= 1;
            }
        }

        /// <summary>
        /// Collects income from projects.
        /// </summary>
        private async Task CollectIncome()
        {
            foreach (var project in Projects.Where(project => project.IsDone))
            {
                Money += project.Reward.Revenue;
            }
        }

        private static SkillsPoints GetSkillsByProfession(Professions profession)
        {
            switch (profession)
            {
                case Professions.Programmer:
                    return new SkillsPoints(3, 1, 1, 1, 1);
                case Professions.Musician:
                    return new SkillsPoints(3, 1, 1, 1, 1);
                case Professions.Designer:
                    return new SkillsPoints(1, 1, 3, 1, 1);
                case Professions.Manager:
                    return new SkillsPoints(1, 1, 1, 3, 1);
                case Professions.Major:
                default:
                    return new SkillsPoints(1, 1, 1, 1, 1);
            }
        }
        /*
            => profession switch
            {
                Professions.Programmer => new SkillsPoints(3, 1, 1, 1, 1),
                Professions.Musician => new SkillsPoints(1, 3, 1, 1, 1),
                Professions.Designer => new SkillsPoints(1, 1, 3, 1, 1),
                Professions.Manager => new SkillsPoints(1, 1, 1, 3, 1),
                _ => new SkillsPoints(1, 1, 1, 1, 1)
            };
        */
    }
}