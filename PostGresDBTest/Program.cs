using Dapper;
using Microsoft.Data.Sqlite;
using Npgsql;
using PostGresDBTest;

var cs = "Host=localhost;Username=postgres;Password=XXXX;Database=pagilla";
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
dbConnect(cs);
//dapperActors(cs);
updateRental(cs);
dapperRentals(cs);


static void updateRental(string cs)
{
    using (var connection = new NpgsqlConnection(cs))
    {
        DateTime newDate = DateTime.Now;
        string sqlQuery = $"UPDATE rental " +
            $"SET rental_date = '{newDate}'"+
            $" WHERE rental_id = 1";
        int rows = connection.Execute(sqlQuery);
        Console.WriteLine("number of rows updated: " + rows);
    }
    
}

static void dapperRentals(string cs)
{
    using (var connection = new NpgsqlConnection(cs))
    {
        //var sql = "select actor_id AS ActorID,first_name AS FirstName,last_name as LastName,last_update AS LastUpdate from actor";
        var sql = "select * from rental ORDER BY rental_id LIMIT 10";
        var rentals = connection.Query<Rental>(sql);
        foreach (var rental in rentals)
        {
            Console.WriteLine($"{rental.RentalId} {rental.CustomerId} {rental.RentalDate.ToLocalTime()}");
        }
        Console.ReadLine();
    }
}


static void dapperActors(string cs)
{
    Console.WriteLine("Trying dapper");
    using (var connection = new NpgsqlConnection(cs))
    {
        //var sql = "select actor_id AS ActorID,first_name AS FirstName,last_name as LastName,last_update AS LastUpdate from actor";
        var sql = "select * from actor";
        var actors = connection.Query<Actor>(sql);
        foreach (var actor in actors)
        {
            Console.WriteLine($"{actor.ActorID} {actor.FirstName} {actor.LastName} {actor.LastUpdate}");
        }
        Console.ReadLine();
    }
}


static void dbConnect(string cs)
{


    using var con = new NpgsqlConnection(cs);
    con.Open();

    var sql = "SELECT version()";

    using var cmd = new NpgsqlCommand(sql, con);

    var version = cmd.ExecuteScalar().ToString();
    Console.WriteLine($"PostgreSQL version: {version}");

}
