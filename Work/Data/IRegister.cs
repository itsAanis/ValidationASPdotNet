using System;
namespace advanced.Data
{
	public interface IRegister
	{
        bool Register(string[] fields);
        bool EmailExists(string emailAddress);

    }
}

