using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SampleApplication.Contacts
{
    public class UserContactsService
    {
        private readonly string _connectionString;

        public UserContactsService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        }

        public IEnumerable<UserContact> GetAll(string username)
        {
            var results = new List<UserContact>();

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                using (var command = new SqlCommand(
                                    @"SELECT [Id], [UserName], [ContactName], [PhoneNumber], [LastSentTo] 
                                      FROM [Contacts] 
                                      WHERE [UserName] = @username", sqlConnection))
                {
                    command.Parameters.AddWithValue("username", username);

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var contact = new UserContact
                        {
                            Id = int.Parse(reader["Id"].ToString()),
                            ContactName = reader["ContactName"].ToString(),
                            Username = reader["UserName"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            LastSentTo = reader["LastSentTo"] != DBNull.Value
                                            ? DateTime.Parse(reader["LastSentTo"].ToString())
                                            : (DateTime?) null
                        };

                        results.Add(contact);
                    }
                }
            }

            return results;
        }

        public UserContact Get(string username, int id)
        {
            UserContact result = null;

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                using (var command = new SqlCommand(
                                    @"SELECT [Id], [UserName], [ContactName], [PhoneNumber], [LastSentTo] 
                                      FROM [Contacts] 
                                      WHERE [UserName] = @username AND [Id] = @id", sqlConnection))
                {
                    command.Parameters.AddWithValue("id", id);
                    command.Parameters.AddWithValue("username", username);

                    var reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        result = new UserContact
                        {
                            Id = int.Parse(reader["Id"].ToString()),
                            ContactName = reader["ContactName"].ToString(),
                            Username = reader["UserName"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            LastSentTo = reader["LastSentTo"] != DBNull.Value
                                            ? DateTime.Parse(reader["LastSentTo"].ToString())
                                            : (DateTime?)null
                        };
                    }
                }
            }

            return result;
        }

        public void Create(UserContact contact)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                using (var command = new SqlCommand(
                                    @"INSERT INTO [Contacts] ([UserName], [ContactName], [PhoneNumber])
                                      VALUES (@username, @contactName, @phoneNumber)", sqlConnection))
                {
                    command.Parameters.AddWithValue("username", contact.Username);
                    command.Parameters.AddWithValue("contactName", contact.ContactName);
                    command.Parameters.AddWithValue("phoneNumber", contact.PhoneNumber);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(string username, int id)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                using (var command = new SqlCommand(
                                    @"DELETE FROM [Contacts]
                                      WHERE [UserName] = @username AND [Id] = @id", sqlConnection))
                {
                    command.Parameters.AddWithValue("username", username);
                    command.Parameters.AddWithValue("id", id);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(UserContact contact)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                using (var command = new SqlCommand(
                                    @"UPDATE [Contacts]
                                      SET [ContactName] = @contactName, [PhoneNumber] = @phoneNumber
                                      WHERE [UserName] = @username AND [Id] = @id", sqlConnection))
                {
                    command.Parameters.AddWithValue("id", contact.Id);
                    command.Parameters.AddWithValue("username", contact.Username);
                    command.Parameters.AddWithValue("contactName", contact.ContactName);
                    command.Parameters.AddWithValue("phoneNumber", contact.PhoneNumber);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateLastSentTime(string username, int contactId)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                using (var command = new SqlCommand(
                                    @"UPDATE [Contacts]
                                      SET [LastSentTo] = @now
                                      WHERE [UserName] = @username AND [Id] = @id", sqlConnection))
                {
                    command.Parameters.AddWithValue("id", contactId);
                    command.Parameters.AddWithValue("username", username);
                    command.Parameters.AddWithValue("now", DateTime.UtcNow);
                    
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}