using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gecko.Bcon.Common;
using Gecko.Bcon.DataAccess;
using Gecko.Bcon.DataAccess.Repositories;
using Gecko.Bcon.Domain;
using NHibernate;

namespace Gecko.Bcon.Schema
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ISession session = ConnectionManager.OpenSession()) {
                session.BeginTransaction();
                var admin = new UserRepository(session).GetById(1);
                if (admin == null) {
                   CreateAdmin(session);
                }
                session.Transaction.Commit();
            }
            Console.ReadLine();
        }

        private static void CreateAdmin(ISession session) {
            var david = new User();
            david.CreatedOn = DateTime.UtcNow;
            david.Email = "david@gecko-is.co.uk";
            david.FirstName = "David";
            david.LastName = " Rankin";
            session.Save(david);
            david.Password = CryptoLibrary.GetHash(david.Id.ToString() + "Secret");
            david.Role = User.Roles.Admin;
            david.State = User.States.Active;

            session.Save(david);

            EmergencyProfile profile = new EmergencyProfile();
            profile.NextOfKinName = "Lizzy ";
            profile.NextOfKinNumber = "00 80000 000";
            profile.User = david;
            profile.KeyCode = "David";
            profile.Allergies = "Cats, Pollen, Dust";
            profile.Medication = "Antihistamine";
            profile.Conditions = "None";
            session.Save(profile);

        }
    }
}
