﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PickUpAndGo.Models.User;
using PickUpAndGo.Persistence;
using PickUpAndGo.Persistence.Context;
using PickUpAndGo.Persistence.Entities;

namespace PickUpAndGo.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/users")]
    public class UserController : CustomControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public UserController(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        /// <summary>
        /// Get user by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult GetById([FromRoute, Required] string id)
        {
            try
            {
                Console.WriteLine(id);
                if (string.IsNullOrWhiteSpace(id))
                    return BadRequest("ID is required!");

                var user = Uow.UserRepository.Get(id);

                if (user != null)
                {
                    var res = Mapper.Map<UserModel>(user);
                    return Ok(res);
                }

                return NotFound("User with given ID does not exist!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return InternalServerError();
            }
        }

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="createUserModel"></param>
        [HttpPost]
        [ProducesResponseType(typeof(UserModel), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create([FromBody] CreateUserModel createUserModel)
        {
            try
            {
                // TODO some validation before
                var entity = Mapper.Map<User>(createUserModel);

                Uow.UserRepository.Add(entity);

                var createResponse = await Uow.CompleteAsync();

                return Created(entity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return InternalServerError();
            }
        }
    }
}