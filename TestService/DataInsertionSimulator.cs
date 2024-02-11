using TestService.Models;

namespace TestService
{
    public static class DataInsertionSimulator
    {
        public static List<UserData> ReadUserDataFromFile(string filePath)
        {
            List<UserData> userDataList = new List<UserData>();
            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] values = line.Split(',');

                    if (values.Length != 6)
                    {
                        continue;
                    }

                    UserData userData = new UserData
                    {
                        URN = GenerateRandomURN(),
                        Username = values[0],
                        PhoneNumber = values[1],
                        EmailAddress = values[2],
                        Age = values[3],
                        UserAddress = values[4],
                        EncryptedBankAccountNumber = values[5]
                    };

                    userDataList.Add(userData);
                }
            }
            catch (Exception ex)
            {
             
                Console.WriteLine($"Error reading file: {ex.Message}");
            }

            return userDataList;
        }

        private static string GenerateRandomURN()
        {
            return Guid.NewGuid().ToString();
        }

    }
}
