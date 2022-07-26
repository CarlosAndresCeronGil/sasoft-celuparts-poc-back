namespace CelupartsPoC
{
    public class DataList
    {
        public static List<UserDto> users = new List<UserDto>
        {
                new UserDto{ IdUser = 1, IdType = "CC", Names = "Usuario",
                    Surnames = "Test 1", Phone = "3219819696",
                    Email = "user1@email.com", Password = "12345",
                    AccountStatus = "Habilitada",
                },
                new UserDto{ IdUser = 2, IdType = "CC", Names = "Usuario",
                    Surnames = "Test 2", Phone = "45154848949",
                    Email = "user2@email.com", Password = "12345",
                    AccountStatus = "Habilitada"
                },
                new UserDto{ IdUser = 3, IdType = "CC", Names = "Usuario",
                    Surnames = "Test 3", Phone = "15158485",
                    Email = "user3@email.com", Password = "12345",
                    AccountStatus = "Habilitada"
                }
         };

        public static List<Request> requests = new List<Request>
        {
                new Request{ IdRequest =  1, UserDto = { IdUser = 1 },
                RequestType = "Reparacion" , PickUpAddress = "Calle 123",
                DeliveryAddress = "Calle 123", PickUpTime = new DateTime(2022, 7, 26),
                PaymentMethod = "Contraentrega", Status = "Iniciada", Quote = 120000,
                StatusQuote = "Pendiente", TechnicalRemarks = "Daño en la pantalla"}
        };
    }
}
