﻿
namespace FidelityHub.Application.Interfaces
{
    public interface ITokenFactory
    {
        string GenerateToken(int size= 32);
    }
}
