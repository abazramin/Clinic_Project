using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Data
{
    public class clsPersonData
    {
        public static bool GetPersonByID(int PersonID, ref string Name, ref DateTime DateOfBirth, ref byte Gendor,
            ref string Phone, ref string Email, ref string Address)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsSetting.connection);

            string query = "select * from Person where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    Name = (string)reader["Name"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (byte)reader["Gendor"];
                    Phone = (string)reader["Phone"];
                    Email = (string)reader["Email"];
                    Address = (string)reader["Address"];

                }
                else
                {
                    isFound = false;
                }

                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static bool GetPersonByName(  string Name, ref int PersonID, ref DateTime DateOfBirth, ref byte Gendor,
            ref string Phone, ref string Email, ref string Address)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsSetting.connection);

            string query = "SELECT * FROM Persons where Name = @Name";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Name", Name);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    PersonID = (int)reader["PersonID"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (byte)reader["Gendor"];
                    Phone = (string)reader["Phone_Number"];
                    Email = (string)reader["Email"];
                    Address = (string)reader["Address"];

                }
                else
                {
                    isFound = false;
                }

                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static DataTable GetAllPerson()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsSetting.connection);

            string query = @"select Persons.PersonID , Persons.Name , Persons.DateOfBirth , 
                            case  when Persons.Gendor = 0 then 'Male' Else 'Famele' end as Gendor ,
                            Persons.Phone_Number , Persons.Email , Persons.Address 
                            from Persons
                            order by Name";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    dt.Load(reader);
                }

                reader.Close();
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }
        public static int AddNewPerson(string Name,   DateTime DateOfBirth,  byte Gendor,
            string Phone_Number, string Email, string Address)
        {
            int PersonID = -1;


            SqlConnection connection = new SqlConnection(clsSetting.connection);

            string query = @"INSERT INTO Persons 
                             (Name,DateOfBirth,Gendor,Phone_Number,Email,Address)
                             VALUES
                             (@Name,@DateOfBirth,@Gendor,@Phone_Number,@Email,@Address);
                             SELECT SCOPE_IDENTITY()";


            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            cmd.Parameters.AddWithValue("@Gendor", Gendor);
            cmd.Parameters.AddWithValue("@Phone_Number", Phone_Number);


            if (Email != "" && Email != null)
            {
                cmd.Parameters.AddWithValue("@Email", Email);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Email", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@Address", Address);


            try
            {
                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString() , out int InsertID))
                {
                    PersonID = InsertID;
                }

            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }


            return PersonID;
        }
        public static bool UpdatePerson(int PersonID ,string Name, DateTime DateOfBirth, byte Gendor,
        string Phone, string Email, string Address)
        {
            int rowAffected = 0;

            SqlConnection connection = new SqlConnection(clsSetting.connection);

            string query = @"UPDATE Persons
                           SET 
                            Name = @Name,
                            DateOfBirth = @DateOfBirth, 
                            Gendor = @Gendor, 
                            Phone_Number = @Phone_Number, 
                            Email = @mail, 
                            Address = @Address
                            WHERE PersonID = @PersonID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            cmd.Parameters.AddWithValue("@Gendor", Gendor);
            cmd.Parameters.AddWithValue("@Phone_Number", Phone);
            if (Email != "" && Email != null)
            {
                cmd.Parameters.AddWithValue("@Email", Email);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Email", DBNull.Value);
            }

            cmd.Parameters.AddWithValue("@Address", Address);


            try
            {
                connection.Open();
                rowAffected = cmd.ExecuteNonQuery();

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            } finally
            {
                connection.Close();
            }


            return (rowAffected > 0);
        }
        public static bool DeletePerson(int PersonID) {

            int rowEff = -1;


            SqlConnection connection = new SqlConnection(clsSetting.connection);

            string query = "DELETE FROM Persons WHERE PersonID = @PersonID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                int rowAffected = cmd.ExecuteNonQuery();


            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }


            return (rowEff > 0);
        }

        public static bool IsExsit(int PersonID)
        {
            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsSetting.connection);

            string query = @"Select Found = 1 From Persons Where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }


            return isFound;
        }

        public static bool IsExsit(string Name)
        {
            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsSetting.connection);

            string query = @"Select Found = 1 From Persons Where Name = @Name";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Name", Name);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
    }
}
