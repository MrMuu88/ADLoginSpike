using System.DirectoryServices.AccountManagement;

// See https://aka.ms/new-console-template for more information


var principalcontext = new PrincipalContext(ContextType.Machine); //on AD set to ContextType.Domain, 

string userName = "";
string password = "";
bool isValid = false;

Console.WriteLine("Please provide Credentials");
do
{
    Console.WriteLine("UserName:");
    userName = Console.ReadLine();

    Console.WriteLine("Password:");
     password = Console.ReadLine();

    //validate UserPass
    isValid = principalcontext.ValidateCredentials(userName, password,ContextOptions.Negotiate); 
    if (isValid)
    {
        Console.WriteLine("User Pass valid");
    }
    else { 
        Console.WriteLine("Incorrect User or Password");
    }
} while (!isValid);

Console.WriteLine("Querying User Groups");
UserPrincipal user = UserPrincipal.FindByIdentity(principalcontext,IdentityType.SamAccountName,userName);

PrincipalSearchResult<Principal> groups = user.GetGroups();

Console.WriteLine("User is Member of:\n\t" +
    string.Join(
        ";" , groups.Select(g => g.Name).ToList()
    )
);

