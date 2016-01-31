using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using AssocOneToMany.Models;
using System.IO;
using Excel;

namespace AssocOneToMany.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        public Manager()
        {
            // If necessary, add constructor code here
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()

        // ############################################################
        // Team

        public IEnumerable<TeamBase> TeamGetAll()
        {
            return Mapper.Map<IEnumerable<TeamBase>>(ds.Teams.OrderBy(t => t.Name));
        }

        public TeamBase TeamGetById(int id)
        {
            // Attempt to fetch the object
            var o = ds.Teams.Find(id);

            // Return the result, or null if not found
            return (o == null) ? null : Mapper.Map<TeamBase>(o);
        }

        public TeamWithPlayers TeamGetByIdWithPlayers(int id)
        {
            // Attempt to fetch the object
            var o = ds.Teams.Include("Players").SingleOrDefault(t => t.Id == id);

            // Return the result, or null if not found
            return (o == null) ? null : Mapper.Map<TeamWithPlayers>(o);
        }

        // ############################################################
        // Player

        public IEnumerable<PlayerBase> PlayerGetAll()
        {
            return Mapper.Map<IEnumerable<PlayerBase>>(ds.Players.OrderBy(p => p.PlayerName));
        }

        public IEnumerable<PlayerWithTeamInfo> PlayerGetAllWithTeamInfo()
        {
            var c = ds.Players.Include("Team").OrderBy(p => p.PlayerName);

            return Mapper.Map<IEnumerable<PlayerWithTeamInfo>>(c);
        }

        public IEnumerable<PlayerWithTeamName> PlayerGetAllWithTeamName()
        {
            var c = ds.Players.Include("Team").OrderBy(p => p.PlayerName);

            return Mapper.Map<IEnumerable<PlayerWithTeamName>>(c);
        }

        public PlayerBase PlayerGetById(int id)
        {
            // Attempt to fetch the object
            var o = ds.Players.Find(id);

            // Return the result, or null if not found
            return (o == null) ? null : Mapper.Map<PlayerBase>(o);
        }

        // ############################################################
        // Load data (one-time task)

        public void LoadData()
        {
            // If there's data, then exit
            if (ds.Teams.Count() > 0) { return; }

            // Load the teams first

            ds.Teams.Add(new Team
            {
                City = "Glendale, AZ",
                CodeName = "ARI",
                Conference = "NFC",
                Division = "West",
                Name = "Arizona Cardinals",
                Stadium = "University of Phoenix Stadium",
                YearFounded = 1920
            });

            ds.Teams.Add(new Team
            {
                City = "Foxborough, MA",
                CodeName = "NE",
                Conference = "AFC",
                Division = "East",
                Name = "New England Patriots",
                Stadium = "Gillette Stadium",
                YearFounded = 1960
            });

            ds.Teams.Add(new Team
            {
                City = "Charlotte, NC",
                CodeName = "CAR",
                Conference = "NFC",
                Division = "South",
                Name = "Carolina Panthers",
                Stadium = "Bank of America Stadium",
                YearFounded = 1995
            });

            ds.Teams.Add(new Team
            {
                City = "Denver, CO",
                CodeName = "DEN",
                Conference = "AFC",
                Division = "West",
                Name = "Denver Broncos",
                Stadium = "Sports Authority Field at Mile High",
                YearFounded = 1960
            });

            ds.SaveChanges();

            // Now, load the players

            // This uses a nice little library from Dietmar Schoder
            // http://www.codeproject.com/Tips/801032/Csharp-How-To-Read-xlsx-Excel-File-With-Lines-of 

            // File system path to the data file (in this project's App_Data folder)
            string path = HttpContext.Current.Server.MapPath("~/App_Data/NFLPlayoffTeams.xlsx");

            // Get or open the workbook
            var wb = Workbook.Worksheets(path);

            // Go through all the worksheets in the workbook
            for (int i = 0; i < wb.Count(); i++)
            {
                // Get a reference to the current worksheet
                var ws = wb.ElementAt(i);

                // Current team
                Team currentTeam = null;

                // Worksheets can't be referenced by worksheet (tab) name
                // Therefore, we'll have to go by index
                if (i == 0) { currentTeam = ds.Teams.SingleOrDefault(t => t.CodeName == "DEN"); }
                if (i == 1) { currentTeam = ds.Teams.SingleOrDefault(t => t.CodeName == "CAR"); }
                if (i == 2) { currentTeam = ds.Teams.SingleOrDefault(t => t.CodeName == "NE"); }
                if (i == 3) { currentTeam = ds.Teams.SingleOrDefault(t => t.CodeName == "ARI"); }

                // Now, go through the list of players
                // Start at index 1, ignore the header row
                for (int j = 1; j < ws.Rows.Count(); j++)
                {
                    // Get a reference to the cell collection
                    // This just makes the syntax that follows easier to work with
                    var c = ws.Rows[j].Cells;

                    // Add a new player
                    ds.Players.Add(new Player
                    {
                        BirthDate = DateTime.Parse(c[6].Text),
                        College = c[8].Text,
                        Height = c[4].Text,
                        PlayerName = c[1].Text,
                        Position = c[2].Text,
                        UniformNumber = (int)c[0].Amount,
                        Weight = (int)c[5].Amount,
                        YearsExperience = (int)c[7].Amount,
                        Team = currentTeam
                    });
                }

                // After each team's players are processed, save the changes
                ds.SaveChanges();
            }
            // Done
        }


    }
}