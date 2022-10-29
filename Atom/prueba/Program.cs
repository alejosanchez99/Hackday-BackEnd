// See https://aka.ms/new-console-template for more information

using Atom.Api.RequestApi;
using Atom.Entities;

Api api = new Api();

RequestUser requestUser = new RequestUser
{
    Users = new List<User>
    {
        new User
        {
            id = "21sqdsd",
            name = "alejandro"
        }
    }
};

string url = "https://sockethackday.azurewebsites.net/api/userhub/calls";
string url2 = "https://mqjl9s6vf4.execute-api.eu-west-1.amazonaws.com/prod/v1/hackday/public/event";
try
{
    //bool task = await api.InvokeIntegrationAsync(requestUser, url);
    //var x = await api.InvokeIntegrationAsync<>(url2);
}
catch (Exception)
{
    throw;
}
