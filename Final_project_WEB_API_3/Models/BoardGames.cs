using System.Text.Json.Serialization;

namespace Final_project_WEB_API_3.Models
{
    public class BoardGames
    {
        public int? ID { get; set; }
        public string? Name { get; set; }

        [JsonPropertyName("Year Published")]
        public int? YearPublished { get; set; }
        
        [JsonPropertyName("Min Players")]
        public int? MinPlayers { get; set; }
        
        [JsonPropertyName("Max Players")]
        public int? MaxPlayers { get; set; }
        
        [JsonPropertyName("Play Time")]
        public int? PlayTime { get; set; }
        
        [JsonPropertyName("Min Age")]
        public int? MinAge { get; set; }
        
        [JsonPropertyName("Users Rated")]
        public int? UsersRated { get; set; }
        
        [JsonPropertyName("Rating Average")]
        public string? RatingAverage { get; set; }
        
        [JsonPropertyName("BGG Rank")]
        public int? BGGRank { get; set; }
        
        [JsonPropertyName("Complexity Average")]
        public string? ComplexityAverage { get; set; }
        
        [JsonPropertyName("Owned Users")]
        public int? OwnedUsers { get; set; }
        public string? Mechanics { get; set; }
        public string? Domains { get; set; }

        public BoardGames(int? iD, string? name, int? yearPublished, int? minPlayers, int? maxPlayers, int? playTime, int? minAge, int? usersRated, string? ratingAverage, int? bGGRank, string? complexityAverage, int? ownedUsers, string? mechanics, string? domains)
        {
            ID = iD;
            Name = name;
            YearPublished = yearPublished;
            MinPlayers = minPlayers;
            MaxPlayers = maxPlayers;
            PlayTime = playTime;
            MinAge = minAge;
            UsersRated = usersRated;
            RatingAverage = ratingAverage;
            BGGRank = bGGRank;
            ComplexityAverage = complexityAverage;
            OwnedUsers = ownedUsers;
            Mechanics = mechanics;
            Domains = domains;  
        }
    }

}

