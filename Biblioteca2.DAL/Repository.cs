using Biblioteca2.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace Biblioteca2.DAL
{
	public class Repository
	{
		private SQLiteConnection _connection;

		public Repository()
		{
			_connection = new SQLiteConnection(
				"Data Source=database.db; Version = 3; New = True; Compress = True; ");
			_connection.Open();

		}

		public void AdaugaCarte(CarteEntity carte)
		{
			SQLiteCommand command = new SQLiteCommand(_connection);
			command.CommandText = $"INSERT INTO Carti (NumeleCartii, NumeAutor, Editura, DataPublicarii, NumarPagini) VALUES ('{carte.NumeleCartii}', '{carte.NumeAutor}','{carte.Editura}', '{carte.DataPublicarii}','{carte.NumarPagini}')";
			command.ExecuteNonQuery();
		}

		public List<CarteEntity> CitesteCartile()
		{
			SQLiteCommand command = new SQLiteCommand(_connection);
			command.CommandText = "Select * from Carti";
			SQLiteDataReader reader = command.ExecuteReader();

			List<CarteEntity> listaCarti = new List<CarteEntity>();

			while (reader.Read())
			{
				CarteEntity carte = new CarteEntity();
				carte.Id = reader.GetInt32(0);
				carte.NumeleCartii = reader.GetString(1);
				carte.NumeAutor = reader.GetString(2);
				carte.Editura = reader.GetString(3);
				carte.DataPublicarii = reader.GetDateTime(4);
				carte.NumarPagini = reader.GetInt32(5);

				listaCarti.Add(carte);
			}
			return listaCarti;
		}
	}
}
