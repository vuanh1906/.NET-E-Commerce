﻿using Core.Entities;

namespace Skinet.Dtos
{
    public class UserDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public BasketDto Basket { get; set; }
    }
}
