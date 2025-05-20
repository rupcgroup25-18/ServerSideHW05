namespace HW4ServerSide.DTO
{
    public class RentedMoviesDTO
    {
        int userId;
        int movieId;
        DateTime rentStart;
        DateTime rentEnd;

        public RentedMoviesDTO(int userId, int movieId, DateTime rentStart, DateTime rentEnd)
        {
            UserId = userId;
            MovieId = movieId;
            RentStart = rentStart;
            RentEnd = rentEnd; 
        }

        public RentedMoviesDTO() { }

        public int UserId { get => userId; set => userId = value; }
        public int MovieId { get => movieId; set => movieId = value; }
        public DateTime RentStart { get => rentStart; set => rentStart = value; }
        public DateTime RentEnd { get => rentEnd; set => rentEnd = value; }
    }
}
