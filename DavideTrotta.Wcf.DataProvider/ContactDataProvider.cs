using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DavideTrotta.Wcf.Contract.Entities;

namespace DavideTrotta.Wcf.DataProvider
{
    public interface IContactDataProvider
    {
        int CreateContact(Contact contact);
        int CreateContactDetail(Contact contact);
        Contact GetContactById(int id);
    }
    public class ContactDataProvider : IContactDataProvider
    {
        public int CreateContact(Contact contact)
        {
            string sql = @"
                        INSERT INTO [dbo].[Contact] ([Name],[Lastname]) VALUES (@Name,@Lastname);
                        SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDb"].ConnectionString))
            {
                return connection.Query<int>(sql, new { Name = contact.Name, Lastname = contact.Lastname }).Single();
            }
        }


        public Contact GetContactById(int id)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDb"].ConnectionString))
            {
                return
                    connection.Query<Contact>(@"SELECT * FROM Contact WHERE Id = @Id", new {id = id}).SingleOrDefault();
            }
        }


        public int CreateContactDetail(Contact contact)
        {
            string sql = @"
                        INSERT INTO [dbo].[ContactDetail] ([ContactId],[Name],[Lastname]) VALUES (@ContactId,@Name,@Lastname);
                        SELECT CAST(SCOPE_IDENTITY() as int)";


            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDb"].ConnectionString))
            {
                return connection.Query<int>(sql, new {ContactId = contact.Id, Name = contact.Name, Lastname = contact.Lastname }).Single();
            }
        }
    }
}
