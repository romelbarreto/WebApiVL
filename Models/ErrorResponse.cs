﻿using Microsoft.AspNetCore.Mvc;

namespace WebApiVL.Models
{
    public class ErrorResponse
    {
        public static BadRequestObjectResult ReturnErrorResponse(string errorMessage)
        {
            return new BadRequestObjectResult(new MainResponse
            {
                ErrorMessage = errorMessage,
                IsSuccess = true
            });
        }
    }
}
